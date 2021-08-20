using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Balance;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.ViewModels.CarePlan.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class BalanceController : BaseController
    {
        private IBalanceService _balanceService;
        private IClientService _clientService;

        public BalanceController(IBalanceService balanceService, IFileUpload fileUpload, IClientService clientService ) : base(fileUpload)
        {
            _balanceService = balanceService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _balanceService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateBalance> reports = new List<CreateBalance>();
            foreach (GetBalance item in entities)
            {
                var report = new CreateBalance();
                report.BalanceId = item.BalanceId;
                report.Description = item.Description;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.Status = item.Status;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateBalance();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }

        public async Task<IActionResult> View(int BalanceId)
        {
            var putEntity = await GetBalance(BalanceId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int BalanceId)
        {
            var putEntity = await GetBalance(BalanceId);
            return View(putEntity);
        }

        public async Task<CreateBalance> GetBalance(int BalanceId)
        {
            var balance = await _balanceService.Get(BalanceId);
            var putEntity = new CreateBalance
            {
                Description = balance.Description,
                Mobility = balance.Mobility,
                BalanceId = balance.BalanceId,
                ClientId = balance.ClientId,
                Name = balance.Name,
                Status = balance.Status,
            };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateBalance model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostBalance post = new PostBalance();

            post.BalanceId = model.BalanceId;
            post.ClientId = model.ClientId;
            post.Description = model.Description;
            post.Mobility = model.Mobility;
            post.Name = model.Name;
            post.Status = model.Status;

            var json = JsonConvert.SerializeObject(post);
            var result = await _balanceService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Balance successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateBalance model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutBalance put = new PutBalance();

            put.BalanceId = model.BalanceId;
            put.ClientId = model.ClientId;
            put.Description = model.Description;
            put.Mobility = model.Mobility;
            put.Name = model.Name;
            put.Status = model.Status;

            var entity = await _balanceService.Put(put);
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
