using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffOneToOne;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
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
    public class StaffOneToOneController : BaseController
    {
        private IStaffOneToOneService _StaffOneToOneService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffOneToOneController(IStaffOneToOneService StaffOneToOneService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffOneToOneService = StaffOneToOneService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffOneToOneService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffOneToOne();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.StaffId = staffId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int OneToOneId)
        {
            var OneToOne = _StaffOneToOneService.Get(OneToOneId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffOneToOne
            {
                Attachment = OneToOne.Result.Attachment,
                Date = OneToOne.Result.Date,
                Deadline = OneToOne.Result.Deadline,
                URL = OneToOne.Result.URL,
                OfficerToAct = OneToOne.Result.OfficerToAct,
                Remarks = OneToOne.Result.Remarks,
                Status = OneToOne.Result.Status,
                ActionRequired = OneToOne.Result.ActionRequired,
                CurrentEventArea = OneToOne.Result.CurrentEventArea,
                DecisionsReached = OneToOne.Result.DecisionsReached,
                ImprovementRecorded = OneToOne.Result.ImprovementRecorded,
                StaffImprovedInAreas = OneToOne.Result.StaffImprovedInAreas,
                StaffId = OneToOne.Result.StaffId,
                StaffConclusion = OneToOne.Result.StaffConclusion,
                Purpose = OneToOne.Result.Purpose,
                PreviousSupervision = OneToOne.Result.PreviousSupervision,
                NextCheckDate = OneToOne.Result.NextCheckDate
                
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffOneToOne model)
        {
            if (model == null || !ModelState.IsValid)
            {
                List<GetStaffs> staffNames = await _staffService.GetStaffs();
                ViewBag.GetStaffs = staffNames;
                return View(model);
            }
            #region Attachment
            string folderA = "clientcomplain";
            string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.Attachment = pathA;
            #endregion

            var postVoice = Mapper.Map<PostStaffOneToOne>(model);

            var result = await _StaffOneToOneService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New One to One successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffOneToOne model)
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
            var putComplain = Mapper.Map<PutStaffOneToOne>(model);
            var entity = await _StaffOneToOneService.Put(putComplain);
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
