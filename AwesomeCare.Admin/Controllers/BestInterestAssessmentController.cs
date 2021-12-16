using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.BestInterestAssessment;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Http;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.Services.Services;

namespace AwesomeCare.Admin.Controllers
{
    public class BestInterestAssessmentController : BaseController
    {
        private IBestInterestAssessmentService _BestInterestAssessment;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public BestInterestAssessmentController(IStaffService staffService, IBestInterestAssessmentService BestInterestAssessment, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _BestInterestAssessment = BestInterestAssessment;
            _clientService = clientService;
            _baseRecord = baseRecord;
            _staffService = staffService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _BestInterestAssessment.Get();

            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            List<CreateBestInterestAssessment> reports = new List<CreateBestInterestAssessment>();
            foreach (GetBestInterestAssessment item in entities)
            {
                var report = new CreateBestInterestAssessment();
                report.BestId = item.BestId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateBestInterestAssessment();

            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            model.baseRecordList = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
            model.HeadingList = baseRecord.Where(s => s.KeyName == "Health_Task_Heading").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
            model.Heading2List = baseRecord.Where(s => s.KeyName == "Health_Task_Heading2").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
            model.TitleList = baseRecord.Where(s => s.KeyName == "MCA_Health_Task_Title").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
            model.Title2List = baseRecord.Where(s => s.KeyName == "MCA_Health_Task_Title2").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
            model.BaseRecordList = baseRecord.ToList();
            model.ClientId = clientId;
            model.BelieveTaskCount = 1;
            model.CareIssuesTaskCount = model.baseRecordList.Count;
            return View(model);

        }
        public async Task<IActionResult> View(int teamleadId)
        {
            var model = new CreateBestInterestAssessment();

            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBestInterestAssessment model, IFormCollection formcollection)
        {
            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            if (model == null || !ModelState.IsValid)
            {
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);

            }
            List<PostHealthTask> htasks = new List<PostHealthTask>();
            for (int i = 1; i <= model.HealthTaskCount; i++)
            {
                var taskname = $"HealthTaskId{i}";
                PostHealthTask htask = new PostHealthTask();
                var taskId = int.Parse(formcollection[taskname]);
                var headingId = int.Parse(formcollection[string.Concat("HeadingId" ,i)]);
                var titleId = int.Parse(formcollection[string.Concat("Title" , i)]);
                var answerId = int.Parse(formcollection[string.Concat("Answer" , i)]);
                var remarks = formcollection[string.Concat("Remarks" , i)];

                htask.HealthTaskId = taskId;
                htask.BestId = model.BestId;
                htask.HeadingId = headingId;
                htask.Title = titleId;
                htask.Answer = answerId;
                htask.Remarks = remarks;
                htasks.Add(htask);

            }
            List<PostBelieveTask> tasks = new List<PostBelieveTask>();
            for (int i = 0; i < model.BelieveTaskCount; i++)
            {
                PostBelieveTask task = new PostBelieveTask();
                var TaskId = int.Parse(formcollection["BelieveTaskId"][i]);
                var Title = int.Parse(formcollection["ReasonableBelieve"][i]);
                task.BestId = model.BestId;
                task.BelieveTaskId = TaskId;
                task.ReasonableBelieve = Title;
                tasks.Add(task);
            }
            PostBestInterestAssessment post = new PostBestInterestAssessment();
            post.BestId = model.BestId;
            post.ClientId = model.ClientId;
            post.Date = model.Date;
            post.Name = model.Name;
            post.Position = model.Position;
            post.Signature = model.Signature;
            post.PostBelieveTask = tasks;
            post.PostHealthTask = htasks;

            var result = await _BestInterestAssessment.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "BestInterestAssessment successfully added" : "An Error Occurred" });

            return RedirectToAction("Reports");
        }
    }
}
