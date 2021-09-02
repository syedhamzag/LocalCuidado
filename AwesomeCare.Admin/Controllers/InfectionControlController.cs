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

        public async Task<IActionResult> Reports()
        {
            var entities = await _infectionService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateInfectionControl> reports = new List<CreateInfectionControl>();
            foreach (GetInfectionControl item in entities)
            {
                var report = new CreateInfectionControl();
                report.ClientId = item.ClientId;
                report.Guideline = item.Guideline;
                report.InfectionId = item.InfectionId;
                report.Remarks = item.Remarks;
                report.Status = item.Status;
                report.TestDate = item.TestDate;
                report.Type = item.Type;
                report.VaccStatus = item.VaccStatus;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.Status = item.Status;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateInfectionControl();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);
        }

        public async Task<IActionResult> View(int InfectionControlId)
        {
            var putEntity = await GetInfectionControl(InfectionControlId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int InfectionControlId)
        {
            var putEntity = await GetInfectionControl(InfectionControlId);
            return View(putEntity);
        }

        public async Task<CreateInfectionControl> GetInfectionControl(int InfectionControlId)
        {
            var infection = await _infectionService.Get(InfectionControlId);
            var putEntity = new CreateInfectionControl
            {
            ClientId = infection.ClientId,
            Guideline = infection.Guideline,
            InfectionId = infection.InfectionId,
            Remarks = infection.Remarks,
            Status = infection.Status,
            TestDate = infection.TestDate,
            Type = infection.Type,
            VaccStatus = infection.VaccStatus,
        };
            return putEntity;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateInfectionControl model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutInfectionControl put = new PutInfectionControl();

            put.ClientId = model.ClientId;
            put.Guideline = model.Guideline;
            put.InfectionId = model.InfectionId;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.TestDate = model.TestDate;
            put.Type = model.Type;
            put.VaccStatus = model.VaccStatus;

            var entity = await _infectionService.Put(put);
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
