using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientServiceWatch;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientServiceWatch;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
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
    public class ClientServiceWatchController : BaseController
    {
        private IClientServiceWatchService _clientServiceWatchService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientServiceWatchController(IClientServiceWatchService clientServiceWatchService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _clientServiceWatchService = clientServiceWatchService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientServiceWatchService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientServiceWatch();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int WatchId)
        {
            var ServiceWatch = _clientServiceWatchService.Get(WatchId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateClientServiceWatch
            {
                ClientId = ServiceWatch.Result.ClientId,
                Attachment = ServiceWatch.Result.Attachment,
                Date = ServiceWatch.Result.Date,
                NextCheckDate = ServiceWatch.Result.NextCheckDate,
                Deadline = ServiceWatch.Result.Deadline,
                Contact = ServiceWatch.Result.Contact,
                Details = ServiceWatch.Result.Details,
                URL = ServiceWatch.Result.URL,
                Incident = ServiceWatch.Result.Incident,
                OfficerToAct = ServiceWatch.Result.OfficerToAct,
                Remarks = ServiceWatch.Result.Remarks,
                Observation = ServiceWatch.Result.Observation,
                Status = ServiceWatch.Result.Status,
                ActionRequired = ServiceWatch.Result.ActionRequired,
                PersonInvolved = ServiceWatch.Result.PersonInvolved
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientServiceWatch model)
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
            string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.Attachment = pathA;
            #endregion

            var postServiceWatch = Mapper.Map<PostClientServiceWatch>(model);

            var result = await _clientServiceWatchService.Create(postServiceWatch);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientServiceWatch model)
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
            var putComplain = Mapper.Map<PutClientServiceWatch>(model);
            var entity = await _clientServiceWatchService.Put(putComplain);
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
