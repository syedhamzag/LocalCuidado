using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffHealth;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
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
    public class StaffHealthController : BaseController
    {
        private IStaffHealthService _staffHealth;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffHealthController(IStaffHealthService staffHealth, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffHealth = staffHealth;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _staffHealth.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffHealth> reports = new List<CreateStaffHealth>();
            foreach (GetStaffHealth item in entities)
            {
                var report = new CreateStaffHealth();
                report.StaffHealthId = item.StaffHealthId;
                report.Heading = item.Heading;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId, int staffHealthId, string heading)
        {
            var model = new CreateStaffHealth();
            model.StaffPersonalInfoId = staffId;
            model.StaffHealthId = staffHealthId;
            model.Heading = heading;
            if (staffHealthId > 0)
            {
                var task = await _staffHealth.Get(staffHealthId);
                model.TaskCount = task.GetStaffHealthTask.Count;
                model.Tasks = task.GetStaffHealthTask;
            }
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await GetstaffHealth(homeId);
        //    return View(model);

        //}
        public async Task<IActionResult> ListHeading(int staffId)
        {
            var model = new CreateStaffHealth();
            model.StaffPersonalInfoId = staffId;

            var staff = await _staffService.GetStaffs();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_Health_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            List<GetStaffHealth> home = await _staffHealth.GetByStaffPersonalInfo(staffId);
            if (home.Count > 0)
            {
                model.StaffHealthId = home.FirstOrDefault().StaffHealthId;
                model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.StaffHealthId.ToString())).ToList();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHeading(CreateStaffHealth model)
        {
            var heading = await _baseRecord.GetBaseRecordItemById(model.HeadingId);
            PostStaffHealth post = new PostStaffHealth();
            post.StaffHealthId = model.StaffHealthId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = heading.ValueName;

            var result = new HttpResponseMessage();
            if (post.StaffHealthId > 0)
            {
                result = await _staffHealth.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _staffHealth.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffHealth successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffHealth model, IFormCollection formcollection)
        {
            List<PostStaffHealthTask> tasks = new List<PostStaffHealthTask>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PostStaffHealthTask task = new PostStaffHealthTask();
                var TaskId = int.Parse(formcollection["StaffHealthTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Answer = int.Parse(formcollection["Answer"][i]);
                var Comment = formcollection["Comment"][i];
                var Point = int.Parse(formcollection["Point"][i]);
                var Score = int.Parse(formcollection["Score"][i]);
                task.StaffHealthId = model.StaffHealthId;
                task.StaffHealthTaskId = TaskId;
                task.Title = Title;
                task.Answer = Answer;
                task.Comment = Comment;
                task.Point = Point;
                task.Score = Score;
                tasks.Add(task);
            }
            PostStaffHealth post = new PostStaffHealth();
            post.StaffHealthId = model.StaffHealthId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Heading = model.Heading;
            post.PostStaffHealthTask = tasks;

            var result = await _staffHealth.Put(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffHealth successfully added to staff" : "An Error Occurred" });

            return RedirectToAction("ListHeading", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
