using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Personal;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.Admin.ViewModels.PersonalDetail;

namespace AwesomeCare.Admin.Controllers
{
    public class PersonalController : BaseController
    {
        private IPersonalService _PersonalService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public PersonalController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IPersonalService PersonalService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _PersonalService = PersonalService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreatePersonal();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePersonal model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostPersonal postlog = new PostPersonal();

            postlog.PersonalDetailId = model.ClientId;
            postlog.DNR = model.DNR;
            postlog.Smoking = model.Smoking;
            postlog.PersonalId = model.PersonalId;

            var result = await _PersonalService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Personal successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
