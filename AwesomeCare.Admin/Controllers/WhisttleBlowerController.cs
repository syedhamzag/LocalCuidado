using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.WhisttleBlower;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.WhisttleBlower;
using AwesomeCare.DataTransferObject.DTOs.WhisttleBlower;
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
    public class WhisttleBlowerController : BaseController
    {
        private IWhisttleBlowerService _WhisttleBlowerService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public WhisttleBlowerController(IWhisttleBlowerService WhisttleBlowerService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _WhisttleBlowerService = WhisttleBlowerService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _WhisttleBlowerService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateWhisttleBlower();
            var clientNames = await _clientService.GetClients();
            model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int WhisttleBlowerId)
        {
            var WhisttleBlower = _WhisttleBlowerService.Get(WhisttleBlowerId);
            var clientNames = await _clientService.GetClients();
            var putEntity = new CreateWhisttleBlower
            {
                WhisttleBlowerId = WhisttleBlower.Result.WhisttleBlowerId,
                Date = WhisttleBlower.Result.Date,
                UserName = WhisttleBlower.Result.UserName,
                StaffName = WhisttleBlower.Result.StaffName,
                IncidentDate = WhisttleBlower.Result.IncidentDate,
                Happening = WhisttleBlower.Result.Happening,
                Evidence = WhisttleBlower.Result.Evidence,
                Witness = WhisttleBlower.Result.Witness,
                LikeCalling = WhisttleBlower.Result.LikeCalling,
                Status = WhisttleBlower.Result.Status,
                ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateWhisttleBlower model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var clientNames = await _clientService.GetClients();
                model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.UserName);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream(),model.Attach.ContentType);
                model.Evidence = pathA;
            }
            else
            {
                model.Evidence = "No Image";
            }
            
            #endregion

            var postVoice = Mapper.Map<PostWhisttleBlower>(model);

            var result = await _WhisttleBlowerService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Spot Check successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateWhisttleBlower model)
        {
            if (!ModelState.IsValid)
            {
                var clientNames = await _clientService.GetClients();
                model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.UserName);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream(), model.Attach.ContentType);
                model.Evidence = pathA;

            }
            else
            {
                model.Evidence = model.Evidence;
            }
            #endregion
            var putComplain = Mapper.Map<PutWhisttleBlower>(model);
            var entity = await _WhisttleBlowerService.Put(putComplain);
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
