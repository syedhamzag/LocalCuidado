using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.TaskBoard;
using AwesomeCare.Admin.ViewModels.TaskBoard;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class TaskBoardController : BaseController
    {
        private ITaskBoardService _taskService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;

        public TaskBoardController(IFileUpload fileUpload, ITaskBoardService taskService, IStaffService staffService, IBaseRecordService baseService):base(fileUpload)
        {
            _taskService = taskService;
            _staffService = staffService;
            _baseService = baseService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _taskService.Get();

            var staffs = await _staffService.GetStaffs();
            List<CreateTaskBoard> reports = new List<CreateTaskBoard>();
            foreach (GetTaskBoard item in entities)
            {
                var report = new CreateTaskBoard();
                report.TaskId = item.TaskId;
                report.CompletionDate = item.CompletionDate;
                report.StaffName = staffs.Where(s => s.StaffPersonalInfoId == item.AssignedBy).FirstOrDefault().Fullname;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }
        public async Task<IActionResult> Index()
        {
            var model = new CreateTaskBoard();
            var staffs = await _staffService.GetStaffs();
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int taskId)
        {
            var putEntity = await GetInfectionControl(taskId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int taskId)
        {
            var putEntity = await GetInfectionControl(taskId);
            return View(putEntity);
        }

        public async Task<CreateTaskBoard> GetInfectionControl(int taskId)
        {
            var staffs = await _staffService.GetStaffs();
            var model = await _taskService.Get(taskId);
            var putEntity = new CreateTaskBoard
            {
            AssignedBy = model.AssignedBy,
            AssignedTo = model.AssignedTo.Select(o => o.StaffPersonalInfoId).ToList(),
            Status = model.Status,
            Attachment = model.Attachment,
            CompletionDate = model.CompletionDate,
            Note = model.Note,
            TaskImage = model.TaskImage,
            TaskName = model.TaskName,
            StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return putEntity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateTaskBoard model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostTaskBoard postlog = new PostTaskBoard();

            #region Evidence
            if (model.Image != null)
            {
                string extention = model.AssignedBy + System.IO.Path.GetExtension(model.Image.FileName);
                string folder = "taskboard";
                string filename = string.Concat(folder, "_TaskImage_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Image.OpenReadStream());
                model.TaskImage = path;
            }
            else
            {
                model.TaskImage = "No Image";
            }
            if (model.Attach != null)
            {
                string extention = model.AssignedBy + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "taskboard";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

            postlog.AssignedBy = model.AssignedBy;
            postlog.AssignedTo = model.AssignedTo.Select(o => new PostTaskBoardAssignedTo { StaffPersonalInfoId = o, TaskBoardId = model.TaskId }).ToList();
            postlog.Status = model.Status;
            postlog.Attachment = model.Attachment;
            postlog.CompletionDate = model.CompletionDate;
            postlog.Note = model.Note;
            postlog.Status = model.Status;
            postlog.TaskImage = model.TaskImage;
            postlog.TaskName = model.TaskName;
            


            var result = await _taskService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Task Board successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports", "TaskBoard");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateTaskBoard model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PutTaskBoard postlog = new PutTaskBoard();

            #region Evidence
            if (model.Image != null)
            {
                string extention = model.AssignedBy + System.IO.Path.GetExtension(model.Image.FileName);
                string folder = "taskboard";
                string filename = string.Concat(folder, "_TaskImage_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Image.OpenReadStream());
                model.TaskImage = path;
            }
            else
            {
                model.TaskImage = model.TaskImage;
            }
            if (model.Attach != null)
            {
                string extention = model.AssignedBy + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "taskboard";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = model.TaskImage;
            }
            #endregion
            postlog.TaskId = model.TaskId;
            postlog.AssignedBy = model.AssignedBy;
            postlog.AssignedTo = model.AssignedTo.Select(o => new PutTaskBoardAssignedTo { StaffPersonalInfoId = o, TaskBoardId = model.TaskId }).ToList();
            postlog.Status = model.Status;
            postlog.Attachment = model.Attachment;
            postlog.CompletionDate = model.CompletionDate;
            postlog.Note = model.Note;
            postlog.Status = model.Status;
            postlog.TaskImage = model.TaskImage;
            postlog.TaskName = model.TaskName;



            var result = await _taskService.Put(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Task Board successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports", "TaskBoard");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int clientId, string pin)
        {
            var getmodal = await _taskService.GetPin();
            if (pin != getmodal.Pin)
                return RedirectToAction("HomeCare", "Client");
            return RedirectToAction("Index", new { clientId = clientId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string pin, List<int> Ids)
        {
            var getmodal = await _taskService.GetPin();
            if (pin != getmodal.Pin)
                return RedirectToAction("Reports");

            List<GetTaskBoard> model = new List<GetTaskBoard>();
            foreach (var item in Ids)
            {
                GetTaskBoard getTask = new GetTaskBoard();
                getTask.TaskId = item;
                model.Add(getTask);
            }
            var result = await _taskService.Delete(model);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "Task Board successfully deleted" : "An Error Occurred" });
            return RedirectToAction("Dashboard", "Dashboard");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePin(string newPin, string oldPin)
        {
            var getmodal = await _taskService.GetPin();
            if (getmodal.Pin != oldPin)
                return RedirectToAction("BaseRecord", "Admin");
            var model = new PostRotaPin();
            model.PinId = getmodal.PinId;
            model.Pin = newPin;
            var result = await _taskService.ChangePin(model);

            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
