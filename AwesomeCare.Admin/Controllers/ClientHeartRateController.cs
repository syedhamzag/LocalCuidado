﻿using AwesomeCare.Admin.Services.ClientHeartRate;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Buffers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using AwesomeCare.Admin.Services.Staff;
using System.IO;
using OperationStatus = AwesomeCare.Admin.Models.OperationStatus;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using AwesomeCare.Model.Models;
using iText.Kernel.Geom;
using iText.Html2pdf;
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientHeartRateController : BaseController
    {
        private IClientHeartRateService _clientHeartRateService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientHeartRateController(IClientHeartRateService clientHeartRateService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientHeartRateService = clientHeartRateService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _baseService = baseService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientHeartRateService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientHeartRate> reports = new List<CreateClientHeartRate>();
            foreach (GetClientHeartRate item in entities)
            {
                var report = new CreateClientHeartRate();
                report.HeartRateId = item.HeartRateId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.TargetHRAttach = item.TargetHRAttach;
                report.GenderAttach = item.GenderAttach;
                report.SeeChartAttach = item.SeeChartAttach;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientHeartRate();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int HeartId)
        {
            var HeartRate = _clientHeartRateService.Get(HeartId);
            var putEntity = new CreateClientHeartRate
            {
                HeartRateId = HeartRate.Result.HeartRateId,
                Reference = HeartRate.Result.Reference,
                ClientId = HeartRate.Result.ClientId,
                Date = HeartRate.Result.Date,
                Time = HeartRate.Result.Time,
                TargetHR = HeartRate.Result.TargetHR,
                Gender = HeartRate.Result.Gender,
                Age = HeartRate.Result.Age,
                BeatsPerSeconds = HeartRate.Result.BeatsPerSeconds,
                SeeChart = HeartRate.Result.SeeChart,
                Comment = HeartRate.Result.Comment,
                PhysicianResponse = HeartRate.Result.PhysicianResponse,
                Deadline = HeartRate.Result.Deadline,
                Remarks = HeartRate.Result.Remarks,
                Status = HeartRate.Result.Status,
                GenderAttach = HeartRate.Result.GenderAttach,
                SeeChartAttach = HeartRate.Result.SeeChartAttach,
                TargetHRAttach = HeartRate.Result.TargetHRAttach,
                OfficerToAct = HeartRate.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = HeartRate.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = HeartRate.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = HeartRate.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = HeartRate.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = HeartRate.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int HeartId, string sender, string password, string recipient, string Smtp)
        {
            var HeartRate = await _clientHeartRateService.Get(HeartId);
            var json = JsonConvert.SerializeObject(HeartRate);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientHeartRate.pdf");
            string subject = "ClientHeartRate";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public byte[] GeneratePdf(string paragraphs)
        {
            byte[] buffer;
            PdfDocument pdfDoc = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                using (PdfWriter pdfWriter = new PdfWriter(memStream))
                {
                    pdfWriter.SetCloseStream(true);
                    using (pdfDoc = new PdfDocument(pdfWriter))
                    {

                        pdfDoc.SetDefaultPageSize(PageSize.A4);
                        pdfDoc.SetCloseWriter(true);
                        Document document = new Document(pdfDoc);
                        var para = new Paragraph(paragraphs.Replace("{", "").Replace("}", "").Replace("\"", "").Replace(",", "\n"));
                        document.Add(para);
                        buffer = memStream.ToArray();
                        document.Close();
                    }
                }
                buffer = memStream.ToArray();
            }
            return buffer;
        }
        public async Task<IActionResult> Edit(int HeartRateId)
        {
            var HeartRate = _clientHeartRateService.Get(HeartRateId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientHeartRate
            {
                HeartRateId = HeartRate.Result.HeartRateId,
                Reference = HeartRate.Result.Reference,
                ClientId = HeartRate.Result.ClientId,
                Date = HeartRate.Result.Date,
                Time = HeartRate.Result.Time,
                TargetHR = HeartRate.Result.TargetHR,
                Gender = HeartRate.Result.Gender,
                Age = HeartRate.Result.Age,
                BeatsPerSeconds = HeartRate.Result.BeatsPerSeconds,
                SeeChart = HeartRate.Result.SeeChart,
                Comment = HeartRate.Result.Comment,
                StaffName = HeartRate.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = HeartRate.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = HeartRate.Result.PhysicianResponse,
                OfficerToAct = HeartRate.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = HeartRate.Result.Deadline,
                Remarks = HeartRate.Result.Remarks,
                Status = HeartRate.Result.Status,
                GenderAttach = HeartRate.Result.GenderAttach,
                SeeChartAttach = HeartRate.Result.SeeChartAttach,
                TargetHRAttach = HeartRate.Result.TargetHRAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientHeartRate model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientHeartRate postlog = new PostClientHeartRate();

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientheartrate";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream(), model.SeeChartAttachment.ContentType);
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = "No Image";
            }

            if (model.TargetHRAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetHRAttachment.FileName);
                string folder = "clientheartrate";
                string filename = string.Concat(folder, "_TargetHRAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetHRAttachment.OpenReadStream(), model.TargetHRAttachment.ContentType);
                model.TargetHRAttach = path;
            }
            else
            {
                model.TargetHRAttach = "No Image";
            }

            if (model.GenderAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.GenderAttachment.FileName);
                string folder = "clientheartrate";
                string filename = string.Concat(folder, "_GenderAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.GenderAttachment.OpenReadStream(), model.GenderAttachment.ContentType);
                model.GenderAttach = path;
            }
            else
            {
                model.GenderAttach = "No Image";
            }
            #endregion

            
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.TargetHR = model.TargetHR;
                postlog.Gender = model.Gender;
                postlog.Age = model.Age;
                postlog.BeatsPerSeconds = model.BeatsPerSeconds;
                postlog.SeeChart = model.SeeChart;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostHeartRateStaffName { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList(); ;
                postlog.Physician = model.Physician.Select(o => new PostHeartRatePhysician { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostHeartRateOfficerToAct { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.SeeChartAttach = model.SeeChartAttach;
                postlog.TargetHRAttach = model.TargetHRAttach;
                postlog.GenderAttach = model.GenderAttach;

            var result = await _clientHeartRateService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Heart Rate successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientHeartRate model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Status Attachment
            if (model.SeeChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientheartrate";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream(), model.SeeChartAttachment.ContentType);
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = model.SeeChartAttach;
            }

            if (model.TargetHRAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetHRAttachment.FileName);
                string folder = "clientheartrate";
                string filename = string.Concat(folder, "_TargetHRAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetHRAttachment.OpenReadStream(), model.TargetHRAttachment.ContentType);
                model.TargetHRAttach = path;
            }
            else
            {
                model.TargetHRAttach = model.TargetHRAttach;
            }

            if (model.GenderAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.GenderAttachment.FileName);
                string folder = "clientheartrate";
                string filename = string.Concat(folder, "_GenderAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.GenderAttachment.OpenReadStream(), model.GenderAttachment.ContentType);
                model.GenderAttach = path;
            }
            else
            {
                model.GenderAttach = model.GenderAttach;
            }
            #endregion

            PutClientHeartRate put = new PutClientHeartRate();
            put.HeartRateId = model.HeartRateId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.TargetHR = model.TargetHR;
            put.Gender = model.Gender;
            put.Age = model.Age;
            put.BeatsPerSeconds = model.BeatsPerSeconds;
            put.SeeChart = model.SeeChart;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutHeartRateStaffName { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList(); ;
            put.Physician = model.Physician.Select(o => new PutHeartRatePhysician { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList(); ;
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutHeartRateOfficerToAct { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.SeeChartAttach = model.SeeChartAttach;
            put.TargetHRAttach = model.TargetHRAttach;
            put.GenderAttach = model.GenderAttach;

            var entity = await _clientHeartRateService.Put(put);
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
        public async Task<IActionResult> Download(int HeartId)
        {
            var entity = await GetDownload(HeartId);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
        public async Task<CreateClientHeartRate> GetDownload(int Id)
        {
            var i = await _clientHeartRateService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateClientHeartRate
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                Reference = i.Reference,
                Date = i.Date,
                SeeChartAttach = i.SeeChartAttach,
                Time = i.Time,
                TargetHRAttach = i.TargetHRAttach,
                GenderAttach = i.GenderAttach,
                Comment = i.Comment,
                Deadline = i.Deadline,
                PhysicianResponse = i.PhysicianResponse,
                Remarks = i.Remarks,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
                TargetHRName = _baseService.GetBaseRecordItemById(i.TargetHR).Result.ValueName,
                GenderName = _baseService.GetBaseRecordItemById(i.Gender).Result.ValueName,
                AgeName = _baseService.GetBaseRecordItemById(i.Age).Result.ValueName,
                BeatsPerSecondsName = _baseService.GetBaseRecordItemById(i.BeatsPerSeconds).Result.ValueName,
                SeeChartName = _baseService.GetBaseRecordItemById(i.SeeChart).Result.ValueName,
            };
            foreach (var item in i.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.OfficerToActName))
                    putEntity.OfficerToActName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.OfficerToActName = putEntity.OfficerToActName + ", " + staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
            return putEntity;
        }
    }
}
