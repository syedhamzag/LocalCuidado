using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Resources;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Resources;
using AwesomeCare.DataTransferObject.DTOs.Resources;
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
    public class ResourcesController : BaseController
    {
        private IResourcesService _ResourcesService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ResourcesController(IResourcesService ResourcesService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _ResourcesService = ResourcesService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _ResourcesService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateResources();
            var clientNames = await _clientService.GetClients();
            model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int ResourcesId)
        {
            var Resources = _ResourcesService.Get(ResourcesId);
            var clientNames = await _clientService.GetClients();
            
            var putEntity = new CreateResources
            {
                ResourcesId = Resources.Result.ResourcesId,
                Date = Resources.Result.Date,
                PublishTo = Resources.Result.PublishTo,
                Heading = Resources.Result.Heading,
                PublishBy = Resources.Result.PublishBy,
                Note = Resources.Result.Note,
                Video = Resources.Result.Video,
                Image = Resources.Result.Image,
                ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateResources model)
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
                string filenameA = string.Concat(folderA, "_Attachment_", model.Date.ToString());
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Image = pathA;
            }
            else
            {
                model.Image = "No Image";
            }
            var postVoice = Mapper.Map<PostResources>(model);

            var result = await _ResourcesService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Resources successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateResources model)
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
                string filenameA = string.Concat(folderA, "_Attachment_", model.Date.ToString());
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Image = pathA;

            }
            else
            {
                model.Image = model.Image;
            }
            #endregion
            var putComplain = Mapper.Map<PutResources>(model);
            var entity = await _ResourcesService.Put(putComplain);
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
