using AwesomeCare.Admin.Services.ClientMedAudit;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Buffers;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientMedAudit;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientMedAuditController : BaseController
    {
        private IClientMedAuditService _clientMedAuditService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientMedAuditController(IClientMedAuditService clientMedAuditService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _clientMedAuditService = clientMedAuditService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _clientMedAuditService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientMedAudit();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int MedAuditId)
        {
            var MedAudit = _clientMedAuditService.Get(MedAuditId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateClientMedAudit
            {
                ClientId = MedAudit.Result.ClientId,
                ActionRecommended = MedAudit.Result.ActionRecommended,
                ActionTaken = MedAudit.Result.ActionTaken,
                Attachment = MedAudit.Result.Attachment,
                Date = MedAudit.Result.Date,
                NextDueDate = MedAudit.Result.NextDueDate,
                Deadline = MedAudit.Result.Deadline,
                EvidenceOfActionTaken = MedAudit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = MedAudit.Result.LessonLearntAndShared,
                LogURL = MedAudit.Result.LogURL,
                NameOfAuditor = MedAudit.Result.NameOfAuditor,
                Observations = MedAudit.Result.Observations,
                OfficerToTakeAction = MedAudit.Result.OfficerToTakeAction,
                Remarks = MedAudit.Result.Remarks,
                RepeatOfIncident = MedAudit.Result.RepeatOfIncident,
                RotCause = MedAudit.Result.RotCause,
                Status = MedAudit.Result.Status,
                ThinkingServiceUsers = MedAudit.Result.ThinkingServiceUsers,
                GapsInAdmistration = MedAudit.Result.GapsInAdmistration,
                RightsOfMedication = MedAudit.Result.RightsOfMedication,
                MarChartReview = MedAudit.Result.MarChartReview,
                MedicationConcern = MedAudit.Result.MedicationConcern,
                HardCopyReview = MedAudit.Result.HardCopyReview,
                MedicationSupplyEfficiency = MedAudit.Result.MedicationSupplyEfficiency,
                MedicationInfoUploadEefficiency = MedAudit.Result.MedicationInfoUploadEefficiency,
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientMedAudit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                ViewBag.GetStaffs = staffNames;
                ViewBag.Staff = new SelectList(staffNames, "StaffPersonalInfoId", "FullName", model.OfficerToTakeAction);
                return View(model);
            }
            #region Evidence
            string folder = "clientcomplain";
            string filename = string.Concat(folder, "_Evidence_", model.ClientId);
            string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
            model.EvidenceOfActionTaken = path;
            #endregion
            #region Attachment
            string folderA = "clientcomplain";
            string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.Attachment = pathA;
            #endregion

            var postMedAudit = Mapper.Map<PostClientMedAudit>(model);

            var result = await _clientMedAuditService.Create(postMedAudit);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientMedAudit model)
        {
            if (!ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                ViewBag.GetStaffs = staffNames;
                return View(model);
            }
            #region Evidence
            if (model.Evidence != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Evidence_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            else
            {
                model.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
            }
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
            var putComplain = Mapper.Map<PutClientMedAudit>(model);
            var entity = await _clientMedAuditService.Put(putComplain);
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
