using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.SpecialHealthCondition;
using AwesomeCare.Admin.ViewModels.CarePlan.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class SpecialHealthConditionController : BaseController
    {
        private ISpecialHealthConditionService _sphealthService;
        private IClientService _clientService;

        public SpecialHealthConditionController(ISpecialHealthConditionService sphealthService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _sphealthService = sphealthService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _sphealthService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateSpecialHealthCondition> reports = new List<CreateSpecialHealthCondition>();
            foreach (GetSpecialHealthCondition item in entities)
            {
                var report = new CreateSpecialHealthCondition();
                report.HealthCondId = item.HealthCondId;
                report.ClientAction = item.ClientAction;
                report.ConditionName = item.ConditionName;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.LivingActivities = item.LivingActivities;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateSpecialHealthCondition();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var getHealth = await _sphealthService.GetbyClient(clientId);
            if (getHealth != null)
            {
                model = GetSpecialHealthCondition(getHealth);
            }
            return View(model);

        }
        public async Task<IActionResult> Delete(int clientId)
        {
            var sp = await _sphealthService.GetbyClient(clientId);
            await _sphealthService.Delete(sp.HealthCondId);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> View(int clientId)
        {
            var sphealth = await _sphealthService.GetbyClient(clientId);
            var putEntity = GetSpecialHealthCondition(sphealth);
            return View(putEntity);
        }
        public async Task<IActionResult> Edit(int SpecialHealthConditionId)
        {
            var sphealth = await _sphealthService.Get(SpecialHealthConditionId);
            var putEntity = GetSpecialHealthCondition(sphealth);
            return View(putEntity);
        }
        public CreateSpecialHealthCondition GetSpecialHealthCondition(GetSpecialHealthCondition sphealth)
        {
            
            var putEntity = new CreateSpecialHealthCondition
            {
                ClientAction = sphealth.ClientAction,
                ClinicRecommendation = sphealth.ClinicRecommendation,
                ConditionName = sphealth.ConditionName,
                ClientId = sphealth.ClientId,
                FeelingAfterIncident = sphealth.FeelingAfterIncident,
                LivingActivities = sphealth.LivingActivities,
                FeelingBeforeIncident = sphealth.FeelingBeforeIncident,
                Frequency = sphealth.Frequency,
                HealthCondId = sphealth.HealthCondId,
                LifestyleSupport = sphealth.LifestyleSupport,
                PlanningHealthCondition = sphealth.PlanningHealthCondition,
                SourceInformation = sphealth.SourceInformation,
                Trigger = sphealth.Trigger,
                ActionName = "Update",
                Title = "Update Special Health Condition"
            };
            return putEntity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateSpecialHealthCondition model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostSpecialHealthCondition post = new PostSpecialHealthCondition();

            post.ClientAction = model.ClientAction;
            post.ClinicRecommendation = model.ClinicRecommendation;
            post.ConditionName = model.ConditionName;
            post.ClientId = model.ClientId;
            post.FeelingAfterIncident = model.FeelingAfterIncident;
            post.LivingActivities = model.LivingActivities;
            post.FeelingBeforeIncident = model.FeelingBeforeIncident;
            post.Frequency = model.Frequency;
            post.HealthCondId = model.HealthCondId;
            post.LifestyleSupport = model.LifestyleSupport;
            post.PlanningHealthCondition = model.PlanningHealthCondition;
            post.SourceInformation = model.SourceInformation;
            post.Trigger = model.Trigger;

            var result = new HttpResponseMessage();
            if (post.HealthCondId > 0)
            {
                result = await _sphealthService.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _sphealthService.Post(post);
                var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Pressure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateSpecialHealthCondition model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PostSpecialHealthCondition put = new PostSpecialHealthCondition();

            put.ClientAction = model.ClientAction;
            put.ClinicRecommendation = model.ClinicRecommendation;
            put.ConditionName = model.ConditionName;
            put.ClientId = model.ClientId;
            put.FeelingAfterIncident = model.FeelingAfterIncident;
            put.LivingActivities = model.LivingActivities;
            put.FeelingBeforeIncident = model.FeelingBeforeIncident;
            put.Frequency = model.Frequency;
            put.HealthCondId = model.HealthCondId;
            put.LifestyleSupport = model.LifestyleSupport;
            put.PlanningHealthCondition = model.PlanningHealthCondition;
            put.SourceInformation = model.SourceInformation;
            put.Trigger = model.Trigger;

            var entity = await _sphealthService.Put(put);
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
    }
}
