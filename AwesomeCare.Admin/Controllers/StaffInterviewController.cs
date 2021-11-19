using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffInterview;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffInterview;
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
    public class StaffInterviewController : BaseController
    {
        private IStaffInterviewService _staffInterview;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffInterviewController(IStaffInterviewService staffInterview, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffInterview = staffInterview;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _staffInterview.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffInterview> reports = new List<CreateStaffInterview>();
            foreach (GetStaffInterview item in entities)
            {
                var report = new CreateStaffInterview();
                report.StaffInterviewId = item.StaffInterviewId;
                report.Heading = item.Heading;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId, int staffInterviewId, string heading)
        {
            var model = new CreateStaffInterview();
            model.StaffPersonalInfoId = staffId;
            model.StaffInterviewId = staffInterviewId;
            model.Heading = heading;
            if (staffInterviewId > 0)
            {
                var task = await _staffInterview.Get(staffInterviewId);
                model.TaskCount = task.GetStaffInterviewTask.Count;
                model.Tasks = task.GetStaffInterviewTask;
            }
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await GetstaffInterview(homeId);
        //    return View(model);

        //}
        public async Task<IActionResult> ListHeading(int staffId)
        {
            var model = new CreateStaffInterview();
            model.StaffPersonalInfoId = staffId;

            var staff = await _staffService.GetStaffs();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_Interview_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            List<GetStaffInterview> home = await _staffInterview.GetByStaffPersonalInfo(staffId);
            if (home.Count > 0)
            {
                model.StaffInterviewId = home.FirstOrDefault().StaffInterviewId;
                model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.StaffInterviewId.ToString())).ToList();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeading(CreateStaffInterview model)
        {
            var heading = await _baseRecord.GetBaseRecordItemById(model.HeadingId);
            PostStaffInterview post = new PostStaffInterview();
            post.StaffInterviewId = model.StaffInterviewId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = heading.ValueName;

            var result = new HttpResponseMessage();
            if (post.StaffInterviewId > 0)
            {
                result = await _staffInterview.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _staffInterview.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffInterview successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffInterview model, IFormCollection formcollection)
        {
            List<PostStaffInterviewTask> tasks = new List<PostStaffInterviewTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PostStaffInterviewTask task = new PostStaffInterviewTask();
                var TaskId = int.Parse(formcollection["StaffInterviewTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Answer = int.Parse(formcollection["Answer"][i]);
                var Comment = formcollection["Comment"][i];
                var Point = int.Parse(formcollection["Point"][i]);
                var Score = int.Parse(formcollection["Score"][i]);
                task.StaffInterviewId = model.StaffInterviewId;
                task.StaffInterviewTaskId = TaskId;
                task.Title = Title;
                task.Answer = Answer;
                task.Comment = Comment;
                task.Point = Point;
                task.Score = Score;
                tasks.Add(task);
            }
            PostStaffInterview post = new PostStaffInterview();
            post.StaffInterviewId = model.StaffInterviewId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = model.Heading;
            post.PostStaffInterviewTask = tasks;

            var result = await _staffInterview.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffInterview successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
