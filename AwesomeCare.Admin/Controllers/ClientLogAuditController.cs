using AwesomeCare.Admin.Services.ClientLogAudit;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
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
using AwesomeCare.Admin.ViewModels.ClientLogAudit;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientLogAuditController : BaseController
    {
        private IClientLogAuditService _clientlogAuditService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientLogAuditController(IClientLogAuditService clientlogAuditService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _clientlogAuditService = clientlogAuditService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientlogAuditService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientLogAudit();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int logAuditId)
        {
            var logAudit = _clientlogAuditService.Get(logAuditId);
            var staffs = await _staffService.GetStaffs();
            List<int> officer = new List<int>();
            officer.Add(logAudit.Result.OfficerToTakeAction);
            var putEntity = new CreateClientLogAudit
            {
                ClientId = logAudit.Result.ClientId,
                ActionRecommended = logAudit.Result.ActionRecommended,
                ActionTaken = logAudit.Result.ActionTaken,
                EvidenceFilePath = logAudit.Result.EvidenceFilePath,
                Date = logAudit.Result.Date,
                NextDueDate = logAudit.Result.NextDueDate,
                Deadline = logAudit.Result.Deadline,
                EvidenceOfActionTaken = logAudit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = logAudit.Result.LessonLearntAndShared,
                LogURL = logAudit.Result.LogURL,
                NameOfAuditor = logAudit.Result.NameOfAuditor,
                Observations = logAudit.Result.Observations,
                OfficerToTakeAction = officer,
                Remarks = logAudit.Result.Remarks,
                RepeatOfIncident = logAudit.Result.RepeatOfIncident,
                RotCause = logAudit.Result.RotCause,
                Status = logAudit.Result.Status,
                ThinkingServiceUsers = logAudit.Result.ThinkingServiceUsers,
                Communication = logAudit.Result.Communication,
                ImproperDocumentation = logAudit.Result.ImproperDocumentation,
                IsCareDifference = logAudit.Result.IsCareDifference,
                IsCareExpected = logAudit.Result.IsCareExpected,
                ProperDocumentation = logAudit.Result.ProperDocumentation,
                ThinkingStaff = logAudit.Result.ThinkingStaff,
                ThinkingStaffStop = logAudit.Result.ThinkingStaffStop,
                OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientLogAudit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            List<PostClientLogAudit> postlogs = new List<PostClientLogAudit>();
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
                model.EvidenceFilePath = pathA;
            #endregion
            foreach (var officer in model.OfficerToTakeAction)
            {
                var postlog = new PostClientLogAudit();
                postlog.ActionRecommended = model.ActionRecommended;
                postlog.ClientId = model.ClientId;
                postlog.ActionRecommended = model.ActionRecommended;
                postlog.ActionTaken = model.ActionTaken;
                postlog.EvidenceFilePath = model.EvidenceFilePath;
                postlog.Date = model.Date;
                postlog.NextDueDate = model.NextDueDate;
                postlog.Deadline = model.Deadline;
                postlog.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                postlog.LessonLearntAndShared = model.LessonLearntAndShared;
                postlog.LogURL = model.LogURL;
                postlog.NameOfAuditor = model.NameOfAuditor;
                postlog.Observations = model.Observations;
                postlog.OfficerToTakeAction = officer;
                postlog.Remarks = model.Remarks;
                postlog.RepeatOfIncident = model.RepeatOfIncident;
                postlog.RotCause = model.RotCause;
                postlog.Status = model.Status;
                postlog.ThinkingServiceUsers = model.ThinkingServiceUsers;
                postlog.Communication = model.Communication;
                postlog.ImproperDocumentation = model.ImproperDocumentation;
                postlog.IsCareDifference = model.IsCareDifference;
                postlog.IsCareExpected = model.IsCareExpected;
                postlog.ProperDocumentation = model.ProperDocumentation;
                postlog.ThinkingStaff = model.ThinkingStaff;
                postlog.ThinkingStaffStop = model.ThinkingStaffStop;

                postlogs.Add(postlog);

            }
            var result = await _clientlogAuditService.Create(postlogs);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientLogAudit model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
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
                model.EvidenceFilePath = pathA;

            }
            else
            {
                model.EvidenceFilePath = model.EvidenceFilePath;
            }
            #endregion
            var putComplain = Mapper.Map<PutClientLogAudit>(model);
            var entity = await _clientlogAuditService.Put(putComplain);
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
