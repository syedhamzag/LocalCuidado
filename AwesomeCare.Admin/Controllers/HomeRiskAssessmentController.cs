using AwesomeCare.Admin.ViewModels.CarePlan.HomeRiskAssessment;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.CarePlanNutrition;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.Services.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace AwesomeCare.Admin.Controllers
{
    public class HomeRiskAssessmentController : BaseController
    {
        private IHomeRiskAssessmentService _clientHomeRiskAssessment;
        private IClientService _clientService;
        private IBaseRecordService _baseRecord;

        public HomeRiskAssessmentController(IHomeRiskAssessmentService clientHomeRiskAssessment, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseRecord) :base(fileUpload)
        {
            _clientHomeRiskAssessment = clientHomeRiskAssessment;
            _clientService = clientService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _clientHomeRiskAssessment.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateHomeRiskAssessment> reports = new List<CreateHomeRiskAssessment>();
            foreach (GetHomeRiskAssessment item in entities)
            {
                var report = new CreateHomeRiskAssessment();
                report.HomeRiskAssessmentId = item.HomeRiskAssessmentId;
                report.Heading = item.Heading;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId, int homeRiskId, string heading)
        {
            var model = new CreateHomeRiskAssessment();                     
            model.ClientId = clientId;
            model.HomeRiskAssessmentId = homeRiskId;
            model.Heading = heading;
            if (homeRiskId > 0)
            {
                var task = await _clientHomeRiskAssessment.Get(homeRiskId);
                model.TaskCount = task.GetHomeRiskAssessmentTask.Count;
                model.Tasks = task.GetHomeRiskAssessmentTask;
            }
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await GetHomeRisk(homeId);
        //    return View(model);

        //}
        public async Task<IActionResult> ListHeading(int clientId)
        {
            var model = new CreateHomeRiskAssessment();
            model.ClientId = clientId;
            
            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Home_Risk_Assessment_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            List<GetHomeRiskAssessment> home = await _clientHomeRiskAssessment.GetByClient(clientId);
            if (home.Count > 0)
            {
                    model.HomeRiskAssessmentId = home.FirstOrDefault().HomeRiskAssessmentId;
                    model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.HomeRiskAssessmentId.ToString())).ToList();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeading(CreateHomeRiskAssessment model)
        {
            var heading = await _baseRecord.GetBaseRecordItemById(model.HeadingId);
            PostHomeRiskAssessment post = new PostHomeRiskAssessment();
            post.HomeRiskAssessmentId = model.HomeRiskAssessmentId;
            post.ClientId = model.ClientId;
            post.Heading = heading.ValueName;

            var result = new HttpResponseMessage();
            if (post.HomeRiskAssessmentId > 0)
            {
                result = await _clientHomeRiskAssessment.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _clientHomeRiskAssessment.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "HomeRiskAssessment successfully added to Client" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { clientId = model.ClientId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeRiskAssessment model, IFormCollection formcollection)
        {
            List<PostHomeRiskAssessmentTask> tasks = new List<PostHomeRiskAssessmentTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PostHomeRiskAssessmentTask task = new PostHomeRiskAssessmentTask();
                var TaskId = int.Parse(formcollection["HomeRiskAssessmentTaskId"][i]);                
                var Title = int.Parse(formcollection["Title"][i]);
                var Answer = int.Parse(formcollection["Answer"][i]);
                var Comment = formcollection["Comment"][i];
                task.HomeRiskAssessmentId = model.HomeRiskAssessmentId;
                task.HomeRiskAssessmentTaskId = TaskId;
                task.Title = Title;
                task.Answer = Answer;
                task.Comment = Comment;
                tasks.Add(task);
            }
            PostHomeRiskAssessment post = new PostHomeRiskAssessment();
            post.HomeRiskAssessmentId = model.HomeRiskAssessmentId;
            post.ClientId = model.ClientId;
            post.Heading = model.Heading;
            post.PostHomeRiskAssessmentTask = tasks;

            var result = await _clientHomeRiskAssessment.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "HomeRiskAssessment successfully added to Client" : "An Error Occurred" });

            return RedirectToAction("HomeCareDetails","Client", new { clientId = model.ClientId });
        }

        //public async Task<GetHomeRiskAssessment> GetHomeRisk(int homeRiskId)
        //{
        //    var home = await _clientHomeRiskAssessment.Get(homeRiskId);
        //    var putEntity = new GetHomeRiskAssessment
        //    {
        //        HomeRiskAssessmentId = home.HomeRiskAssessmentId,
        //        ClientId = home.ClientId,
        //        Heading = home.Heading,
        //        GetHomeRiskAssessmentTask = home.GetHomeRiskAssessmentTask
        //    };
        //    return putEntity;
        //}


    }
}
