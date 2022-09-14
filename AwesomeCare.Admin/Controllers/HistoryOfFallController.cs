using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.HistoryOfFall;
using AwesomeCare.Admin.ViewModels.CarePlan.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class HistoryOfFallController : BaseController
    {
        private IHistoryOfFallService _historyService;
        private IClientService _clientService;

        public HistoryOfFallController(IHistoryOfFallService historyService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _historyService = historyService;
            _clientService = clientService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _historyService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateHistoryOfFall> reports = new List<CreateHistoryOfFall>();
            foreach (GetHistoryOfFall item in entities)
            {
                var report = new CreateHistoryOfFall();
                report.HistoryId = item.HistoryId;
                report.Cause = item.Cause;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.Date = item.Date;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateHistoryOfFall();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var historyOfFall = await _historyService.GetbyClient(clientId);
            if (historyOfFall != null)
            {
                model = GetHistoryOfFall(historyOfFall);
            }
            return View(model);

        }
        public async Task<IActionResult> Delete(int clientId)
        {
            var sp = await _historyService.GetbyClient(clientId);
            await _historyService.Delete(sp.HistoryId);
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = clientId });
        }
        public async Task<IActionResult> View(int clientId)
        {
            var HistoryOfFall = await _historyService.GetbyClient(clientId);
            var putEntity = GetHistoryOfFall(HistoryOfFall);
            return View(putEntity);
        }
        public async Task<IActionResult> Edit(int HistoryOfFallId)
        {
            var HistoryOfFall = await _historyService.Get(HistoryOfFallId);
            var putEntity = GetHistoryOfFall(HistoryOfFall);
            return View(putEntity);
        }
        public CreateHistoryOfFall GetHistoryOfFall(GetHistoryOfFall HistoryOfFall)
        {
            
            var putEntity = new CreateHistoryOfFall
            {
                HistoryId = HistoryOfFall.HistoryId,
                Cause = HistoryOfFall.Cause,
                Date = HistoryOfFall.Date,
                ClientId = HistoryOfFall.ClientId,
                Details = HistoryOfFall.Details,
                Prevention = HistoryOfFall.Prevention,
                ActionName = "Update",
                Title = "Update History Of Fall"
            };
            return putEntity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateHistoryOfFall model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostHistoryOfFall post = new PostHistoryOfFall();

            post.HistoryId = model.HistoryId;
            post.ClientId = model.ClientId;
            post.Cause = model.Cause;
            post.Details = model.Details;
            post.Prevention = model.Prevention;
            post.Date = model.Date;

            var result = new HttpResponseMessage();
            if (post.HistoryId > 0)
            {
                result = await _historyService.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _historyService.Post(post);
                var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Pressure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateHistoryOfFall model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PostHistoryOfFall put = new PostHistoryOfFall();

            put.HistoryId = model.HistoryId;
            put.ClientId = model.ClientId;
            put.Date = model.Date;
            put.Details = model.Details;
            put.Cause = model.Cause;
            put.Prevention = model.Prevention;

            var entity = await _historyService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }
    }
}
