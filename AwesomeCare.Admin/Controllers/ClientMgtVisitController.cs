using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientMgtVisit;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientMgtVisit;
using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
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

namespace AwesomeCare.Admin.Controllers
{
    public class ClientMgtVisitController : BaseController
    {
        private IClientMgtVisitService _clientMgtVisitService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientMgtVisitController(IClientMgtVisitService clientMgtVisitService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _clientMgtVisitService = clientMgtVisitService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientMgtVisitService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientMgtVisit();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int VisitId)
        {
            var MgtVisit = _clientMgtVisitService.Get(VisitId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateClientMgtVisit
            {
                ClientId = MgtVisit.Result.ClientId,
                Attachment = MgtVisit.Result.Attachment,
                Date = MgtVisit.Result.Date,
                Deadline = MgtVisit.Result.Deadline,
                EvidenceOfActionTaken = MgtVisit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = MgtVisit.Result.LessonLearntAndShared,
                URL = MgtVisit.Result.URL,
                HowToComplain = MgtVisit.Result.HowToComplain,
                OfficerToAct = MgtVisit.Result.OfficerToAct,
                Remarks = MgtVisit.Result.Remarks,
                RotCause = MgtVisit.Result.RotCause,
                Status = MgtVisit.Result.Status,
                ActionRequired = MgtVisit.Result.ActionRequired,
                ActionsTakenByMPCC = MgtVisit.Result.ActionsTakenByMPCC,
                ImprovementExpect = MgtVisit.Result.ImprovementExpect,
                Observation = MgtVisit.Result.Observation,
                RateManagers = MgtVisit.Result.RateManagers,
                ServiceRecommended = MgtVisit.Result.ServiceRecommended,
                NextCheckDate = MgtVisit.Result.NextCheckDate,
                RateServiceRecieving = MgtVisit.Result.RateServiceRecieving,
                StaffBestSupport = MgtVisit.Result.StaffBestSupport
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientMgtVisit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                ViewBag.GetStaffs = staffNames;
                ViewBag.Staff = new SelectList(staffNames, "StaffPersonalInfoId", "FullName", model.OfficerToAct);
                return View(model);
            }
            #region Evidence
            string folder = "clientcomplain";
            string filename = string.Concat(folder, "_Evidence_", model.ClientId, model.NextCheckDate.TimeOfDay);
            string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
            model.EvidenceOfActionTaken = path;
            #endregion
            #region Attachment
            string folderA = "clientcomplain";
            string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId, model.NextCheckDate.TimeOfDay);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.Attachment = pathA;
            #endregion

            var postMgtVisit = Mapper.Map<PostClientMgtVisit>(model);

            var result = await _clientMgtVisitService.Create(postMgtVisit);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientMgtVisit model)
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
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
            var putComplain = Mapper.Map<PutClientMgtVisit>(model);
            var entity = await _clientMgtVisitService.Put(putComplain);
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
