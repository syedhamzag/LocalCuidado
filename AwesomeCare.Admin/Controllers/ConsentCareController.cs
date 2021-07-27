using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ConsentCare;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.Admin.ViewModels.PersonalDetail;

namespace AwesomeCare.Admin.Controllers
{
    public class ConsentCareController : BaseController
    {
        private IConsentCareService _ConsentCareService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public ConsentCareController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IConsentCareService ConsentCareService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _ConsentCareService = ConsentCareService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateConsentCare();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateConsentCare model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostConsentCare postlog = new PostConsentCare();

            postlog.ClientId = model.ClientId;
            postlog.CareId = model.CareId;
            postlog.Signature = model.Signature;
            postlog.Date = model.Date; 

            var result = await _ConsentCareService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Consent Care successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
