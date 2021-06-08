using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffAdlObs;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
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
    public class StaffAdlObsController : BaseController
    {
        private IStaffAdlObsService _StaffAdlObsService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffAdlObsController(IStaffAdlObsService StaffAdlObsService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffAdlObsService = StaffAdlObsService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffAdlObsService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffAdlObs();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetStaffs = staffNames;
            ViewBag.GetClients = clientNames;
            model.StaffId = staffId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int ObserverId)
        {
            var Observer = _StaffAdlObsService.Get(ObserverId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffAdlObs
            {
                ClientId = Observer.Result.ClientId,
                Attachment = Observer.Result.Attachment,
                Date = Observer.Result.Date,
                Deadline = Observer.Result.Deadline,
                URL = Observer.Result.URL,
                OfficerToAct = Observer.Result.OfficerToAct,
                Remarks = Observer.Result.Remarks,
                Status = Observer.Result.Status,
                ActionRequired = Observer.Result.ActionRequired,
                Details = Observer.Result.Details,
                UnderstandingofControl = Observer.Result.UnderstandingofControl,
                UnderstandingofEquipment = Observer.Result.UnderstandingofEquipment,
                StaffId = Observer.Result.StaffId,
                UnderstandingofService = Observer.Result.UnderstandingofService,
                NextCheckDate = Observer.Result.NextCheckDate,
                FivePrinciples = Observer.Result.FivePrinciples,
                Comments = Observer.Result.Comments
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffAdlObs model)
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

            var postVoice = Mapper.Map<PostStaffAdlObs>(model);

            var result = await _StaffAdlObsService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Adl Observation successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffAdlObs model)
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
            var putComplain = Mapper.Map<PutStaffAdlObs>(model);
            var entity = await _StaffAdlObsService.Put(putComplain);
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
