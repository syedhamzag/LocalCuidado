using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSurvey;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
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
    public class StaffSurveyController : BaseController
    {
        private IStaffSurveyService _StaffSurveyService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffSurveyController(IStaffSurveyService StaffSurveyService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _StaffSurveyService = StaffSurveyService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSurveyService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffSurvey();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.StaffId = staffId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int StaffSurveyId)
        {
            var StaffSurvey = _StaffSurveyService.Get(StaffSurveyId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateStaffSurvey
            {
                Attachment = StaffSurvey.Result.Attachment,
                Date = StaffSurvey.Result.Date,
                Deadline = StaffSurvey.Result.Deadline,
                URL = StaffSurvey.Result.URL,
                OfficerToAct = StaffSurvey.Result.OfficerToAct,
                Remarks = StaffSurvey.Result.Remarks,
                Status = StaffSurvey.Result.Status,
                ActionRequired = StaffSurvey.Result.ActionRequired,
                NextCheckDate = StaffSurvey.Result.NextCheckDate,
                WorkTeam = StaffSurvey.Result.WorkTeam,
                StaffId = StaffSurvey.Result.StaffId,
                AccessToPolicies = StaffSurvey.Result.AccessToPolicies,
                AdequateTrainingReceived = StaffSurvey.Result.AdequateTrainingReceived,
                AreaRequiringImprovements = StaffSurvey.Result.AreaRequiringImprovements,
                CompanyManagement = StaffSurvey.Result.CompanyManagement,
                HealthCareServicesSatisfaction = StaffSurvey.Result.HealthCareServicesSatisfaction,
                SupportFromCompany = StaffSurvey.Result.SupportFromCompany,
                WorkEnvironmentSuggestions = StaffSurvey.Result.WorkEnvironmentSuggestions,
                Details = StaffSurvey.Result.Details
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffSurvey model)
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

            var postVoice = Mapper.Map<PostStaffSurvey>(model);

            var result = await _StaffSurveyService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Survey successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffSurvey model)
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
            var putComplain = Mapper.Map<PutStaffSurvey>(model);
            var entity = await _StaffSurveyService.Put(putComplain);
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
