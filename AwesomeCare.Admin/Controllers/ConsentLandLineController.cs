using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ConsentLandLine;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.Admin.ViewModels.PersonalDetail;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class ConsentLandLineController : BaseController
    {
        private IConsentLandLineService _ConsentLandLineService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public ConsentLandLineController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IConsentLandLineService ConsentLandLineService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _ConsentLandLineService = ConsentLandLineService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateConsentLandLine();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateConsentLandLine model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostConsentLandLine postlog = new PostConsentLandLine();

            postlog.PersonalDetailId = model.ClientId;
            postlog.LandlineId = model.LandlineId;
            postlog.Signature = model.Signature;
            postlog.Date = model.Date;
            postlog.LogMethod = model.LogMethod;

            var json = JsonConvert.SerializeObject(postlog);
            var result = await _ConsentLandLineService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Consent LandLine successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
