using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.PersonalHygiene;
using AwesomeCare.Admin.ViewModels.CarePlan.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class PersonalHygieneController : BaseController
    {
        private IPersonalHygieneService _phygieneService;
        private IClientService _clientService;

        public PersonalHygieneController(IPersonalHygieneService phygieneService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _phygieneService = phygieneService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreatePersonalHygiene();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePersonalHygiene model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostPersonalHygiene post = new PostPersonalHygiene();

            post.HygieneId = model.HygieneId;
            post.ClientId = model.ClientId;
            post.Cleaning = model.Cleaning;
            post.CleaningFreq = model.CleaningFreq;
            post.CleaningTools = model.CleaningTools;
            post.DesiredCleaning = model.DesiredCleaning;
            post.DirtyLaundry = model.DirtyLaundry;
            post.DryLaundry = model.DryLaundry;
            post.GeneralAppliance = model.GeneralAppliance;
            post.Ironing = model.Ironing;
            post.LaundryGuide = model.LaundryGuide;
            post.LaundrySupport = model.LaundrySupport;
            post.WashingMachine = model.WashingMachine;
            post.WhoClean = model.WhoClean;

            var json = JsonConvert.SerializeObject(post);
            var result = await _phygieneService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Balance successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }
    }
}
