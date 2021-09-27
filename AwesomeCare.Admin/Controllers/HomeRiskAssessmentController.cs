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

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateHomeRiskAssessment();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Home_Risk_Assessment_Heading").Select(s=>s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.HeadingList = filterBaseRecord.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }
        public async Task<IActionResult> View(int homeId)
        {
            var model = await GetHomeRisk(homeId);
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateHomeRiskAssessment create)
        {

            List<PostHomeRiskAssessment> posts = new List<PostHomeRiskAssessment>();
            foreach (var item in create.HeadingList)
            {
                PostHomeRiskAssessment post = new PostHomeRiskAssessment();
                post.ClientId = create.ClientId;
                post.Heading = item.Text;
                post.PostHomeRiskAssessmentTask = create.Tasks.Select(s => new PostHomeRiskAssessmentTask { Answer = s.Answer, Comment = s.Comment, Title = s.Title }).ToList();
                posts.Add(post);
            }
            

            var result = await _clientHomeRiskAssessment.Create(posts);
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                return View("HomeCareDetails", create);
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "HomeRiskAssessment successfully added to Client" : "An Error Occurred" });

            return RedirectToAction("HomeCareDetails", new { clientId = create.ClientId });
        }

        public async Task<GetHomeRiskAssessment> GetHomeRisk(int homeRiskId)
        {
            var home = await _clientHomeRiskAssessment.Get(homeRiskId);
            var putEntity = new GetHomeRiskAssessment
            {
                HomeRiskAssessmentId = home.HomeRiskAssessmentId,
                ClientId = home.ClientId,
                Heading = home.Heading,
                GetHomeRiskAssessmentTask = home.GetHomeRiskAssessmentTask
            };
            return putEntity;
        }


    }
}
