using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffCompetenceTest;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
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
    public class StaffCompetenceTestController : BaseController
    {
        private IStaffCompetenceTestService _staffCompetenceTest;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffCompetenceTestController(IStaffCompetenceTestService staffCompetenceTest, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffCompetenceTest = staffCompetenceTest;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _staffCompetenceTest.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffCompetenceTest> reports = new List<CreateStaffCompetenceTest>();
            foreach (GetStaffCompetenceTest item in entities)
            {
                var report = new CreateStaffCompetenceTest();
                report.StaffCompetenceTestId = item.StaffCompetenceTestId;
                report.Heading = item.Heading;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId, int staffCompetenceTestId, string heading)
        {
            var model = new CreateStaffCompetenceTest();
            model.StaffPersonalInfoId = staffId;
            model.StaffCompetenceTestId = staffCompetenceTestId;
            model.Heading = heading;
            if (staffCompetenceTestId > 0)
            {
                var task = await _staffCompetenceTest.Get(staffCompetenceTestId);
                model.TaskCount = task.GetStaffCompetenceTestTask.Count;
                model.Tasks = task.GetStaffCompetenceTestTask;
            }
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await GetstaffCompetenceTest(homeId);
        //    return View(model);

        //}
        public async Task<IActionResult> ListHeading(int staffId)
        {
            var model = new CreateStaffCompetenceTest();
            model.StaffPersonalInfoId = staffId;

            var staff = await _staffService.GetStaffs();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_CompetenceTest_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            List<GetStaffCompetenceTest> home = await _staffCompetenceTest.GetByStaffPersonalInfo(staffId);
            if (home.Count > 0)
            {
                model.StaffCompetenceTestId = home.FirstOrDefault().StaffCompetenceTestId;
                model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.StaffCompetenceTestId.ToString())).ToList();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeading(CreateStaffCompetenceTest model)
        {
            var heading = await _baseRecord.GetBaseRecordItemById(model.HeadingId);
            PostStaffCompetenceTest post = new PostStaffCompetenceTest();
            post.StaffCompetenceTestId = model.StaffCompetenceTestId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = heading.ValueName;

            var result = new HttpResponseMessage();
            if (post.StaffCompetenceTestId > 0)
            {
                result = await _staffCompetenceTest.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _staffCompetenceTest.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffCompetenceTest successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffCompetenceTest model, IFormCollection formcollection)
        {
            List<PostStaffCompetenceTestTask> tasks = new List<PostStaffCompetenceTestTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PostStaffCompetenceTestTask task = new PostStaffCompetenceTestTask();
                var TaskId = int.Parse(formcollection["StaffCompetenceTestTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Answer = int.Parse(formcollection["Answer"][i]);
                var Comment = formcollection["Comment"][i];
                var Point = int.Parse(formcollection["Point"][i]);
                var Score = int.Parse(formcollection["Score"][i]);
                task.StaffCompetenceTestId = model.StaffCompetenceTestId;
                task.StaffCompetenceTestTaskId = TaskId;
                task.Title = Title;
                task.Answer = Answer;
                task.Comment = Comment;
                task.Point = Point;
                task.Score = Score;
                tasks.Add(task);
            }
            PostStaffCompetenceTest post = new PostStaffCompetenceTest();
            post.StaffCompetenceTestId = model.StaffCompetenceTestId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = model.Heading;
            post.PostStaffCompetenceTestTask = tasks;

            var result = await _staffCompetenceTest.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffCompetenceTest successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
