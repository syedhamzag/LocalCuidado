using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffShadowing;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffShadowing;
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
    public class StaffShadowingController : BaseController
    {
        private IStaffShadowingService _staffShadowing;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffShadowingController(IStaffShadowingService staffShadowing, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffShadowing = staffShadowing;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _staffShadowing.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffShadowing> reports = new List<CreateStaffShadowing>();
            foreach (GetStaffShadowing item in entities)
            {
                var report = new CreateStaffShadowing();
                report.StaffShadowingId = item.StaffShadowingId;
                report.Heading = item.Heading;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId, int staffshadowingId, string heading)
        {
            var model = new CreateStaffShadowing();
            model.StaffPersonalInfoId = staffId;
            model.StaffShadowingId = staffshadowingId;
            model.Heading = heading;
            if (staffshadowingId > 0)
            {
                var task = await _staffShadowing.Get(staffshadowingId);
                model.TaskCount = task.GetStaffShadowingTask.Count;
                model.Tasks = task.GetStaffShadowingTask;
            }
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await Getstaffshadowing(homeId);
        //    return View(model);

        //}
        public async Task<IActionResult> ListHeading(int staffId)
        {
            var model = new CreateStaffShadowing();
            model.StaffPersonalInfoId = staffId;

            var staff = await _staffService.GetStaffs();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_Shadowing_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            List<GetStaffShadowing> home = await _staffShadowing.GetByStaffPersonalInfo(staffId);
            if (home.Count > 0)
            {
                model.StaffShadowingId = home.FirstOrDefault().StaffShadowingId;
                model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.StaffShadowingId.ToString())).ToList();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeading(CreateStaffShadowing model)
        {
            var heading = await _baseRecord.GetBaseRecordItemById(model.HeadingId);
            PostStaffShadowing post = new PostStaffShadowing();
            post.StaffShadowingId = model.StaffShadowingId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = heading.ValueName;

            var result = new HttpResponseMessage();
            if (post.StaffShadowingId > 0)
            {
                result = await _staffShadowing.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _staffShadowing.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffShadowing successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffShadowing model, IFormCollection formcollection)
        {
            List<PostStaffShadowingTask> tasks = new List<PostStaffShadowingTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PostStaffShadowingTask task = new PostStaffShadowingTask();
                var TaskId = int.Parse(formcollection["StaffShadowingTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Answer = int.Parse(formcollection["Answer"][i]);
                var Comment = formcollection["Comment"][i];
                var Point = int.Parse(formcollection["Point"][i]);
                var Score = int.Parse(formcollection["Score"][i]);
                task.StaffShadowingId = model.StaffShadowingId;
                task.StaffShadowingTaskId = TaskId;
                task.Title = Title;
                task.Answer = Answer;
                task.Comment = Comment;
                task.Point = Point;
                task.Score = Score;
                tasks.Add(task);
            }
            PostStaffShadowing post = new PostStaffShadowing();
            post.StaffShadowingId = model.StaffShadowingId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = model.Heading;
            post.PostStaffShadowingTask = tasks;

            var result = await _staffShadowing.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffShadowing successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
