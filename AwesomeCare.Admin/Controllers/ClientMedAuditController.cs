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

        public async Task<IActionResult> Report()
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
            var MedAudit = _clientMedAuditService.Get(clientId.Value);
            if (MedAudit.Result != null)
            {
                model.MedAuditId = MedAudit.Result.MedAuditId;
                model.ActionRecommended = MedAudit.Result.ActionRecommended;
                model.ActionTaken = MedAudit.Result.ActionTaken;
                model.Attachment = MedAudit.Result.Attachment;
                model.Date = MedAudit.Result.Date;
                model.NextDueDate = MedAudit.Result.NextDueDate;
                model.Deadline = MedAudit.Result.Deadline;
                model.EvidenceOfActionTaken = MedAudit.Result.EvidenceOfActionTaken;
                model.LessonLearntAndShared = MedAudit.Result.LessonLearntAndShared;
                model.LogURL = MedAudit.Result.LogURL;
                model.NameOfAuditor = MedAudit.Result.NameOfAuditor;
                model.Observations = MedAudit.Result.Observations;
                model.OfficerToTakeAction = MedAudit.Result.OfficerToTakeAction;
                model.Remarks = MedAudit.Result.Remarks;
                model.RepeatOfIncident = MedAudit.Result.RepeatOfIncident;
                model.RotCause = MedAudit.Result.RotCause;
                model.Status = MedAudit.Result.Status;
                model.ThinkingServiceUsers = MedAudit.Result.ThinkingServiceUsers;
                model.GapsInAdmistration = MedAudit.Result.GapsInAdmistration;
                model.RightsOfMedication = MedAudit.Result.RightsOfMedication;
                model.MarChartReview = MedAudit.Result.MarChartReview;
                model.MedicationConcern = MedAudit.Result.MedicationConcern;
                model.HardCopyReview = MedAudit.Result.HardCopyReview;
                model.MedicationSupplyEfficiency = MedAudit.Result.MedicationSupplyEfficiency;
                model.MedicationInfoUploadEefficiency = MedAudit.Result.MedicationInfoUploadEefficiency;
                model.ActionName = "Update";  
            }
            return View(model);

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
            var MedAudit_ = await _clientMedAuditService.Get(model.ClientId);
            if (MedAudit_ == null)
            {
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

            }
            else
            {
                model.EvidenceOfActionTaken = MedAudit_.EvidenceOfActionTaken;
                model.Attachment = MedAudit_.Attachment;
            }

            var postMedAudit = Mapper.Map<PostClientMedAudit>(model);

            var result = await _clientMedAuditService.Create(postMedAudit);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("Index", new { clientId = model.ClientId });
            
        }
    }
}
