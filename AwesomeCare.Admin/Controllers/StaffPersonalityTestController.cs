using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
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
    public class StaffPersonalityTestController : BaseController
    {
        private IStaffPersonalityTest _personalityTestService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;

        public StaffPersonalityTestController(IFileUpload fileupload, IStaffService staffService, IStaffPersonalityTest personalityTestService, IBaseRecordService baseService) : base(fileupload)
        {
            _staffService = staffService;
            _personalityTestService = personalityTestService;
            _baseService = baseService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _personalityTestService.Get();

            var staffs = await _staffService.GetStaffs();
            List<CreateStaffPersonalityTest> reports = new List<CreateStaffPersonalityTest>();
            foreach (GetStaffPersonalityTest item in entities)
            {

                var report = new CreateStaffPersonalityTest();
                if (reports.Count == 0)
                {
                    report.StaffPersonalInfoId = item.StaffPersonalInfoId;
                    report.StaffName = staffs.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                    report.QuestionName = _baseService.GetBaseRecordItemById(item.Question).Result.ValueName;
                    report.AnswerName = _baseService.GetBaseRecordItemById(item.Answer).Result.ValueName;
                    reports.Add(report);
                }
                else
                {
                    if (reports.FirstOrDefault().StaffPersonalInfoId != item.StaffPersonalInfoId)
                    {
                        report.StaffPersonalInfoId = item.StaffPersonalInfoId;
                        report.StaffName = staffs.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                        report.QuestionName = _baseService.GetBaseRecordItemById(item.Question).Result.ValueName;
                        report.AnswerName = _baseService.GetBaseRecordItemById(item.Answer).Result.ValueName;
                        reports.Add(report);
                    }
                }
                
            }
            return View(reports);
        }
        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateStaffPersonalityTest();
            var ptest = await _personalityTestService.Get(staffId);
            if (ptest != null)
            {
                model.StaffPersonalInfoId = ptest.FirstOrDefault().StaffPersonalInfoId;
                model.GetStaffPersonalityTest = ptest;
                model.PersonalityCount = ptest.Count();
            }
            return View(model);

        }
        public async Task<IActionResult> View(int staffId)
        {
            var model = new CreateStaffPersonalityTest();
            var ptest = await _personalityTestService.Get(staffId);
            if (ptest != null)
            {
                model.GetStaffPersonalityTest = ptest;
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffPersonalityTest model, IFormCollection formcollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                return View(model);
            }
            List<PostStaffPersonalityTest> posts = new List<PostStaffPersonalityTest>();
            for (int i =0; i < model.PersonalityCount; i++)
            {
                PostStaffPersonalityTest post = new PostStaffPersonalityTest();
                var Question = int.Parse(formcollection["Question"][i]);
                var Answer = int.Parse(formcollection["Answer"][i]);
                var TestId = int.Parse(formcollection["TestId"][i]);
                post.TestId = TestId;
                post.StaffPersonalInfoId = model.StaffPersonalInfoId;
                post.Question = Question;
                post.Answer = Answer;
                posts.Add(post);
            }
            var result = new HttpResponseMessage();
            if (posts.FirstOrDefault().TestId > 0)
            {
                result = await _personalityTestService.Put(posts);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            { 
            result = await _personalityTestService.Create(posts);
            var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode != false ? "New Personality Test successfully registered" : "An Error Occurred" });
            return RedirectToAction("Dashboard", "Dashboard");

        }
    }
}
