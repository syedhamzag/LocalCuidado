using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSpotCheck;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
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
    public class StaffSpotCheckController : BaseController
    {
        private IStaffSpotCheckService _StaffSpotCheckService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffSpotCheckController(IStaffSpotCheckService StaffSpotCheckService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffSpotCheckService = StaffSpotCheckService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSpotCheckService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffSpotCheck();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetStaffs = staffNames;
            ViewBag.GetClients = clientNames;
            model.StaffId = staffId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int SpotCheckId)
        {
            var SpotCheck = _StaffSpotCheckService.Get(SpotCheckId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffSpotCheck
            {
                ClientId = SpotCheck.Result.ClientId,
                Attachment = SpotCheck.Result.Attachment,
                Date = SpotCheck.Result.Date,
                Deadline = SpotCheck.Result.Deadline,
                URL = SpotCheck.Result.URL,
                OfficerToAct = SpotCheck.Result.OfficerToAct,
                Remarks = SpotCheck.Result.Remarks,
                Status = SpotCheck.Result.Status,
                ActionRequired = SpotCheck.Result.ActionRequired,
                NextCheckDate = SpotCheck.Result.NextCheckDate,
                AreaComments = SpotCheck.Result.AreaComments,
                StaffDressCode = SpotCheck.Result.StaffDressCode,
                StaffArriveOnTime = SpotCheck.Result.StaffArriveOnTime,
                StaffId = SpotCheck.Result.StaffId,
                Details = SpotCheck.Result.Details,  
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffSpotCheck model)
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

            var postVoice = Mapper.Map<PostStaffSpotCheck>(model);

            var result = await _StaffSpotCheckService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Spot Check successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffSpotCheck model)
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
            var putComplain = Mapper.Map<PutStaffSpotCheck>(model);
            var entity = await _StaffSpotCheckService.Put(putComplain);
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
