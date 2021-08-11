using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ConsentData;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.Admin.ViewModels.PersonalDetail;

namespace AwesomeCare.Admin.Controllers
{
    public class ConsentDataController : BaseController
    {
        private IConsentDataService _ConsentDataService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public ConsentDataController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IConsentDataService ConsentDataService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _ConsentDataService = ConsentDataService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateConsentData();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateConsentData model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostConsentData postlog = new PostConsentData();

            postlog.PersonalDetailId = model.ClientId;
            postlog.DataId = model.DataId;
            postlog.Signature = model.Signature;
            postlog.Date = model.Date;

            var result = await _ConsentDataService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Consent Data successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
