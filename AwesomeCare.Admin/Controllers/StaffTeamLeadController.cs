using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffTeamLead;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLead;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffTeamLeadController : BaseController
    {
        private IStaffTeamLeadService _StaffTeamLead;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffTeamLeadController(IStaffService staffService, IStaffTeamLeadService StaffTeamLead, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _StaffTeamLead = StaffTeamLead;
            _clientService = clientService;
            _baseRecord = baseRecord;
            _staffService = staffService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffTeamLead.Get();

            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            List<CreateStaffTeamLead> reports = new List<CreateStaffTeamLead>();
            foreach (GetStaffTeamLead item in entities)
            {
                var report = new CreateStaffTeamLead();
                report.TeamLeadId = item.TeamLeadId;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffInvolved).FirstOrDefault().Fullname;
                report.ClientName = client.Where(s => s.ClientId == item.ClientInvolved).FirstOrDefault().FullName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateStaffTeamLead();

            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_Team_Lead_Task_Title").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int teamleadId)
        {
            var model = new CreateStaffTeamLead();

            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_TeamLead_Title").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffTeamLead model, IFormCollection formcollection)
        {
            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            if (model == null || !ModelState.IsValid)
            {
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);

            }
            List<PostStaffTeamLeadTasks> tasks = new List<PostStaffTeamLeadTasks>();
            for (int i = 0; i < model.TaskCount; i++)
            {
                PostStaffTeamLeadTasks task = new PostStaffTeamLeadTasks();
                var TaskId = int.Parse(formcollection["TeamLeadTaskId"][i]);
                var Title = int.Parse(formcollection["Title"][i]);
                var Comment = formcollection["Comments"][i];
                var Status = formcollection["Status"][i];
                task.TeamLeadId = model.TeamLeadId;
                task.TeamLeadTaskId = TaskId;
                task.Title = Title;
                task.Comments = Comment;
                task.Status = Status;
                tasks.Add(task);
            }
            PostStaffTeamLead post = new PostStaffTeamLead();
            post.TeamLeadId = model.TeamLeadId;
            post.ClientInvolved = model.ClientInvolved;
            post.Date = model.Date;
            post.StaffInvolved = model.StaffInvolved;
            post.DidYouDo = model.DidYouDo;
            post.DidYouObserved = model.DidYouObserved;
            post.OfficeToDo = model.OfficeToDo;
            post.Rota = model.Rota;
            post.StaffStoppedWorking = model.StaffStoppedWorking;
            post.PostStaffTeamLeadTasks = tasks;

            var result = await _StaffTeamLead.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffTeamLead successfully added" : "An Error Occurred" });

            return RedirectToAction("Reports");
        }
    }
}
