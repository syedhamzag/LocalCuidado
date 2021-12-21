using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Enotice;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Enotice;
using AwesomeCare.DataTransferObject.DTOs.Enotice;
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
    public class EnoticeController : BaseController
    {
        private IEnoticeService _EnoticeService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public EnoticeController(IEnoticeService EnoticeService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _EnoticeService = EnoticeService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _EnoticeService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateEnotice();
            var clientNames = await _clientService.GetClients();
            model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int EnoticeId)
        {
            var Enotice = _EnoticeService.Get(EnoticeId);
            var clientNames = await _clientService.GetClients();
            var putEntity = new CreateEnotice
            {
                EnoticeId = Enotice.Result.EnoticeId,
                Date = Enotice.Result.Date,
                PublishTo = Enotice.Result.PublishTo,
                Heading = Enotice.Result.Heading,
                PublishBy = Enotice.Result.PublishBy,
                Note = Enotice.Result.Note,
                Video = Enotice.Result.Video,
                Image = Enotice.Result.Image,
                ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateEnotice model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var clientNames = await _clientService.GetClients();
                model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
                return View(model);
            }
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.Date);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Image = pathA;
            }
            else
            {
                model.Image = "No Image";
            }


            var postVoice = Mapper.Map<PostEnotice>(model);

            var result = await _EnoticeService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Enotice successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEnotice model)
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
                string filenameA = string.Concat(folderA, "_Attachment_", model.Date);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Image = pathA;

            }
            else
            {
                model.Image = model.Image;
            }
            #endregion
            var putComplain = Mapper.Map<PutEnotice>(model);
            var entity = await _EnoticeService.Put(putComplain);
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
