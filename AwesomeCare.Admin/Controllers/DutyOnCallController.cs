using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.DutyOnCall;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
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
    public class DutyOnCallController : BaseController
    {
        private IDutyOnCallService _dutyoncallService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;

        public DutyOnCallController(IDutyOnCallService dutyoncallService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService, IStaffService staffService) : base(fileUpload)
        {
            _dutyoncallService = dutyoncallService;
            _clientService = clientService;
            _baseService = baseService;
            _staffService = staffService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _dutyoncallService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateDutyOnCall> reports = new List<CreateDutyOnCall>();
            foreach (GetDutyOnCall item in entities)
            {
                var report = new CreateDutyOnCall();
                report.DutyOnCallId = item.DutyOnCallId;
                report.Attachment = item.Attachment;
                report.RefNo = item.RefNo;
                report.Subject = item.Subject;
                report.ClientId = item.ClientId;
                report.DateOfCall = item.DateOfCall;
                report.DateOfIncident = item.DateOfIncident;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.PriorityName = _baseService.GetBaseRecordItemById(item.Priority).Result.ValueName;
                report.NotificationStatusName = _baseService.GetBaseRecordItemById(item.NotificationStatus).Result.ValueName;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateDutyOnCall();
            model.ClientId = clientId;
            
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var staff = await _staffService.GetStaffs();
            model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);
        }

        public async Task<IActionResult> View(int DutyOnCallId)
        {
            var putEntity = await GetDutyOnCall(DutyOnCallId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int DutyOnCallId)
        {
            var putEntity = await GetDutyOnCall(DutyOnCallId);
            return View(putEntity);
        }
        public async Task<CreateDutyOnCall> GetDutyOnCall(int DutyOnCallId)
        {
            var i = await _dutyoncallService.Get(DutyOnCallId);
            var staff = await _staffService.GetStaffs();
            
            var putEntity = new CreateDutyOnCall
            {
                ClientId = i.ClientId,
                Remarks = i.Remarks,
                NotificationStatus = i.NotificationStatus,
                Status = i.Status,
                ActionTaken = i.ActionTaken,
                Attachment = i.Attachment,
                ClientInitial = i.ClientInitial,
                DateOfCall = i.DateOfCall,
                DateOfIncident = i.DateOfIncident,
                DetailsOfIncident = i.DetailsOfIncident,
                DetailsRequired = i.DetailsRequired,
                DutyOnCallId = i.DutyOnCallId,
                NotifyPerson = i.NotifyPerson,
                NotifyStaffInvolved = i.NotifyStaffInvolved,
                PersonResponsible = i.PersonResponsible.Select(s => s.StaffPersonalInfoId).ToList(),
                PersonToAct = i.PersonToAct.Select(s=>s.StaffPersonalInfoId).ToList(),
                PositionOfReporting = i.PositionOfReporting,
                Priority = i.Priority,
                RefNo = i.RefNo,
                ReportedBy = i.ReportedBy,
                StaffBlacklisted = i.StaffBlacklisted,
                Subject = i.Subject,
                TelephoneToCall = i.TelephoneToCall,
                TimeOfCall = i.TimeOfCall,
                TypeOfDutyCall = i.TypeOfDutyCall,
                TypeOfIncident = i.TypeOfIncident,
                Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateDutyOnCall model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                var staff = await _staffService.GetStaffs();
                model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            PostDutyOnCall duty = new PostDutyOnCall();
            if (model.Attach != null)
            {
                string extention = model.RefNo + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "dutyoncall";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            duty.ClientId = model.ClientId;
            duty.Remarks = model.Remarks;
            duty.Status = model.Status;
            duty.NotificationStatus = model.NotificationStatus;
            duty.ActionTaken = model.ActionTaken;
            duty.Attachment = model.Attachment;
            duty.ClientInitial = model.ClientInitial;
            duty.DateOfCall = model.DateOfCall;
            duty.DateOfIncident = model.DateOfIncident;
            duty.DetailsOfIncident = model.DetailsOfIncident;
            duty.DetailsRequired = model.DetailsRequired;
            duty.DutyOnCallId = model.DutyOnCallId;
            duty.NotifyPerson = model.NotifyPerson;
            duty.NotifyStaffInvolved = model.NotifyStaffInvolved;
            duty.PersonResponsible = model.PersonResponsible.Select(s => new PostDutyOnCallPersonResponsible { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId}).ToList();
            duty.PersonToAct = model.PersonToAct.Select(s => new PostDutyOnCallPersonToAct { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId}).ToList();
            duty.PositionOfReporting = model.PositionOfReporting;
            duty.Priority = model.Priority;
            duty.RefNo = model.RefNo;
            duty.ReportedBy = model.ReportedBy;
            duty.StaffBlacklisted = model.StaffBlacklisted;
            duty.Subject = model.Subject;
            duty.TelephoneToCall = model.TelephoneToCall;
            duty.TimeOfCall = model.TimeOfCall;
            duty.TypeOfDutyCall = model.TypeOfDutyCall;
            duty.TypeOfIncident = model.TypeOfIncident;

            var json = JsonConvert.SerializeObject(duty);
            var result = await _dutyoncallService.Create(duty);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateDutyOnCall model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staff = await _staffService.GetStaffs();
                model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            PutDutyOnCall duty = new PutDutyOnCall();
            if (model.Attach != null)
            {
                string extention = model.RefNo + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "dutyoncall";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = model.Attachment;
            }
            duty.ClientId = model.ClientId;
            duty.Remarks = model.Remarks;
            duty.Status = model.Status;
            duty.NotificationStatus = model.NotificationStatus;
            duty.ActionTaken = model.ActionTaken;
            duty.Attachment = model.Attachment;
            duty.ClientInitial = model.ClientInitial;
            duty.DateOfCall = model.DateOfCall;
            duty.DateOfIncident = model.DateOfIncident;
            duty.DetailsOfIncident = model.DetailsOfIncident;
            duty.DetailsRequired = model.DetailsRequired;
            duty.DutyOnCallId = model.DutyOnCallId;
            duty.NotifyPerson = model.NotifyPerson;
            duty.NotifyStaffInvolved = model.NotifyStaffInvolved;
            duty.PersonResponsible = model.PersonResponsible.Select(s => new PutDutyOnCallPersonResponsible { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId }).ToList();
            duty.PersonToAct = model.PersonToAct.Select(s => new PutDutyOnCallPersonToAct { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId }).ToList();
            duty.PositionOfReporting = model.PositionOfReporting;
            duty.Priority = model.Priority;
            duty.RefNo = model.RefNo;
            duty.ReportedBy = model.ReportedBy;
            duty.StaffBlacklisted = model.StaffBlacklisted;
            duty.Subject = model.Subject;
            duty.TelephoneToCall = model.TelephoneToCall;
            duty.TimeOfCall = model.TimeOfCall;
            duty.TypeOfDutyCall = model.TypeOfDutyCall;
            duty.TypeOfIncident = model.TypeOfIncident;

            var entity = await _dutyoncallService.Put(duty);
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

        public async Task<IActionResult> Download(int DutyOnCallId)
        {
            var entity = await GetDutyOnCall(DutyOnCallId);
            var client = await _clientService.GetClientDetail();
            var stream = new MemoryStream();
            int row = 1;
            #region Excel Sheet
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(entity.RefNo.ToString());
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
                workSheet.Cells["D"+row+":I"+row].Merge = true;
                workSheet.Cells["D"+row].Value = "Untoward Incident";
                workSheet.Cells["D"+row].Style.Font.Bold = true;
                workSheet.Cells["D"+row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
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
                workSheet.Cells["F" + row].Value = entity.ClientInitial;
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
                workSheet.Cells["F" + row].Value = entity.RefNo;
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
                workSheet.Cells["E" + row].Value = entity.ReportedBy;
                workSheet.Cells["E" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["E" + row + ":F" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["G" + row].Value = "Position:";
                workSheet.Cells["G" + row].Style.Font.Bold = true;
                workSheet.Cells["G" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["G" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Cells["H" + row + ":I" + row].Merge = true;
                workSheet.Cells["H" + row].Value = _baseService.GetBaseRecordItemById(entity.PositionOfReporting).Result.ValueName;
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
                workSheet.Cells["D" + row].Value = entity.DetailsOfIncident;
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
                workSheet.Cells["D" + row].Value = entity.ActionTaken;
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
                workSheet.Cells["D" + row].Value = entity.DetailsRequired;
                workSheet.Cells["D" + row].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                workSheet.Cells["D" + row + ":I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                workSheet.Row(row).Height = 50;
                package.Save();
            }
            #endregion
            stream.Position = 0;
            string excelName = $"Untowatds-{client.FirstOrDefault(s=>s.ClientId == entity.ClientId).FullName}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
