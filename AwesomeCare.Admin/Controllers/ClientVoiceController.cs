using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientVoice;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientVoice;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
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
    public class ClientVoiceController : BaseController
    {
        private IClientVoiceService _clientVoiceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientVoiceController(IClientVoiceService clientVoiceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _clientVoiceService = clientVoiceService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientVoiceService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientVoice();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int VoiceId)
        {
            var Voice = _clientVoiceService.Get(VoiceId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateClientVoice
            {
                ClientId = Voice.Result.ClientId,
                Attachment = Voice.Result.Attachment,
                Date = Voice.Result.Date,
                Deadline = Voice.Result.Deadline,
                EvidenceOfActionTaken = Voice.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = Voice.Result.LessonLearntAndShared,
                URL = Voice.Result.URL,
                NameOfCaller = Voice.Result.NameOfCaller,
                OfficerToAct = Voice.Result.OfficerToAct,
                Remarks = Voice.Result.Remarks,
                RotCause = Voice.Result.RotCause,
                Status = Voice.Result.Status,
                ActionRequired = Voice.Result.ActionRequired,
                ActionsTakenByMPCC = Voice.Result.ActionsTakenByMPCC,
                AreasOfImprovements = Voice.Result.AreasOfImprovements,
                HealthGoalLongTerm = Voice.Result.HealthGoalLongTerm,
                HealthGoalShortTerm = Voice.Result.HealthGoalShortTerm,
                InterestedInPrograms = Voice.Result.InterestedInPrograms,
                NextCheckDate = Voice.Result.NextCheckDate,
                OfficeStaffSupport = Voice.Result.OfficeStaffSupport,
                RateServiceRecieving = Voice.Result.RateServiceRecieving,
                RateStaffAttending = Voice.Result.RateStaffAttending,
                SomethingSpecial = Voice.Result.SomethingSpecial,
                StaffBestSupport = Voice.Result.StaffBestSupport,
                StaffPoorSupport = Voice.Result.StaffPoorSupport
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientVoice model)
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

            var postVoice = Mapper.Map<PostClientVoice>(model);

            var result = await _clientVoiceService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientVoice model)
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
            var putComplain = Mapper.Map<PutClientVoice>(model);
            var entity = await _clientVoiceService.Put(putComplain);
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
