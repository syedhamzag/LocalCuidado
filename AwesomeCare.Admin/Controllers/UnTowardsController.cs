using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Untowards;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AwesomeCare.Admin.Extensions;
using AwesomeCare.Admin.Services.Untowards;
using AwesomeCare.Admin.Models;
using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.Untowards;
using AwesomeCare.Services.Services;
using AwesomeCare.DataTransferObject.Models.MailChimp;

namespace AwesomeCare.Admin.Controllers
{
    public class UnTowardsController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<UnTowardsController> _logger;
        private IClientService _clientService;
        private IUntowardsService _untowardsService;
        private readonly IMailChimpService mailChimpService;

        public UnTowardsController(IStaffService staffService,
            IFileUpload fileUpload,
            ILogger<UnTowardsController> logger,
            IClientService clientService,
            IUntowardsService untowardsService,
           IMailChimpService mailChimpService) : base(fileUpload)
        {
            _staffService = staffService;
            _logger = logger;
            _clientService = clientService;
            _untowardsService = untowardsService;
            this.mailChimpService = mailChimpService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _untowardsService.Get();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var staffs = await _staffService.GetStaffs();
            var clients = await _clientService.GetClientDetail();
            var clientInvolvingParties = await _clientService.GetClientInvolvingPartyBase();

            var model = new CreateUntowards();
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.HomeCareClients = clients.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            model.ClientInvolvingParties = clientInvolvingParties.Select(s => new SelectListItem(s.ItemName, s.ClientInvolvingPartyItemId.ToString())).ToList();

            HttpContext.Session.Set<List<GetStaffs>>("staffs", staffs);
            HttpContext.Session.Set<List<GetClientDetail>>("clients", clients);
            HttpContext.Session.Set<List<GetClientInvolvingPartyItem>>("clientInvolvingParties", clientInvolvingParties);


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUntowards model)
        {
            model.IsBlackListRequired = string.Equals(model.IsBlackListed, "yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            model.IsHospitalEntry = string.Equals(model.HospitalEntry, "yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            model.IsHospitalExit = string.Equals(model.HospitalExit, "yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            model.ShouldNotifyInvolvingStaff = string.Equals(model.NotifyInvolvingStaff, "yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;

            model.OfficerToAct = model.OfficersToAct.Select(o => new DataTransferObject.DTOs.Untowards.PostUntowardsOfficerToAct
            {
                StaffPersonalInfoId = int.Parse(o)
            }).ToList();

            model.StaffInvolved = model.StaffsInvolved.Select(s => new DataTransferObject.DTOs.Untowards.PostUntowardsStaffInvolved
            {
                StaffPersonalInfoId = int.Parse(s)
            }).ToList();

            if (model.StaffInvolved.Count == 0)
            {
                ModelState.AddModelError("StaffInvolved", "select at least a staff");
            }

            TryValidateModel(model);

            var staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");
            if (staffs == null)
            {
                staffs = await _staffService.GetStaffs();
            }
            if (model == null || !ModelState.IsValid)
            {
                model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.HomeCareClients = HttpContext.Session.Get<List<GetClientDetail>>("clients").Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                model.ClientInvolvingParties = HttpContext.Session.Get<List<GetClientInvolvingPartyItem>>("clientInvolvingParties").Select(s => new SelectListItem(s.ItemName, s.ClientInvolvingPartyItemId.ToString())).ToList();

                return View(model);
            }


            if (model.FileAttachment != null)
            {
                var filepath = await _fileUpload.UploadFile("untowards", false, model.FileAttachment.FileName, model.FileAttachment.OpenReadStream(),model.FileAttachment.ContentType);
                model.Attachment = filepath;
            }

            var postModel = Mapper.Map<PostUntowards>(model);
            var result = await _untowardsService.PostUntowards(postModel);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Untowards successfully saved" : "An Error Occurred" });

            if (model.ShouldNotifyInvolvingStaff && result.IsSuccessStatusCode)
            {
                await SendEmailAsync(model, staffs);
            }
            //Send mail to Officer to act
            await SendEmailToOfficerToAct(model, model.OfficersToAct);

            return RedirectToAction("Index");

        }

        async Task SendEmailToOfficerToAct(CreateUntowards model,List<string> emails)
        {
            try
            {
                if (emails.Count == 0) return;
                var recipients = emails.Select(e => new DataTransferObject.Models.MailChimp.Recipient { Email = e, Name = "", Type = DataTransferObject.Enums.EmailTypeEnum.to }).ToList();

                await mailChimpService.SendAsync($"Untowards - {model.Subject}", $"Hello, {Environment.NewLine} you have a pending untowards message.", false, recipients);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmail");
            }
        }

        async Task SendEmailAsync(CreateUntowards model, List<GetStaffs> staffs)
        {
            try
            {
                List<Recipient> staffToSendEmail = new List<Recipient>();
                foreach (var staff in model.StaffInvolved)
                {
                    var email = staffs.FirstOrDefault(s => s.StaffPersonalInfoId == staff.StaffPersonalInfoId);
                    if (email != null)
                        staffToSendEmail.Add(new Recipient { Email = email.Email,Name = email.Fullname, Type = DataTransferObject.Enums.EmailTypeEnum.to});
                }

                await mailChimpService.SendAsync($"Untowards - {model.Subject}", $"Hello, {Environment.NewLine} you have a pending untowards message.", false, staffToSendEmail);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmail");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _untowardsService.Get(id);
            return View(result);
        }

    }
}