using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientProgram;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
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
    public class ClientProgramController : BaseController
    {
        private IClientProgramService _clientProgramService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientProgramController(IClientProgramService clientProgramService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _clientProgramService = clientProgramService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientProgramService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientProgram();
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            return View(model);

        }
        public async Task<IActionResult> Edit(int ProgramId)
        {
            var Program = _clientProgramService.Get(ProgramId);
            List<GetStaffs> staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var putEntity = new CreateClientProgram
            {
                ClientId = Program.Result.ClientId,
                Attachment = Program.Result.Attachment,
                Date = Program.Result.Date,
                NextCheckDate = Program.Result.NextCheckDate,
                Deadline = Program.Result.Deadline,
                ProgramOfChoice = Program.Result.ProgramOfChoice,
                URL = Program.Result.URL,
                DaysOfChoice = Program.Result.DaysOfChoice,
                OfficerToAct = Program.Result.OfficerToAct,
                Remarks = Program.Result.Remarks,
                DetailsOfProgram = Program.Result.DetailsOfProgram,
                Status = Program.Result.Status,
                ActionRequired = Program.Result.ActionRequired,
                Observation = Program.Result.Observation,
                PlaceLocationProgram = Program.Result.PlaceLocationProgram
                
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientProgram model)
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

            var postProgram = Mapper.Map<PostClientProgram>(model);

            var result = await _clientProgramService.Create(postProgram);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientProgram model)
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
            var putComplain = Mapper.Map<PutClientProgram>(model);
            var entity = await _clientProgramService.Put(putComplain);
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
