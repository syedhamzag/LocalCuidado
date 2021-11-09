using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.TrackingConcernNote;
using AwesomeCare.Admin.ViewModels.TrackingConcernNote;
using AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class TrackingConcernNoteController : BaseController
    {
        private ITrackingConcernNote _trackingConcernNoteService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;

        public TrackingConcernNoteController(ITrackingConcernNote trackingConcernNoteService, IFileUpload fileUpload, IBaseRecordService baseService, IStaffService staffService) : base(fileUpload)
        {
            _trackingConcernNoteService = trackingConcernNoteService;
            _baseService = baseService;
            _staffService = staffService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _trackingConcernNoteService.Get();

            List<CreateTrackingConcernNote> reports = new List<CreateTrackingConcernNote>();
            foreach (GetTrackingConcernNote item in entities)
            {
                var report = new CreateTrackingConcernNote();
                report.Ref = item.Ref;
                report.Attachment = item.Attachment;
                report.DateOfIncident = item.DateOfIncident;
                report.ExpectedDeadline = item.ExpectedDeadline;
                report.DateOfIncident = item.DateOfIncident;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateTrackingConcernNote();

            var staff = await _staffService.GetStaffs();
            model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);
        }

        public async Task<IActionResult> View(int TrackingConcernNoteId)
        {
            var putEntity = await GetTrackingConcernNote(TrackingConcernNoteId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int TrackingConcernNoteId)
        {
            var putEntity = await GetTrackingConcernNote(TrackingConcernNoteId);
            return View(putEntity);
        }
        public async Task<CreateTrackingConcernNote> GetTrackingConcernNote(int TrackingConcernNoteId)
        {
            var c = await _trackingConcernNoteService.Get(TrackingConcernNoteId);
            var staff = await _staffService.GetStaffs();

            var putEntity = new CreateTrackingConcernNote
            {
                Ref = c.Ref,
                ConcernNote = c.ConcernNote,
                Date = c.Date,
                ActionRequired = c.ActionRequired,
                DateOfIncident = c.DateOfIncident,
                ExpectedDeadline = c.ExpectedDeadline,
                StaffNotify = c.StaffNotify,
                ManagerCopied = c.ManagerCopied,
                Remarks = c.Remarks,
                Status = c.Status,
                Attachment = c.Attachment,
                Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateTrackingConcernNote model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffs();
                model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            PostTrackingConcernNote track = new PostTrackingConcernNote();
            if (model.Attach != null)
            {
                string extention = System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "TrackingConcernNote";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            track.Ref = model.Ref;
            track.ConcernNote = model.ConcernNote;
            track.Date = model.Date;
            track.ActionRequired = model.ActionRequired;
            track.DateOfIncident = model.DateOfIncident;
            track.ExpectedDeadline = model.ExpectedDeadline;
            track.StaffNotify = model.StaffNotify;
            track.ManagerCopied = model.ManagerCopied;
            track.Remarks = model.Remarks;
            track.Status = model.Status;
            track.Attachment = model.Attachment;
            track.PostManagerInvolved = model.Manager.Select(s => new PostTrackingConcernManager { StaffPersonalInfoId = s, TrackingConcernNoteId = model.Ref }).ToList();
            track.PostStaffInvolved = model.Staff.Select(s => new PostTrackingConcernStaff { StaffPersonalInfoId = s, TrackingConcernNoteId = model.Ref }).ToList();


            var json = JsonConvert.SerializeObject(track);
            var result = await _trackingConcernNoteService.Create(track);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            return RedirectToAction("Reports");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateTrackingConcernNote model)
        {
            if (!ModelState.IsValid)
            {
                var staff = await _staffService.GetStaffs();
                model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            PutTrackingConcernNote track = new PutTrackingConcernNote();
            if (model.Attach != null)
            {
                string extention = System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "TrackingConcernNote";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = model.Attachment;
            }
            track.Ref = model.Ref;
            track.ConcernNote = model.ConcernNote;
            track.Date = model.Date;
            track.ActionRequired = model.ActionRequired;
            track.DateOfIncident = model.DateOfIncident;
            track.ExpectedDeadline = model.ExpectedDeadline;
            track.StaffNotify = model.StaffNotify;
            track.ManagerCopied = model.ManagerCopied;
            track.Remarks = model.Remarks;
            track.Status = model.Status;
            track.Attachment = model.Attachment;
            track.PutManagerInvolved = model.Manager.Select(s => new PutTrackingConcernManager { StaffPersonalInfoId = s, TrackingConcernNoteId = model.Ref }).ToList();
            track.PutStaffInvolved = model.Staff.Select(s => new PutTrackingConcernStaff { StaffPersonalInfoId = s, TrackingConcernNoteId = model.Ref }).ToList();

            var entity = await _trackingConcernNoteService.Put(track);
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

        public async Task<IActionResult> Download(int TrackingConcernNoteId)
        {
            var entity = await GetTrackingConcernNote(TrackingConcernNoteId);
            var stream = new MemoryStream();
            int row = 1;
            #region Excel Sheet
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(entity.Ref.ToString());
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 5;
                workSheet.Column(3).Width = 5;
                workSheet.Column(4).Width = 15;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 15;
                workSheet.Column(7).Width = 15;
                workSheet.Column(8).Width = 15;
                workSheet.Column(9).Width = 15;

                workSheet.Cells["D1:I1"].Merge = true;
                workSheet.Cells["D1"].Value = "Sheffield City Council - Communities";
                workSheet.Cells["D1"].Style.Font.Bold = true;
                workSheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Untoward Incident";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                row++;
                workSheet.Cells["D" + row].Value = "Please find below untoward incident as detail below:";
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                row++;
                workSheet.Cells["D" + row + ":E" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Service User initials";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row + ":E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["F" + row + ":I" + row].Merge = true;
                workSheet.Cells["F" + row].Value = entity.Ref;
                workSheet.Cells["F" + row].Style.Font.Bold = true;
                workSheet.Cells["F" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["F" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":E" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Care First No.";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row + ":E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["F" + row + ":I" + row].Merge = true;
                workSheet.Cells["F" + row].Value = entity.Ref;
                workSheet.Cells["F" + row].Style.Font.Bold = true;
                workSheet.Cells["F" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["F" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":E" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Team Responsible";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row + ":E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["F" + row + ":I" + row].Merge = true;
                workSheet.Cells["F" + row].Value = "Sheffield Social Services";
                workSheet.Cells["F" + row].Style.Font.Bold = true;
                workSheet.Cells["F" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["F" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":E" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Date Of Incident";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row + ":E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["F" + row + ":I" + row].Merge = true;
                workSheet.Cells["F" + row].Value = entity.DateOfIncident.ToString("dd.MM.yyyy");
                workSheet.Cells["F" + row].Style.Font.Bold = true;
                workSheet.Cells["F" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["F" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row + ":I" + row].Style.WrapText = true;
                workSheet.Cells["D" + row + ":I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                workSheet.Cells["D" + row].Value = "NB: Initials only to be used for Service Users, their family members or any other party mentioned in this Untoward.";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.Font.Color.SetColor(Color.Red);
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Row(row).Height = 35;
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.LightGray;
                workSheet.Cells["D" + row].Value = "Provider:";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row].Value = "Reported By:";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["E" + row + ":F" + row].Merge = true;
                workSheet.Cells["E" + row].Value = entity.Ref;
                workSheet.Cells["E" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["E" + row + ":F" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["G" + row].Value = "Position:";
                workSheet.Cells["G" + row].Style.Font.Bold = true;
                workSheet.Cells["G" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["G" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["H" + row + ":I" + row].Merge = true;
                workSheet.Cells["H" + row].Value = _baseService.GetBaseRecordItemById(entity.Ref).Result.ValueName;
                workSheet.Cells["H" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["H" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row].Value = "Tel:";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["E" + row + ":F" + row].Merge = true;
                workSheet.Cells["E" + row].Value = "";
                workSheet.Cells["E" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["E" + row + ":F" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["G" + row].Value = "Date:";
                workSheet.Cells["G" + row].Style.Font.Bold = true;
                workSheet.Cells["G" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["G" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["H" + row + ":I" + row].Merge = true;
                workSheet.Cells["H" + row].Value = entity.DateOfIncident.ToString("dd.MM.yyyy");
                workSheet.Cells["H" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["H" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row + ":I" + row].Style.WrapText = true;
                workSheet.Cells["D" + row + ":I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                workSheet.Cells["D" + row].Value = entity.Ref;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Row(row).Height = 100;
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Details of action taken:";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row + ":I" + row].Style.WrapText = true;
                workSheet.Cells["D" + row + ":I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                workSheet.Cells["D" + row].Value = entity.Ref;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Row(row).Height = 50;
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row].Value = "Details of any action required by service:";
                workSheet.Cells["D" + row].Style.Font.Bold = true;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                row++;
                workSheet.Cells["D" + row + ":I" + row].Merge = true;
                workSheet.Cells["D" + row + ":I" + row].Style.WrapText = true;
                workSheet.Cells["D" + row + ":I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                workSheet.Cells["D" + row].Value = entity.Ref;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Row(row).Height = 50;
                package.Save();
            }
            #endregion
            stream.Position = 0;
            string excelName = $"Untowatds-.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
