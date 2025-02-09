﻿using AwesomeCare.Admin.Services.ClientPulseRate;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientPulseRate;
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
    public class ClientPulseRateController : BaseController
    {
        private IClientPulseRateService _clientPulseRateService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientPulseRateController(IClientPulseRateService clientPulseRateService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientPulseRateService = clientPulseRateService;
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
            var entities = await _clientPulseRateService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientPulseRate> reports = new List<CreateClientPulseRate>();
            foreach (GetClientPulseRate item in entities)
            {
                var report = new CreateClientPulseRate();
                report.PulseRateId = item.PulseRateId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.TargetPulseAttach = item.TargetPulseAttach;
                report.SeeChartAttach = item.SeeChartAttach;
                report.Chart = item.Chart;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientPulseRate();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int pulseId)
        {
            var PulseRate =  _clientPulseRateService.Get(pulseId);
            var putEntity = new CreateClientPulseRate
            {
                PulseRateId = PulseRate.Result.PulseRateId,
                ClientId = PulseRate.Result.ClientId,
                Reference = PulseRate.Result.Reference,
                Date = PulseRate.Result.Date,
                Time = PulseRate.Result.Time,
                TargetPulse = PulseRate.Result.TargetPulse,
                CurrentPulse = PulseRate.Result.CurrentPulse,
                SeeChart = PulseRate.Result.SeeChart,
                Comment = PulseRate.Result.Comment,
                PhysicianResponse = PulseRate.Result.PhysicianResponse,
                Deadline = PulseRate.Result.Deadline,
                Remarks = PulseRate.Result.Remarks,
                Status = PulseRate.Result.Status,
                SeeChartAttach = PulseRate.Result.SeeChartAttach,
                TargetPulseAttach = PulseRate.Result.TargetPulseAttach,
                Chart = PulseRate.Result.Chart,
                OfficerToAct = PulseRate.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = PulseRate.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = PulseRate.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = PulseRate.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = PulseRate.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = PulseRate.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int pulseId, string sender, string password, string recipient, string Smtp)
        {
            var PulseRate = await _clientPulseRateService.Get(pulseId);
            var json = JsonConvert.SerializeObject(PulseRate);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientPulseRate.pdf");
            string subject = "ClientPulseRate";
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
        public async Task<IActionResult> Edit(int PulseRateId)
        {
            var PulseRate = _clientPulseRateService.Get(PulseRateId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientPulseRate
            {
                PulseRateId = PulseRate.Result.PulseRateId,
                Reference = PulseRate.Result.Reference,
                ClientId = PulseRate.Result.ClientId,
                Date = PulseRate.Result.Date,
                Time = PulseRate.Result.Time,
                TargetPulse = PulseRate.Result.TargetPulse,
                CurrentPulse = PulseRate.Result.CurrentPulse,
                SeeChart = PulseRate.Result.SeeChart,
                Comment = PulseRate.Result.Comment,
                StaffName = PulseRate.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = PulseRate.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = PulseRate.Result.PhysicianResponse,
                OfficerToAct = PulseRate.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = PulseRate.Result.Deadline,
                Remarks = PulseRate.Result.Remarks,
                Status = PulseRate.Result.Status,
                SeeChartAttach = PulseRate.Result.SeeChartAttach,
                TargetPulseAttach = PulseRate.Result.TargetPulseAttach,
                Chart = PulseRate.Result.Chart,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientPulseRate model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientPulseRate postlog = new PostClientPulseRate();

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientpulserate";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream(), model.SeeChartAttachment.ContentType);
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = "No Image";
            }

            if (model.ChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.ChartAttachment.FileName);
                string folder = "clientpulserate";
                string filename = string.Concat(folder, "_ChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ChartAttachment.OpenReadStream(),model.ChartAttachment.ContentType);
                model.Chart = path;
            }
            else
            {
                model.Chart = "No Image";
            }

            if (model.TargetPulseAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetPulseAttachment.FileName);
                string folder = "clientpulserate";
                string filename = string.Concat(folder, "_TargetPulseAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetPulseAttachment.OpenReadStream(),model.TargetPulseAttachment.ContentType);
                model.TargetPulseAttach = path;
            }
            else
            {
                model.TargetPulseAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.TargetPulse = model.TargetPulse;
                postlog.CurrentPulse = model.CurrentPulse;
                postlog.SeeChart = model.SeeChart;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostPulseRateStaffName { StaffPersonalInfoId = o, PulseRateId = model.PulseRateId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostPulseRatePhysician { StaffPersonalInfoId = o, PulseRateId = model.PulseRateId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostPulseRateOfficerToAct { StaffPersonalInfoId = o, PulseRateId = model.PulseRateId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.SeeChartAttach = model.SeeChartAttach;
                postlog.TargetPulseAttach = model.TargetPulseAttach;
                postlog.Chart = model.Chart;

            var result = await _clientPulseRateService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Pulse Rate successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientPulseRate model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientpulserate";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream(),model.SeeChartAttachment.ContentType);
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = model.SeeChartAttach;
            }

            if (model.ChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.ChartAttachment.FileName);
                string folder = "clientpulserate";
                string filename = string.Concat(folder, "_ChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ChartAttachment.OpenReadStream(), model.ChartAttachment.ContentType);
                model.Chart = path;
            }
            else
            {
                model.Chart = model.Chart;
            }

            if (model.TargetPulseAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetPulseAttachment.FileName);
                string folder = "clientpulserate";
                string filename = string.Concat(folder, "_TargetPulseAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetPulseAttachment.OpenReadStream(), model.TargetPulseAttachment.ContentType);
                model.TargetPulseAttach = path;
            }
            else
            {
                model.TargetPulseAttach = model.TargetPulseAttach;
            }
            #endregion

            PutClientPulseRate put = new PutClientPulseRate();
            put.PulseRateId = model.PulseRateId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.TargetPulse = model.TargetPulse;
            put.CurrentPulse = model.CurrentPulse;
            put.SeeChart = model.SeeChart;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutPulseRateStaffName { StaffPersonalInfoId = o, PulseRateId = model.PulseRateId }).ToList();
            put.Physician = model.Physician.Select(o => new PutPulseRatePhysician { StaffPersonalInfoId = o, PulseRateId = model.PulseRateId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutPulseRateOfficerToAct { StaffPersonalInfoId = o, PulseRateId = model.PulseRateId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.SeeChartAttach = model.SeeChartAttach;
            put.TargetPulseAttach = model.TargetPulseAttach;
            put.Chart = model.Chart;

            var entity = await _clientPulseRateService.Put(put);
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
        public async Task<IActionResult> Download(int pulseId)
        {
            var entity = await GetDownload(pulseId);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
        public async Task<CreateClientPulseRate> GetDownload(int Id)
        {
            var i = await _clientPulseRateService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateClientPulseRate
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                Reference = i.Reference,
                Date = i.Date,
                Time = i.Time,
                TargetPulseAttach = i.TargetPulseAttach,
                CurrentPulse = i.CurrentPulse,
                Chart = i.Chart,
                SeeChartAttach = i.SeeChartAttach,
                Comment = i.Comment,
                PhysicianResponse = i.PhysicianResponse,
                Deadline = i.Deadline,
                Remarks = i.Remarks,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
                TargetPulseName = _baseService.GetBaseRecordItemById(i.TargetPulse).Result.ValueName,
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
