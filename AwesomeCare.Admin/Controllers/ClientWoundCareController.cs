﻿using AwesomeCare.Admin.Services.ClientWoundCare;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;
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
    public class ClientWoundCareController : BaseController
    {
        private IClientWoundCareService _clientWoundCareService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientWoundCareController(IClientWoundCareService clientWoundCareService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientWoundCareService = clientWoundCareService;
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
            var entities = await _clientWoundCareService.Get();

            var client = await _clientService.GetClientDetail();
            var baserecord = await _baseService.GetBaseRecordsWithItems();
            List<CreateClientWoundCare> reports = new List<CreateClientWoundCare>();
            foreach (GetClientWoundCare item in entities)
            {
                var report = new CreateClientWoundCare();
                report.WoundCareId = item.WoundCareId;
                report.Reference = item.Reference;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = baserecord.Select(s => s.BaseRecordItems.FirstOrDefault(s => s.BaseRecordItemId == item.Status).ValueName).FirstOrDefault();
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientWoundCare();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int woundId)
        {
            string OfficerToAct = "";
            var WoundCare = await _clientWoundCareService.Get(woundId);
            var staff = _staffService.GetStaffs();
            foreach (var item in WoundCare.OfficerToAct)
            {
                OfficerToAct = OfficerToAct + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname);
            }
            var json = JsonConvert.SerializeObject(WoundCare);
            return View(WoundCare);
        }
        public async Task<IActionResult> Email(int woundId, string sender, string password, string recipient, string Smtp)
        {
            string OfficerToAct = "";
            var WoundCare = await _clientWoundCareService.Get(woundId);
            var staff = _staffService.GetStaffs();
            foreach (var item in WoundCare.OfficerToAct)
            {
                OfficerToAct = OfficerToAct + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname);
            }
            var json = JsonConvert.SerializeObject(WoundCare);
            var newJson = json + OfficerToAct;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientWoundCare.pdf");
            string subject = "ClientWoundCare";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int woundId)
        {
            string OfficerToAct = "";
            var WoundCare = await _clientWoundCareService.Get(woundId);
            var staff = _staffService.GetStaffs();
            foreach (var item in WoundCare.OfficerToAct)
            {
                OfficerToAct = OfficerToAct + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname);
            }
            var json = JsonConvert.SerializeObject(WoundCare);
            var newJson = json + OfficerToAct;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "ClientWoundCare.pdf");
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
        public async Task<IActionResult> Edit(int WoundCareId)
        {
            var WoundCare = _clientWoundCareService.Get(WoundCareId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientWoundCare
            {
                WoundCareId = WoundCare.Result.WoundCareId,
                Reference = WoundCare.Result.Reference,
                ClientId = WoundCare.Result.ClientId,
                Date = WoundCare.Result.Date,
                Time = WoundCare.Result.Time,
                Type = WoundCare.Result.Type,
                Goal = WoundCare.Result.Goal,
                Measurment = WoundCare.Result.Measurment,
                UlcerStage = WoundCare.Result.UlcerStage,
                PainLvl = WoundCare.Result.PainLvl,
                Location = WoundCare.Result.Location,
                WoundCause = WoundCare.Result.WoundCause,
                StatusImage = WoundCare.Result.StatusImage,
                StatusAttach = WoundCare.Result.StatusAttach,
                Comment = WoundCare.Result.Comment,
                StaffName = WoundCare.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = WoundCare.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = WoundCare.Result.PhysicianResponse,
                OfficerToAct = WoundCare.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = WoundCare.Result.Deadline,
                Remarks = WoundCare.Result.Remarks,
                Status = WoundCare.Result.Status,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientWoundCare model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientWoundCare postlog = new PostClientWoundCare();

            #region Attachment
            if (model.StatusAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_StatusAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = "No Image";
            }

            if (model.TypeAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TypeAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TypeAttachment.OpenReadStream());
                model.TypeAttach = path;
            }
            else
            {
                model.TypeAttach = "No Image";
            }

            if (model.LocationAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_LocationAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.LocationAttachment.OpenReadStream());
                model.LocationAttach = path;
            }
            else
            {
                model.LocationAttach = "No Image";
            }

            if (model.UlcerStageAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_UlcerStageAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.UlcerStageAttachment.OpenReadStream());
                model.UlcerStageAttach = path;
            }
            else
            {
                model.UlcerStageAttach = "No Image";
            }

            if (model.MeasurementAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TargetINR_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.MeasurementAttachment.OpenReadStream());
                model.MeasurementAttach = path;
            }
            else
            {
                model.MeasurementAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Type = model.Type;
                postlog.Goal = model.Goal;
                postlog.Measurment = model.Measurment;
                postlog.UlcerStage = model.UlcerStage;
                postlog.PainLvl = model.PainLvl;
                postlog.WoundCause = model.WoundCause;
                postlog.Location = model.Location;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostWoundCareStaffName { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostWoundCarePhysician { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostWoundCareOfficerToAct { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;

            var json = JsonConvert.SerializeObject(postlog);
            var result = await _clientWoundCareService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Wound Care successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientWoundCare model)
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
            if (model.StatusAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_StatusAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }

            if (model.TypeAttachment != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Type_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.TypeAttachment.OpenReadStream());
                model.TypeAttach = pathA;
            }
            else
            {
                model.TypeAttach = model.TypeAttach;
            }

            if (model.LocationAttachment != null)
            {
                string folderB = "clientcomplain";
                string filenameB = string.Concat(folderB, "_Location_", model.ClientId);
                string pathB = await _fileUpload.UploadFile(folderB, true, filenameB, model.LocationAttachment.OpenReadStream());
                model.LocationAttach = pathB;
            }
            else
            {
                model.LocationAttach = model.LocationAttach;
            }

            if (model.UlcerStageAttachment != null)
            {
                string folderC = "clientcomplain";
                string filenameC = string.Concat(folderC, "_UlcerStage_", model.ClientId);
                string pathC = await _fileUpload.UploadFile(folderC, true, filenameC, model.UlcerStageAttachment.OpenReadStream());
                model.UlcerStageAttach = pathC;
            }
            else
            {
                model.UlcerStageAttach = model.UlcerStageAttach;
            }

            if (model.MeasurementAttachment != null)
            {
                string folderD = "clientcomplain";
                string filenameD = string.Concat(folderD, "_Measurement_", model.ClientId);
                string pathD = await _fileUpload.UploadFile(folderD, true, filenameD, model.MeasurementAttachment.OpenReadStream());
                model.MeasurementAttach = pathD;
            }
            else
            {
                model.MeasurementAttach = model.MeasurementAttach;
            }
            #endregion

            PutClientWoundCare put = new PutClientWoundCare();
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Type = model.Type;
            put.Goal = model.Goal;
            put.UlcerStage = model.UlcerStage;
            put.Measurment = model.Measurment;
            put.Location = model.Location;
            put.PainLvl = model.PainLvl;
            put.WoundCause = model.WoundCause;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutWoundCareStaffName { StaffPersonalInfoId = o }).ToList();
            put.Physician = model.Physician.Select(o => new PutWoundCarePhysician { StaffPersonalInfoId = o }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutWoundCareOfficerToAct { StaffPersonalInfoId = o }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;

            var entity = await _clientWoundCareService.Put(put);
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
