using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientPerformanceIndicator;
using AwesomeCare.Admin.ViewModels.ClientPerformance;
using AwesomeCare.DataTransferObject.DTOs.ClientPerformanceIndicator;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientPerformanceIndicatorController : BaseController
    {
        private IClientPerformanceIndicatorService _PerformanceIndicator;
        private IBaseRecordService _baseRecord;
        private IClientService _clientServices;

        public ClientPerformanceIndicatorController(IClientPerformanceIndicatorService PerformanceIndicator, IFileUpload fileUpload, IBaseRecordService baseRecord, IClientService clientServices) : base(fileUpload)
        {
            _PerformanceIndicator = PerformanceIndicator;
            _baseRecord = baseRecord;
            _clientServices = clientServices;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _PerformanceIndicator.Get();
            var clients = await _clientServices.GetClientDetail();
            List<CreatePerformanceIndicator> reports = new List<CreatePerformanceIndicator>();
            foreach (GetClientPerformanceIndicator item in entities)
            {
                var report = new CreatePerformanceIndicator();
                report.PerformanceIndicatorId = item.PerformanceIndicatorId;
                report.Heading = item.Heading;
                report.ClientName = clients.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int performanceId)
        {
            var performance = await _PerformanceIndicator.Get(performanceId);
            var clients = await _clientServices.GetClientDetail();
            var model = new CreatePerformanceIndicator();
            if (performance != null)
            {
                model.PerformanceIndicatorId = performance.PerformanceIndicatorId;
                model.Heading = performance.Heading;
                model.Rating = performance.Rating;
                model.Date = performance.Date;
                model.DueDate = performance.DueDate;
                model.Remarks = performance.Remarks;
                model.ClientId = performance.ClientId;
                //model.ClientName = clients.Where(s => s.ClientId == performance.ClientId).FirstOrDefault().FullName;
                model.TaskCount = performance.GetClientPerformanceIndicatorTask.Count;
                model.Tasks = performance.GetClientPerformanceIndicatorTask;
            }
            return View(model);

        }
        public async Task<IActionResult> ListHeading(int clientId)
        {
            var model = new CreatePerformanceIndicator();
            var clients = await _clientServices.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Performance_Indicator_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            List<GetClientPerformanceIndicator> pi = await _PerformanceIndicator.Get();
            List<GetClientPerformanceIndicator> cpi = pi.Where(s=>s.ClientId==clientId).ToList();
            if (cpi.Count > 0)
            {
                model.PerformanceIndicatorId = cpi.FirstOrDefault().PerformanceIndicatorId;
                model.Date = cpi.FirstOrDefault().Date;
                model.DueDate = cpi.FirstOrDefault().DueDate;
                model.Rating = cpi.FirstOrDefault().Rating;
                model.Remarks = cpi.FirstOrDefault().Remarks;
                model.HeadingList = cpi.Select(s => new SelectListItem(s.Heading, s.PerformanceIndicatorId.ToString())).ToList();
                model.ClientName = clients.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListHeading(CreatePerformanceIndicator model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            var heading = await _baseRecord.GetBaseRecordItemById(model.HeadingId);
            PostClientPerformanceIndicator post = new PostClientPerformanceIndicator();
            post.PerformanceIndicatorId = model.PerformanceIndicatorId;
            post.Heading = heading.ValueName;
            post.Date = model.Date;
            post.DueDate = model.DueDate;
            post.Rating = model.Rating;
            post.ClientId = model.ClientId;
            post.Remarks = model.Remarks;

            var result = new HttpResponseMessage();
            if (post.PerformanceIndicatorId > 0)
            {
                result = await _PerformanceIndicator.Edit(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _PerformanceIndicator.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "PerformanceIndicator successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { clientId = model.ClientId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePerformanceIndicator model, IFormCollection formcollection)
        {
            List<PutClientPerformanceIndicatorTask> tasks = new List<PutClientPerformanceIndicatorTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PutClientPerformanceIndicatorTask task = new PutClientPerformanceIndicatorTask();
                var TaskId = int.Parse(formcollection["PerformanceIndicatorTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Score = int.Parse(formcollection["Score"][i]);
                task.PerformanceIndicatorId = model.PerformanceIndicatorId;
                task.PerformanceIndicatorTaskId = TaskId;
                task.Title = Title;
                task.Score = Score;

                tasks.Add(task);
            }
            PutClientPerformanceIndicator post = new PutClientPerformanceIndicator();
            post.PerformanceIndicatorId = model.PerformanceIndicatorId;
            post.Heading = model.Heading;
            post.Date = model.Date;
            post.DueDate = model.DueDate;
            post.Rating = model.Rating;
            post.ClientId = model.ClientId;
            post.Remarks = model.Remarks;
            post.PutClientPerformanceIndicatorTask = tasks;

            var result = await _PerformanceIndicator.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "PerformanceIndicator successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("Index", new { performanceId = model.PerformanceIndicatorId });
        }
        [HttpGet]
        public JsonResult DeleteTask(int taskId)
        {
            var getClient = _PerformanceIndicator.DeleteTask(taskId);
            return Json(getClient.Result);
        }
    }
}
