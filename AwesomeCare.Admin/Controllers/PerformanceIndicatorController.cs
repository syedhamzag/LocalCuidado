using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.PerformanceIndicator;
using AwesomeCare.Admin.ViewModels.PerformanceIndicator;
using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class PerformanceIndicatorController : BaseController
    {
        private IPerformanceIndicatorService _PerformanceIndicator;
        private IBaseRecordService _baseRecord;

        public PerformanceIndicatorController(IPerformanceIndicatorService PerformanceIndicator, IFileUpload fileUpload, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _PerformanceIndicator = PerformanceIndicator;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _PerformanceIndicator.Get();

            List<CreatePerformanceIndicator> reports = new List<CreatePerformanceIndicator>();
            foreach (GetPerformanceIndicator item in entities)
            {
                var report = new CreatePerformanceIndicator();
                report.PerformanceIndicatorId = item.PerformanceIndicatorId;
                report.Heading = item.Heading;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int PerformanceIndicatorId)
        {
            var performance = await _PerformanceIndicator.Get(PerformanceIndicatorId);
            var model = new CreatePerformanceIndicator();
            model.PerformanceIndicatorId = PerformanceIndicatorId;
            
            if (performance != null)
            {
                model.Heading = performance.Heading;
                model.Rating = performance.Rating;
                model.Date = performance.Date;
                model.DueDate = performance.DueDate;
                model.Remarks = performance.Remarks;
                model.TaskCount = performance.GetPerformanceIndicatorTask.Count;
                model.Tasks = performance.GetPerformanceIndicatorTask;
            }
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await GetPerformanceIndicator(homeId);
        //    return View(model);

        //}
        public async Task<IActionResult> ListHeading()
        {
            var model = new CreatePerformanceIndicator();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Performance_Indicator_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            List<GetPerformanceIndicator> pi = await _PerformanceIndicator.Get();
            if (pi.Count > 0)
            {
                model.PerformanceIndicatorId = pi.FirstOrDefault().PerformanceIndicatorId;
                model.Date = pi.FirstOrDefault().Date;
                model.DueDate = pi.FirstOrDefault().DueDate;
                model.Rating = pi.FirstOrDefault().Rating;
                model.Remarks = pi.FirstOrDefault().Remarks;
                model.HeadingList = pi.Select(s => new SelectListItem(s.Heading, s.PerformanceIndicatorId.ToString())).ToList();
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
            PostPerformanceIndicator post = new PostPerformanceIndicator();
            post.PerformanceIndicatorId = model.PerformanceIndicatorId;
            post.Heading = heading.ValueName;
            post.Date = model.Date;
            post.DueDate = model.DueDate;
            post.Rating = model.Rating;
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

            return RedirectToAction("Index", new { PerformanceIndicatorId = post.PerformanceIndicatorId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePerformanceIndicator model, IFormCollection formcollection)
        {
            List<PutPerformanceIndicatorTask> tasks = new List<PutPerformanceIndicatorTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PutPerformanceIndicatorTask task = new PutPerformanceIndicatorTask();
                var TaskId = int.Parse(formcollection["PerformanceIndicatorTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Score = int.Parse(formcollection["Score"][i]);
                task.PerformanceIndicatorId = model.PerformanceIndicatorId;
                task.PerformanceIndicatorTaskId = TaskId;
                task.Title = Title;
                task.Score = Score;
                
                tasks.Add(task);
            }
            PutPerformanceIndicator post = new PutPerformanceIndicator();
            post.PerformanceIndicatorId = model.PerformanceIndicatorId;
            post.Heading = model.Heading;
            post.Date = model.Date;
            post.DueDate = model.DueDate;
            post.Rating = model.Rating;
            post.Remarks = model.Remarks;
            post.PutPerformanceIndicatorTask = tasks;

            var result = await _PerformanceIndicator.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "PerformanceIndicator successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("Index", new { PerformanceIndicatorId = post.PerformanceIndicatorId });
        }
        [HttpGet]
        public JsonResult DeleteTask(int taskId)
        {
            var getClient = _PerformanceIndicator.DeleteTask(taskId);
            return Json(getClient.Result);
        }
    }
}
