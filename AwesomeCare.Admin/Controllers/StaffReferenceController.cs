using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffReference;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffReference;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Client;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffReferenceController : BaseController
    {
        private IStaffReferenceService _StaffReferenceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffReferenceController(IStaffReferenceService StaffReferenceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffReferenceService = StaffReferenceService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffReferenceService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffReference();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetStaffs = staffNames;
            ViewBag.GetClients = clientNames;
            return View(model);

        }
        public async Task<IActionResult> Edit(int ReferenceId)
        {
            var StaffReference = _StaffReferenceService.Get(ReferenceId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetStaffs = staffNames;
            ViewBag.GetClients = clientNames;
            var putEntity = new CreateStaffReference
            {
                Address = StaffReference.Result.Address,
                ApplicantRole = StaffReference.Result.ApplicantRole,
                Caring = StaffReference.Result.Caring,
                ConfirmedBy = StaffReference.Result.ConfirmedBy,
                Contact = StaffReference.Result.Contact,
                DateofEmployement = StaffReference.Result.DateofEmployement,
                DateofExit = StaffReference.Result.DateofExit,
                RehireStaff = StaffReference.Result.RehireStaff,
                RefreeName = StaffReference.Result.RefreeName,
                Relationship = StaffReference.Result.Relationship,
                StaffId = StaffReference.Result.StaffId,
                WorkUnderPressure = StaffReference.Result.WorkUnderPressure,
                TeamWork = StaffReference.Result.TeamWork,
                Email = StaffReference.Result.Email,
                PreviousExperience = StaffReference.Result.PreviousExperience,
                Knowledgeable = StaffReference.Result.Knowledgeable,
                Integrity = StaffReference.Result.Integrity,
                Date = StaffReference.Result.Date,
                Status = StaffReference.Result.Status,
                Attachment = StaffReference.Result.Attach
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffReference model)
        {
            if (model == null || !ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetStaffs = staffNames;
                ViewBag.GetClients = clientNames;
                return View(model);
            }
            #region Attachment
            string folderA = "clientcomplain";
            string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.Attachment = pathA;
            #endregion

            var postVoice = Mapper.Map<PostStaffReference>(model);

            var result = await _StaffReferenceService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Reference successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffReference model)
        {
            if (!ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                ViewBag.GetStaffs = staffNames;
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
            var putComplain = Mapper.Map<PutStaffReference>(model);
            var entity = await _StaffReferenceService.Put(putComplain);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity != null,
                Message = entity != null ? "Successful" : "Operation failed"
            });
            if (entity != null)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
