using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.FilesAndRecord;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.FilesAndRecord;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class FilesAndRecordController : BaseController
    {
        private IFilesAndRecordService _FilesAndRecordService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;

        public FilesAndRecordController(IStaffService staffService, IFilesAndRecordService FilesAndRecordService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService) : base(fileUpload)
        {
            _FilesAndRecordService = FilesAndRecordService;
            _clientService = clientService;
            _baseService = baseService;
            _staffService = staffService;

        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _FilesAndRecordService.Get();

            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            List<CreateFilesAndRecord> reports = new List<CreateFilesAndRecord>();
            foreach (GetFilesAndRecord item in entities)
            {
                var report = new CreateFilesAndRecord();
                report.FilesAndRecordId = item.FilesAndRecordId;
                report.Date = item.Date;
                report.Attachment = item.Attachment;
                report.Subject = item.Subject;
                report.StaffName = staff.Where(s=>s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateFilesAndRecord();
            model.ClientId = clientId;

            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);
        }

        public async Task<IActionResult> View(int FilesAndRecordId)
        {
            var putEntity = await GetFilesAndRecord(FilesAndRecordId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int FilesAndRecordId)
        {
            var putEntity = await GetFilesAndRecord(FilesAndRecordId);
            return View(putEntity);
        }
        public async Task<CreateFilesAndRecord> GetFilesAndRecord(int FilesAndRecordId)
        {
            var i = await _FilesAndRecordService.Get(FilesAndRecordId);
            var staff = await _staffService.GetStaffs();
            var putEntity = new CreateFilesAndRecord
            {
                FilesAndRecordId = i.FilesAndRecordId,
                ClientId = i.ClientId,
                Remarks = i.Remarks,
                Date = i.Date,
                Subject = i.Subject,
                Attachment = i.Attachment,
                StaffPersonalInfoId = i.StaffPersonalInfoId,
                StaffList = staff.Select(s=> new SelectListItem(s.Fullname,s.StaffPersonalInfoId.ToString())).ToList()
            };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateFilesAndRecord model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                var staff = await _staffService.GetStaffs();
                model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                return View(model);
            }

            PostFilesAndRecord post = new PostFilesAndRecord();
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "filesandrecord";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            post.FilesAndRecordId = model.FilesAndRecordId;
            post.ClientId = model.ClientId;
            post.Subject = model.Subject;
            post.Remarks = model.Remarks;
            post.Attachment = model.Attachment;
            post.Date = model.Date;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;

            var result = await _FilesAndRecordService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateFilesAndRecord model)
        {
            if (!ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffs();
                var client = await _clientService.GetClient(model.ClientId);
                model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutFilesAndRecord put = new PutFilesAndRecord();
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "filesandrecord";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = model.Attachment;
            }
            put.ClientId = model.ClientId;
            put.Subject = model.Subject;
            put.Remarks = model.Remarks;
            put.Attachment = model.Attachment;
            put.Date = model.Date;
            put.StaffPersonalInfoId = model.StaffPersonalInfoId;
            put.Attachment = model.Attachment;

            var entity = await _FilesAndRecordService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
