﻿using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.DutyOnCall;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Http;

namespace AwesomeCare.Admin.Controllers
{
    public class DutyOnCallController : BaseController
    {
        private IDutyOnCallService _dutyoncallService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;
        private IMailChimpService _emailService;

        public DutyOnCallController(IDutyOnCallService dutyoncallService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService, IStaffService staffService,
            IMailChimpService emailService) : base(fileUpload)
        {
            _dutyoncallService = dutyoncallService;
            _clientService = clientService;
            _baseService = baseService;
            _staffService = staffService;
            _emailService = emailService;
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
        public async Task<IActionResult> Email(int DutyOnCallId)
        {
            var email = new List<DataTransferObject.Models.MailChimp.Recipient>();
            var attachments = new List<IFormFile>();
            var entity = await GetDutyDownload(DutyOnCallId);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            email.Add(new DataTransferObject.Models.MailChimp.Recipient { Name = client.FullName,Type = DataTransferObject.Enums.EmailTypeEnum.to, Email = client.Email });
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            IFormFile file = new FormFile(stream, 0, stream.Length, null, client.FullName + ".docx")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            };
            attachments.Add(file);
            await _emailService.SendAsync($"{client.FullName}", "", false, email,attachments);
            stream.Close();
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = entity.ClientId });
        }
        public async Task<IActionResult> Download(int DutyOnCallId)
        {
            var entity = await GetDutyDownload(DutyOnCallId);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);

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
        public async Task<CreateDutyOnCall> GetDutyDownload(int DutyOnCallId)
        {
            var i = await _dutyoncallService.Get(DutyOnCallId);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateDutyOnCall
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s=>s.ClientId==i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s=>s.ClientId==i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s=>s.ClientId==i.ClientId).FirstOrDefault().DateOfBirth,
                Remarks = i.Remarks,
                NotificationStatusName = _baseService.GetBaseRecordItemById(i.NotificationStatus).Result.ValueName,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
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
                PositionOfReportingName = _baseService.GetBaseRecordItemById(i.PositionOfReporting).Result.ValueName,
                PriorityName = _baseService.GetBaseRecordItemById(i.Priority).Result.ValueName,
                RefNo = i.RefNo,
                ReportedBy = i.ReportedBy,
                StaffBlacklisted = i.StaffBlacklisted,
                Subject = i.Subject,
                TelephoneToCallName = _baseService.GetBaseRecordItemById(i.TelephoneToCall).Result.ValueName,
                TimeOfCall = i.TimeOfCall,
                TypeOfDutyCallName = _baseService.GetBaseRecordItemById(i.TypeOfDutyCall).Result.ValueName,
                TypeOfIncidentName = _baseService.GetBaseRecordItemById(i.TypeOfIncident).Result.ValueName,
            };
            foreach (var item in i.PersonResponsible.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.PersonResponsibleName))
                    putEntity.PersonResponsibleName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.PersonResponsibleName = putEntity.PersonResponsibleName +", "+ staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
            foreach (var item in i.PersonToAct.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.PersonToActName))
                    putEntity.PersonToActName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.PersonToActName = putEntity.PersonToActName +", "+ staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
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
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
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
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
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
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }

        
    }
}
