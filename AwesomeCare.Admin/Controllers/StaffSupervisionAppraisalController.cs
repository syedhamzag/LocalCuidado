using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSupervisionAppraisal;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervisionAppraisal;
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
    public class StaffSupervisionAppraisalController : BaseController
    {
        private IStaffSupervisionAppraisalService _StaffSupervisionAppraisalService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffSupervisionAppraisalController(IStaffSupervisionAppraisalService StaffSupervisionAppraisalService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffSupervisionAppraisalService = StaffSupervisionAppraisalService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSupervisionAppraisalService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffSupervisionAppraisal();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.StaffId = staffId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int StaffSupervisionAppraisalId)
        {
            var StaffSupervisionAppraisal = _StaffSupervisionAppraisalService.Get(StaffSupervisionAppraisalId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffSupervisionAppraisal
            {
                Attachment = StaffSupervisionAppraisal.Result.Attachment,
                Date = StaffSupervisionAppraisal.Result.Date,
                Deadline = StaffSupervisionAppraisal.Result.Deadline,
                URL = StaffSupervisionAppraisal.Result.URL,
                Remarks = StaffSupervisionAppraisal.Result.Remarks,
                Status = StaffSupervisionAppraisal.Result.Status,
                ActionRequired = StaffSupervisionAppraisal.Result.ActionRequired,
                NextCheckDate = StaffSupervisionAppraisal.Result.NextCheckDate,
                WorkTeam = StaffSupervisionAppraisal.Result.WorkTeam,
                OfficerToAct = StaffSupervisionAppraisal.Result.OfficerToAct,
                CondourAndWhistleBlowing = StaffSupervisionAppraisal.Result.CondourAndWhistleBlowing,
                Details = StaffSupervisionAppraisal.Result.Details,
                FiveStarRating = StaffSupervisionAppraisal.Result.FiveStarRating,
                NoAbilityToSupport = StaffSupervisionAppraisal.Result.NoAbilityToSupport,
                NoCondourAndWhistleBlowing = StaffSupervisionAppraisal.Result.NoCondourAndWhistleBlowing,
                ProfessionalDevelopment = StaffSupervisionAppraisal.Result.ProfessionalDevelopment,
                StaffAbility = StaffSupervisionAppraisal.Result.StaffAbility,
                StaffComplaints = StaffSupervisionAppraisal.Result.StaffComplaints,
                StaffDevelopment = StaffSupervisionAppraisal.Result.StaffDevelopment,
                StaffRating = StaffSupervisionAppraisal.Result.StaffRating,
                StaffSupportAreas = StaffSupervisionAppraisal.Result.StaffSupportAreas,
                StaffId = StaffSupervisionAppraisal.Result.StaffId,
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffSupervisionAppraisal model)
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

            var postVoice = Mapper.Map<PostStaffSupervisionAppraisal>(model);

            var result = await _StaffSupervisionAppraisalService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Supervision Appraisal successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffSupervisionAppraisal model)
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
            var putComplain = Mapper.Map<PutStaffSupervisionAppraisal>(model);
            var entity = await _StaffSupervisionAppraisalService.Put(putComplain);
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
