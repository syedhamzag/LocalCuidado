using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffMedComp;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
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
    public class StaffMedCompController : BaseController
    {
        private IStaffMedCompService _StaffMedCompService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffMedCompController(IStaffMedCompService StaffMedCompService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffMedCompService = StaffMedCompService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffMedCompService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffMedComp();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetStaffs = staffNames;
            ViewBag.GetClients = clientNames;
            return View(model);

        }
        public async Task<IActionResult> Edit(int MedCompId)
        {
            var MedComp = _StaffMedCompService.Get(MedCompId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffMedComp
            {
                ClientId = MedComp.Result.ClientId,
                Attachment = MedComp.Result.Attachment,
                Date = MedComp.Result.Date,
                Deadline = MedComp.Result.Deadline,
                URL = MedComp.Result.URL,
                OfficerToAct = MedComp.Result.OfficerToAct,
                Remarks = MedComp.Result.Remarks,
                Status = MedComp.Result.Status,
                ActionRequired = MedComp.Result.ActionRequired,
                NextCheckDate = MedComp.Result.NextCheckDate,
                StaffId = MedComp.Result.StaffId,
                RateStaff = MedComp.Result.StaffId,
                UnderstandingofMedication = MedComp.Result.UnderstandingofMedication,
                UnderstandingofRights = MedComp.Result.UnderstandingofRights,
                ReadingMedicalPrescriptions = MedComp.Result.ReadingMedicalPrescriptions,
                Details = MedComp.Result.Details,
                CarePlan = MedComp.Result.CarePlan

            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffMedComp model)
        {
            if (model == null || !ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                ViewBag.GetStaffs = staffNames;
                ViewBag.Staff = new SelectList(staffNames, "StaffPersonalInfoId", "FullName", model.OfficerToAct);
                return View(model);
            }
            #region Attachment
            string folderA = "clientcomplain";
            string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.Attachment = pathA;
            #endregion

            var postVoice = Mapper.Map<PostStaffMedComp>(model);

            var result = await _StaffMedCompService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Med Comp Observation successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffMedComp model)
        {
            if (!ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetStaffs = staffNames;
                ViewBag.GetClients = clientNames;
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
            var putComplain = Mapper.Map<PutStaffMedComp>(model);
            var entity = await _StaffMedCompService.Put(putComplain);
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
