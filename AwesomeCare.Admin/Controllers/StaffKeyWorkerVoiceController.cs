using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffKeyWorkerVoice;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorkerVoice;
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
    public class StaffKeyWorkerVoiceController : BaseController
    {
        private IStaffKeyWorkerVoiceService _StaffKeyWorkerVoiceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffKeyWorkerVoiceController(IStaffKeyWorkerVoiceService StaffKeyWorkerVoiceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffKeyWorkerVoiceService = StaffKeyWorkerVoiceService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffKeyWorkerVoiceService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffKeyWorkerVoice();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetStaffs = staffNames;
            ViewBag.GetClients = clientNames;
            return View(model);

        }
        public async Task<IActionResult> Edit(int KeyWorkerId)
        {
            var KeyWorker = _StaffKeyWorkerVoiceService.Get(KeyWorkerId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffKeyWorkerVoice
            {
                Attachment = KeyWorker.Result.Attachment,
                Date = KeyWorker.Result.Date,
                Deadline = KeyWorker.Result.Deadline,
                URL = KeyWorker.Result.URL,
                Remarks = KeyWorker.Result.Remarks,
                Status = KeyWorker.Result.Status,
                ActionRequired = KeyWorker.Result.ActionRequired,
                NextCheckDate = KeyWorker.Result.NextCheckDate,
                ChangesWeNeed = KeyWorker.Result.ChangesWeNeed,
                Details = KeyWorker.Result.Details,
                NotComfortableServices = KeyWorker.Result.NotComfortableServices,
                MovingAndHandling = KeyWorker.Result.MovingAndHandling,
                OfficerToAct  = KeyWorker.Result.OfficertoAct,
                ServicesRequiresTime = KeyWorker.Result.ServicesRequiresTime,
                MedicationChanges = KeyWorker.Result.MedicationChanges,
                NutritionalChanges = KeyWorker.Result.NutritionalChanges,
                RiskAssessment = KeyWorker.Result.RiskAssessment,
                ServicesRequiresServices = KeyWorker.Result.ServicesRequiresServices,
                WellSupportedServices = KeyWorker.Result.WellSupportedServices,
                TeamYouWorkFor = KeyWorker.Result.TeamYouWorkFor,
                StaffId = KeyWorker.Result.StaffId,                
                HealthAndWellNessChanges = KeyWorker.Result.HealthAndWellNessChanges
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffKeyWorkerVoice model)
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

            var postVoice = Mapper.Map<PostStaffKeyWorkerVoice>(model);

            var result = await _StaffKeyWorkerVoiceService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Key worker Voice successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffKeyWorkerVoice model)
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
            var putComplain = Mapper.Map<PutStaffKeyWorkerVoice>(model);
            var entity = await _StaffKeyWorkerVoiceService.Put(putComplain);
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
