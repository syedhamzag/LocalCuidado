using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.InfectionControl;
using AwesomeCare.Admin.ViewModels.CarePlan.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.InfectionControl;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class InfectionControlController : BaseController
    {
        private IInfectionControlService _infectionService;
        private IClientService _clientService;

        public InfectionControlController(IInfectionControlService infectionService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _infectionService = infectionService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateInfectionControl();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateInfectionControl model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }

            PostInfectionControl infection = new PostInfectionControl();

            infection.ClientId = model.ClientId;
            infection.Guideline = model.Guideline;
            infection.InfectionId = model.InfectionId;
            infection.Remarks = model.Remarks;
            infection.Status = model.Status;
            infection.TestDate = model.TestDate;
            infection.Type = model.Type;
            infection.VaccStatus = model.VaccStatus;

            var json = JsonConvert.SerializeObject(infection);
            var result = await _infectionService.Create(infection);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Infection Control successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }
    }
}
