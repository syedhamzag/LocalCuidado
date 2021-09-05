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

        public async Task<IActionResult> Reports()
        {
            var entities = await _phygieneService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreatePersonalHygiene> reports = new List<CreatePersonalHygiene>();
            foreach (GetPersonalHygiene item in entities)
            {
                var report = new CreatePersonalHygiene();
                report.HygieneId = item.HygieneId;
                report.ClientId = item.ClientId;
                report.Cleaning = item.Cleaning;
                report.CleaningFreq = item.CleaningFreq;
                report.CleaningTools = item.CleaningTools;
                report.DesiredCleaning = item.DesiredCleaning;
                report.DirtyLaundry = item.DirtyLaundry;
                report.DryLaundry = item.DryLaundry;
                report.GeneralAppliance = item.GeneralAppliance;
                report.Ironing = item.Ironing;
                report.LaundryGuide = item.LaundryGuide;
                report.LaundrySupport = item.LaundrySupport;
                report.WashingMachine = item.WashingMachine;
                report.WhoClean = item.WhoClean;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
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

        public async Task<IActionResult> Edit(int hygieneId)
        {
            var putEntity = await GetHygiene(hygieneId);
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePersonalHygiene model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutPersonalHygiene put = new PutPersonalHygiene();

            put.HygieneId = model.HygieneId;
            put.ClientId = model.ClientId;
            put.Cleaning = model.Cleaning;
            put.CleaningFreq = model.CleaningFreq;
            put.CleaningTools = model.CleaningTools;
            put.DesiredCleaning = model.DesiredCleaning;
            put.DirtyLaundry = model.DirtyLaundry;
            put.DryLaundry = model.DryLaundry;
            put.GeneralAppliance = model.GeneralAppliance;
            put.Ironing = model.Ironing;
            put.LaundryGuide = model.LaundryGuide;
            put.LaundrySupport = model.LaundrySupport;
            put.WashingMachine = model.WashingMachine;
            put.WhoClean = model.WhoClean;

            var entity = await _phygieneService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }

        public async Task<IActionResult> View(int hygieneId)
        {
            var putEntity = await GetHygiene(hygieneId);
            return View(putEntity);
        }

        public async Task<CreatePersonalHygiene> GetHygiene(int hygieneId)
        {
            var hygiene = await _phygieneService.Get(hygieneId);
            var putEntity = new CreatePersonalHygiene
            {
            HygieneId = hygiene.HygieneId,
            ClientId = hygiene.ClientId,
            Cleaning = hygiene.Cleaning,
            CleaningFreq = hygiene.CleaningFreq,
            CleaningTools = hygiene.CleaningTools,
            DesiredCleaning = hygiene.DesiredCleaning,
            DirtyLaundry = hygiene.DirtyLaundry,
            DryLaundry = hygiene.DryLaundry,
            GeneralAppliance = hygiene.GeneralAppliance,
            Ironing = hygiene.Ironing,
            LaundryGuide = hygiene.LaundryGuide,
            LaundrySupport = hygiene.LaundrySupport,
            WashingMachine = hygiene.WashingMachine,
            WhoClean = hygiene.WhoClean,
        };
            return putEntity;
        }

        
    }
}
