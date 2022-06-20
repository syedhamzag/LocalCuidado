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
using System.Net.Http;

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
                report.ClientId = item.ClientId;
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
            model.BaseRecordList = baseRecord.ToList();
            model.ClientId = clientId;
            model.BelieveTaskCount = 1;
            model.CareIssuesTaskCount = model.baseRecordList.Count;


            GetBestInterestAssessment mcaBest = await _BestInterestAssessment.Get(clientId);
            if (mcaBest != null)
            {
                model = Get(mcaBest);
                model.baseRecordList = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                model.HeadingList = baseRecord.Where(s => s.KeyName == "Health_Task_Heading").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                model.Heading2List = baseRecord.Where(s => s.KeyName == "Health_Task_Heading2").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                model.BaseRecordList = baseRecord.ToList();
                model.BelieveTaskCount = mcaBest.GetBelieveTask.Count;
                model.CareIssuesTaskCount = model.baseRecordList.Count;

            }
            return View(model);

        }
        public async Task<IActionResult> View(int clientId)
        {
            var model = new CreateBestInterestAssessment();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            GetBestInterestAssessment mcaBest = await _BestInterestAssessment.Get(clientId); ;
            if (mcaBest != null)
            {
                model = Get(mcaBest);
                model.baseRecordList = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                model.HeadingList = baseRecord.Where(s => s.KeyName == "Health_Task_Heading").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                model.Heading2List = baseRecord.Where(s => s.KeyName == "Health_Task_Heading2").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                model.BaseRecordList = baseRecord.ToList();
                model.BelieveTaskCount = mcaBest.GetBelieveTask.Count;
                model.CareIssuesTaskCount = model.baseRecordList.Count;

            }
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
            List<PostHealthTask2> h2tasks = new List<PostHealthTask2>();
            for (int i = 1; i <= model.HealthTask2Count; i++)
            {
                var taskname = $"HealthTask2Id{i}";
                PostHealthTask2 h2task = new PostHealthTask2();
                var taskId = int.Parse(formcollection[taskname]);
                var headingId = int.Parse(formcollection[string.Concat("Heading2Id", i)]);
                var titleId = int.Parse(formcollection[string.Concat("Title2", i)]);
                var answerId = int.Parse(formcollection[string.Concat("Answer2", i)]);
                var remarks = formcollection[string.Concat("Remarks2", i)];

                h2task.HealthTask2Id = taskId;
                h2task.BestId = model.BestId;
                h2task.Heading2Id = headingId;
                h2task.Title = titleId;
                h2task.Answer = answerId;
                h2task.Remark = remarks;
                h2tasks.Add(h2task);

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
            List<PostCareIssuesTask> ptasks = new List<PostCareIssuesTask>();
            for (int i = 1; i <= model.CareIssuesTaskCount; i++)
            {
                var check = formcollection[string.Concat("careIssues", i)];
                if (check.Count > 0)
                {
                    if (check[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var Id = int.Parse(formcollection[string.Concat("CareIssuesTaskId", i)]);
                        var issue = int.Parse(formcollection[string.Concat("Issues", i)]);
                        PostCareIssuesTask list = new PostCareIssuesTask();
                        list.BestId = model.BestId;
                        list.CareIssuesTaskId = Id;
                        list.Issues = issue;
                        ptasks.Add(list);
                    }
                }
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
            post.PostHealthTask2 = h2tasks;
            post.PostCareIssuesTask = ptasks;

            var result = new HttpResponseMessage();
            if (model.BestId > 0)
            {
                result = await _BestInterestAssessment.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            { 
                result = await _BestInterestAssessment.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "BestInterestAssessment successfully added" : "An Error Occurred" });

            return RedirectToAction("Reports");
        }

        private CreateBestInterestAssessment Get(GetBestInterestAssessment mcaBest)
        {

            CreateBestInterestAssessment model = new CreateBestInterestAssessment();
            if (mcaBest != null)
            {
                model.ActionName = "Update";
                model.ClientId = mcaBest.ClientId;
                model.Date = mcaBest.Date;
                model.BestId = mcaBest.BestId;
                model.Name = mcaBest.Name;
                model.Position = mcaBest.Position;
                model.Signature = mcaBest.Signature;
                model.GetBelieveTask = (from t in mcaBest.GetBelieveTask
                                    select new GetBelieveTask
                                    {
                                        BelieveTaskId = t.BelieveTaskId,
                                        BestId = t.BestId,
                                        ReasonableBelieve = t.ReasonableBelieve,
                                    }).ToList();
                model.GetCareIssuesTask = (from t in mcaBest.GetCareIssuesTask
                                     select new GetCareIssuesTask
                                     {
                                         CareIssuesTaskId = t.CareIssuesTaskId,
                                         BestId = t.BestId,
                                         Issues = t.Issues,
                                     }).ToList();
                model.GetHealthTask = (from t in mcaBest.GetHealthTask
                                       select new GetHealthTask
                                       {
                                           HealthTaskId = t.HealthTaskId,
                                           BestId = t.BestId,
                                           HeadingId = t.HeadingId,
                                           Title = t.Title,
                                           Answer = t.Answer,
                                           Remarks = t.Remarks
                                       }).ToList();
                model.GetHealthTask2 = (from t in mcaBest.GetHealthTask2
                                        select new GetHealthTask2
                                        {
                                            HealthTask2Id = t.HealthTask2Id,
                                            BestId = t.BestId,
                                            Heading2Id = t.Heading2Id,
                                            Title = t.Title,
                                            Answer = t.Answer,
                                            Remark = t.Remark
                                        }).ToList();
            }
            return model;
        }
    }
}
