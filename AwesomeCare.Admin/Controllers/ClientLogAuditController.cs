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
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int logAuditId)
        {
            var logAudit = _clientlogAuditService.Get(logAuditId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateClientLogAudit
            {
                ClientId = logAudit.Result.ClientId,
                ActionRecommended = logAudit.Result.ActionRecommended,
                ActionTaken = logAudit.Result.ActionTaken,
                Attachment = logAudit.Result.Attachment,
                Date = logAudit.Result.Date,
                NextDueDate = logAudit.Result.NextDueDate,
                Deadline = logAudit.Result.Deadline,
                EvidenceOfActionTaken = logAudit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = logAudit.Result.LessonLearntAndShared,
                LogURL = logAudit.Result.LogURL,
                NameOfAuditor = logAudit.Result.NameOfAuditor,
                Observations = logAudit.Result.Observations,
                OfficerToTakeAction = logAudit.Result.OfficerToTakeAction,
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
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientLogAudit model)
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

            var postLogAudit = Mapper.Map<PostClientLogAudit>(model);

            var result = await _clientlogAuditService.Create(postLogAudit);
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
