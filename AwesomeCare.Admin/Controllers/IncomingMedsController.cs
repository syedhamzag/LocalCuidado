using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.IncomingMeds;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.IncomingMeds;
using AwesomeCare.DataTransferObject.DTOs.IncomingMeds;
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
    public class IncomingMedsController : BaseController
    {
        private IIncomingMedsService _IncomingMedsService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public IncomingMedsController(IIncomingMedsService IncomingMedsService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _IncomingMedsService = IncomingMedsService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _IncomingMedsService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateIncomingMeds();
            var clientNames = await _clientService.GetClients();
            model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> Edit(int IncomingMedsId)
        {
            var IncomingMeds = _IncomingMedsService.Get(IncomingMedsId);
            var clientNames = await _clientService.GetClients();
            var putEntity = new CreateIncomingMeds
            {
                IncomingMedsId = IncomingMeds.Result.IncomingMedsId,
                Date = IncomingMeds.Result.Date,
                UserName = IncomingMeds.Result.UserName,
                StaffName = IncomingMeds.Result.StaffName,
                StartDate = IncomingMeds.Result.StartDate,
                ChartImage = IncomingMeds.Result.ChartImage,
                MedsImage = IncomingMeds.Result.MedsImage,
                Status = IncomingMeds.Result.Status,
                ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateIncomingMeds model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var clientNames = await _clientService.GetClients();
                model.ClientList = clientNames.Select(s => new SelectListItem(s.Firstname, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null) //If model is null post image
            { 
            string folderA = "clientcomplain";
            string filenameA = string.Concat(folderA, "_Attachment_", model.UserName);
            string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
            model.MedsImage = pathA;
            }
            else
            {
                model.MedsImage = "No Image";
            }
            #endregion

            var postVoice = Mapper.Map<PostIncomingMeds>(model);

            var result = await _IncomingMedsService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Incoming Meds successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateIncomingMeds model)
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
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.MedsImage = pathA;

            }
            else
            {
                model.MedsImage = model.MedsImage;
            }
            #endregion
            var putComplain = Mapper.Map<PutIncomingMeds>(model);
            var entity = await _IncomingMedsService.Put(putComplain);
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
