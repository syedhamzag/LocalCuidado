using AwesomeCare.Admin.Services.Admin;
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
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class PhysicalAbilityController : BaseController
    {
        private IPhysicalAbilityService _physicalService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;

        public PhysicalAbilityController(IPhysicalAbilityService physicalService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService) : base(fileUpload)
        {
            _physicalService = physicalService;
            _clientService = clientService;
            _baseService = baseService;
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
                report.MobilityName = _baseService.GetBaseRecordItemById(item.Mobility).Result.ValueName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
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
            var physicalAbility = await _physicalService.GetbyClient(clientId);
            if (physicalAbility != null)
            {
                model = GetPhysicalAbility(physicalAbility);
            }
            return View(model);

        }
        public async Task<IActionResult> Delete(int clientId)
        {
            var sp = await _physicalService.GetbyClient(clientId);
            await _physicalService.Delete(sp.PhysicalId);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> View(int clientId)
        {
            var PhysicalAbility = await _physicalService.GetbyClient(clientId);
            var putEntity = GetPhysicalAbility(PhysicalAbility);
            return View(putEntity);
        }
        public async Task<IActionResult> Edit(int PhysicalAbilityId)
        {
            var PhysicalAbility = await _physicalService.Get(PhysicalAbilityId);
            var putEntity = GetPhysicalAbility(PhysicalAbility);
            return View(putEntity);
        }
        public CreatePhysicalAbility GetPhysicalAbility(GetPhysicalAbility PhysicalAbility)
        {
            
            var putEntity = new CreatePhysicalAbility
            {
                Description = PhysicalAbility.Description,
                Mobility = PhysicalAbility.Mobility,
                PhysicalId = PhysicalAbility.PhysicalId,
                ClientId = PhysicalAbility.ClientId,
                Name = PhysicalAbility.Name,
                Status = PhysicalAbility.Status,
                ActionName = "Update",
                Title = "Update Physical Ability"
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

            var result = new HttpResponseMessage();
            if (post.PhysicalId > 0)
            {
                result = await _physicalService.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _physicalService.Post(post);
                var content = await result.Content.ReadAsStringAsync();
            }

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

            PostPhysicalAbility put = new PostPhysicalAbility();

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
