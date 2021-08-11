using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.KeyIndicators;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.Admin.ViewModels.PersonalDetail;

namespace AwesomeCare.Admin.Controllers
{
    public class KeyIndicatorsController : BaseController
    {
        private IKeyIndicatorsService _KeyIndicatorsService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public KeyIndicatorsController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IKeyIndicatorsService KeyIndicatorsService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _KeyIndicatorsService = KeyIndicatorsService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateKeyIndicators();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateKeyIndicators model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostKeyIndicators postlog = new PostKeyIndicators();

            postlog.PersonalDetailId = model.ClientId;
            postlog.AboutMe = model.AboutMe;
            postlog.KeyId = model.KeyId;
            postlog.FamilyRole = model.FamilyRole;
            postlog.Debture = model.Debture;
            postlog.LivingStatus = model.LivingStatus;
            postlog.LogMethod = model.LogMethod;
            postlog.ThingsILike = model.ThingsILike;

            var result = await _KeyIndicatorsService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New KeyIndicators successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
