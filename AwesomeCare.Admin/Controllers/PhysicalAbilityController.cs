using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.PhysicalAbility;
using AwesomeCare.Admin.ViewModels.CarePlan.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class PhysicalAbilityController : BaseController
    {
        private IPhysicalAbilityService _physicalService;
        private IClientService _clientService;

        public PhysicalAbilityController(IPhysicalAbilityService physicalService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _physicalService = physicalService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _physicalService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreatePhysicalAbility> reports = new List<CreatePhysicalAbility>();
            foreach (GetPhysicalAbility item in entities)
            {
                var report = new CreatePhysicalAbility();
                report.PhysicalId = item.PhysicalId;
                report.Description = item.Description;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.Status = item.Status;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreatePhysicalAbility();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }

        public async Task<IActionResult> View(int PhysicalAbilityId)
        {
            var putEntity = await GetPhysicalAbility(PhysicalAbilityId);
            return View(putEntity);
        }
        public async Task<IActionResult> Edit(int PhysicalAbilityId)
        {
            var putEntity = await GetPhysicalAbility(PhysicalAbilityId);
            return View(putEntity);
        }
        public async Task<CreatePhysicalAbility> GetPhysicalAbility(int PhysicalAbilityId)
        {
            var PhysicalAbility = await _physicalService.Get(PhysicalAbilityId);
            var putEntity = new CreatePhysicalAbility
            {
                Description = PhysicalAbility.Description,
                Mobility = PhysicalAbility.Mobility,
                PhysicalId = PhysicalAbility.PhysicalId,
                ClientId = PhysicalAbility.ClientId,
                Name = PhysicalAbility.Name,
                Status = PhysicalAbility.Status,
            };
            return putEntity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePhysicalAbility model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostPhysicalAbility post = new PostPhysicalAbility();

            post.PhysicalId = model.PhysicalId;
            post.ClientId = model.ClientId;
            post.Description = model.Description;
            post.Mobility = model.Mobility;
            post.Name = model.Name;
            post.Status = model.Status;

            var json = JsonConvert.SerializeObject(post);
            var result = await _physicalService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Pressure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreatePhysicalAbility model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutPhysicalAbility put = new PutPhysicalAbility();

            put.PhysicalId = model.PhysicalId;
            put.ClientId = model.ClientId;
            put.Description = model.Description;
            put.Mobility = model.Mobility;
            put.Name = model.Name;
            put.Status = model.Status;

            var entity = await _physicalService.Put(put);
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
