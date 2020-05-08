using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffCommunication;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.Admin.ViewModels.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{

    public class StaffController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<StaffController> _logger;
        private IFileUpload _fileUpload;
        private IStaffCommunication _staffCommunication;
        private IClientRotaTypeService _clientRotaTypeService;
        private IClientRotaNameService _clientRotaNameService;
        public StaffController(IStaffService staffService, IClientRotaNameService clientRotaNameService, ILogger<StaffController> logger, IFileUpload fileUpload, IStaffCommunication staffCommunication, IClientRotaTypeService clientRotaTypeService) : base(fileUpload)
        {
            _staffService = staffService;
            _logger = logger;
            _fileUpload = fileUpload;
            _staffCommunication = staffCommunication;
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaNameService = clientRotaNameService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var staffs = await _staffService.GetStaffs();

                return View(staffs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetStaffs");
                throw;
            }

        }

        public async Task<IActionResult> Details(int staffId)
        {
            var profile = await _staffService.Profile(staffId);
            profile.Statuses.ForEach(s =>
            {
                if (s.Value == profile.Status.ToString() || s.Text == profile.Status.ToString())
                    s.Selected = true;
            });
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStaffRecord(StaffDetails staff)
        {
            if (!ModelState.IsValid)
            {
                return View(staff);
            }

            var postApproval = new PostStaffApproval
            {
                Comment = staff.Comment,
                Rate = staff.Rate,
                StaffPersonalInfoId = staff.StaffPersonalInfoId,
                Status = staff.Status
            };
            var result = await _staffService.Approval(postApproval);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Successfully Updated" : "An Error Occurred" });
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Communication()
        {
            var staffComs = await _staffCommunication.GetStaffCommunication();
            return View(staffComs);
        }

        [Route("/Staff/Communication/New")]
        public async Task<IActionResult> NewCommunication()
        {
            var model = new CreateStaffCommunication();
            var staffs = await _staffService.GetStaffs();
            model.Staffs = staffs;
            return View(model);
        }
        [HttpPost]
        [Route("/Staff/NewCommunication", Name = "NewCommunication")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCommunication(CreateStaffCommunication staffCommunication)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCommunication", staffCommunication);
            }

            if (staffCommunication.FileAttachment != null)
            {
                string attachmentFolder = "staffcommunication";
                var file = await _fileUpload.UploadFile(attachmentFolder, false, string.Concat(attachmentFolder, DateTime.Now.ToString("yyyyMMddhhmmss"), "_", staffCommunication.FileAttachment.FileName), staffCommunication.FileAttachment.OpenReadStream());
                staffCommunication.Attachment = file;
            }
            var model = Mapper.Map<PostStaffCommunication>(staffCommunication);
            var result = await _staffCommunication.Post(model);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Communication Successfully saved" : "An Error Occurred" });

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Communication");
            }
            else
            {
                if (result.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                    ModelState.AddModelError("", content);
                return View("NewCommunication", model);
            }


        }

        [HttpGet]
        [Route("/Staff/Communication/Details/{id}", Name = "CommunicationDetails")]
        public async Task<IActionResult> CommunicationDetails(int id)
        {
            var details = await _staffCommunication.GetStaffCommunication(id);
            return View(details);
        }

        [HttpGet]
        [Route("[Controller]/Rota/Create", Name = "CreateRota")]
        public IActionResult CreateRota()
        {

            var model = new CreateStaffRota();
            return View(model);
        }


        [HttpPost]
        [Route("[Controller]/Rota/Create", Name = "CreateRota")]
        public IActionResult CreateRota(CreateStaffRota model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToRoute("PreviewRota", new { startDate = model.StartDate, endDate = model.StopDate });
        }

        [HttpGet]
        [Route("[Controller]/Rota/Preview", Name = "PreviewRota")]
        public async Task<IActionResult> PreviewRota(string startDate, string endDate)
        {

            PreviewStaffRota model = new PreviewStaffRota();
            model.StartDate = startDate;
            model.StopDate = endDate;
            var staffs = await _staffService.GetStaffs();
            var selections = await _staffService.GetRotaSelections();

            DateTime start = DateTime.TryParseExact(startDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-Us"), DateTimeStyles.None, out DateTime sdate) ? sdate : default;
            DateTime end = DateTime.TryParseExact(endDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-Us"), DateTimeStyles.None, out DateTime edate) ? edate : default;

            var days = end.Subtract(start).Days + 1;
            var dates = Enumerable.Range(0, days).Select(d => start.AddDays(d));

            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Selections = selections.Select(r => new SelectListItem(r.ItemName, r.StaffRotaDynamicAdditionId.ToString())).ToList();

            await SetPreviewRota(model, dates.ToList());
            return View(model);
        }

        [HttpPost]
        [Route("[Controller]/Rota/Preview", Name = "PreviewRota")]
        public async Task<IActionResult> PreviewRota(PreviewStaffRota model)
        {
            List<PostStaffRota> rotas = new List<PostStaffRota>();
            foreach (var day in model.RotaDays)
            {

                foreach (var staff in day.SelectedStaffs)
                {
                    var rota = new PostStaffRota();
                    rota.Remark = day.Remark;
                    rota.ReferenceNumber = DateTime.Now.ToString("yyyyMMddhhmmssms") + day.SelectedStaffs.IndexOf(staff);
                    rota.RotaDate = day.Date;//.ToString("MM/dd/yyyy");
                    rota.RotaId = day.RotaId;
                    rota.StaffRotaPeriods = (from rp in day.RotaTypes
                                             where rp.IsSelected
                                             select new PostStaffRotaPeriod
                                             {
                                                 ClientRotaTypeId = rp.ClientRotaTypeId
                                             }).ToList();
                    rota.Staff = staff;
                    rota.StaffRotaPartners = (from pt in day.SelectedStaffs
                                              where pt != staff
                                              select new PostStaffRotaPartner
                                              {
                                                  StaffId = pt
                                              }).ToList();

                    rota.StaffRotaItem = (from item in day.Items
                                          select new PostStaffRotaItem
                                          {
                                              StaffRotaDynamicAdditionId = item
                                          }).ToList();
                    rotas.Add(rota);
                }


            }


            var result = await _staffService.CreateRota(rotas);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Rota Successfully saved" : "An Error Occurred" });

            if (!result.IsSuccessStatusCode)
            {
                _logger.LogError(content, "PreviewRota");
                var staffs = await _staffService.GetStaffs();
                model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }


            return RedirectToAction("RotaAdmin", "Rotering", new { startDate = model.StartDate, stopDate = model.StopDate });

        }

        async Task SetPreviewRota(PreviewStaffRota model, List<DateTime> dates)
        {
            var rotaTypes = await _clientRotaTypeService.Get();

            var rotas = await _clientRotaNameService.Get();

            foreach (var date in dates)
            {
                PreviewStaffRotaDate rotaDate = new PreviewStaffRotaDate();
                rotaDate.Date = date;
                rotaDate.RotaTypes = rotaTypes.Select(r => new CreateMedicationPeriod()
                {
                    RotaType = r.RotaType,
                    ClientRotaTypeId = r.ClientRotaTypeId,
                    IsSelected = true
                }).ToList();

                rotaDate.Rotas = rotas.Select(r => new SelectListItem(r.RotaName, r.RotaId.ToString())).ToList();

                model.RotaDays.Add(rotaDate);
            }

        }


    }
}