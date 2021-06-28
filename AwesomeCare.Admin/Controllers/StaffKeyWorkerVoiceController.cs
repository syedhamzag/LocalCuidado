﻿using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffKeyWorkerVoice;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Client;
using System.Net.Mail;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using OperationStatus = AwesomeCare.Admin.Models.OperationStatus;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using AwesomeCare.Model.Models;
using iText.Kernel.Geom;
using iText.Html2pdf;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffKeyWorkerVoiceController : BaseController
    {
        private IStaffKeyWorkerVoiceService _StaffKeyWorkerVoiceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffKeyWorkerVoiceController(IStaffKeyWorkerVoiceService StaffKeyWorkerVoiceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffKeyWorkerVoiceService = StaffKeyWorkerVoiceService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffKeyWorkerVoiceService.Get();
            return View(entities);
        }
        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffKeyWorkerVoice();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetClients = clientNames;
            return View(model);

        }
        public async Task<IActionResult> View(int workerId)
        {
            var keyWorker = await _StaffKeyWorkerVoiceService.Get(workerId);
            return View(keyWorker);
        }
        public async Task<IActionResult> Email(int workerId, string sender, string password, string recipient, string Smtp)
        {
            var keyWorker = await _StaffKeyWorkerVoiceService.Get(workerId);
            var json = JsonConvert.SerializeObject(keyWorker);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffKeyWorkerVoice.pdf");
            string subject = "StaffKeyWorkerVoice";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int workerId)
        {
            var keyWorker = await _StaffKeyWorkerVoiceService.Get(workerId);
            var json = JsonConvert.SerializeObject(keyWorker);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "StaffKeyWorkerVoice.pdf");
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
        public async Task<IActionResult> Edit(int workerId)
        {
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetClients = clientNames;
            var staffs = await _staffService.GetStaffs();
            var KeyWorker = _StaffKeyWorkerVoiceService.Get(workerId);

            var putEntity = new CreateStaffKeyWorkerVoice
            {
                KeyWorkerId = KeyWorker.Result.KeyWorkerId,
                Attachment = KeyWorker.Result.Attachment,
                Date = KeyWorker.Result.Date,
                Deadline = KeyWorker.Result.Deadline,
                URL = KeyWorker.Result.URL,
                Remarks = KeyWorker.Result.Remarks,
                Status = KeyWorker.Result.Status,
                ActionRequired = KeyWorker.Result.ActionRequired,
                NextCheckDate = KeyWorker.Result.NextCheckDate,
                ChangesWeNeed = KeyWorker.Result.ChangesWeNeed,
                Details = KeyWorker.Result.Details,
                NotComfortableServices = KeyWorker.Result.NotComfortableServices,
                MovingAndHandling = KeyWorker.Result.MovingAndHandling,
                OfficerToAct  = KeyWorker.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                ServicesRequiresTime = KeyWorker.Result.ServicesRequiresTime,
                MedicationChanges = KeyWorker.Result.MedicationChanges,
                NutritionalChanges = KeyWorker.Result.NutritionalChanges,
                RiskAssessment = KeyWorker.Result.RiskAssessment,
                ServicesRequiresServices = KeyWorker.Result.ServicesRequiresServices,
                WellSupportedServices = KeyWorker.Result.WellSupportedServices,
                TeamYouWorkFor = KeyWorker.Result.Workteam.Select(s=>s.StaffPersonalInfoId).ToList(),
                StaffId = KeyWorker.Result.StaffId,                
                HealthAndWellNessChanges = KeyWorker.Result.HealthAndWellNessChanges,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffKeyWorkerVoice model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetClients = clientNames;
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
            }
            else
            { model.Attachment = "No Image"; }
            #endregion
                var post = new PostStaffKeyWorkerVoice();
                post.Reference = model.Reference;
                post.Attachment =model.Attachment;
                post.Date =model.Date;
                post.Deadline =model.Deadline;
                post.URL =model.URL;
                post.Remarks =model.Remarks;
                post.Status =model.Status;
                post.ActionRequired =model.ActionRequired;
                post.NextCheckDate =model.NextCheckDate;
                post.ChangesWeNeed =model.ChangesWeNeed;
                post.Details =model.Details;
                post.NotComfortableServices =model.NotComfortableServices;
                post.MovingAndHandling =model.MovingAndHandling;
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostKeyWorkerOfficerToAct { StaffPersonalInfoId = o, KeyWorkerId = model.KeyWorkerId }).ToList();
                post.ServicesRequiresTime =model.ServicesRequiresTime;
                post.MedicationChanges =model.MedicationChanges;
                post.NutritionalChanges =model.NutritionalChanges;
                post.RiskAssessment =model.RiskAssessment;
                post.ServicesRequiresServices =model.ServicesRequiresServices;
                post.WellSupportedServices =model.WellSupportedServices;
                post.Workteam = model.TeamYouWorkFor.Select(o => new PostKeyWorkerWorkteam { StaffPersonalInfoId = o, KeyWorkerId = model.KeyWorkerId }).ToList();
                post.StaffId =model.StaffId;                
                post.HealthAndWellNessChanges =model.HealthAndWellNessChanges;

                var result = await _StaffKeyWorkerVoiceService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Key worker Voice successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffKeyWorkerVoice model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetClients = clientNames;
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion

                var put = new PutStaffKeyWorkerVoice();
                put.KeyWorkerId = model.KeyWorkerId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.ChangesWeNeed = model.ChangesWeNeed;
                put.Details = model.Details;
                put.NotComfortableServices = model.NotComfortableServices;
                put.MovingAndHandling = model.MovingAndHandling;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutKeyWorkerOfficerToAct { StaffPersonalInfoId = o, KeyWorkerId = model.KeyWorkerId }).ToList();
                put.ServicesRequiresTime = model.ServicesRequiresTime;
                put.MedicationChanges = model.MedicationChanges;
                put.NutritionalChanges = model.NutritionalChanges;
                put.RiskAssessment = model.RiskAssessment;
                put.ServicesRequiresServices = model.ServicesRequiresServices;
                put.WellSupportedServices = model.WellSupportedServices;
                put.Workteam = model.TeamYouWorkFor.Select(o => new PutKeyWorkerWorkteam { StaffPersonalInfoId = o, KeyWorkerId = model.KeyWorkerId }).ToList();
                put.StaffId = model.StaffId;
                put.HealthAndWellNessChanges = model.HealthAndWellNessChanges;

            var entity = await _StaffKeyWorkerVoiceService.Put(put);
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
