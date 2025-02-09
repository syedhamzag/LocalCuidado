﻿using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSurvey;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
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
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffSurveyController : BaseController
    {
        private IStaffSurveyService _StaffSurveyService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private IBaseRecordService _baseService;

        public StaffSurveyController(IStaffSurveyService StaffSurveyService, IFileUpload fileUpload, IStaffService staffService, IEmailService emailService,
            IBaseRecordService baseService) : base(fileUpload)
        {
            _StaffSurveyService = StaffSurveyService;
            _staffService = staffService;
            _emailService = emailService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSurveyService.Get();
            var staff = await _staffService.GetStaffs();
            List<CreateStaffSurvey> reports = new List<CreateStaffSurvey>();
            foreach (GetStaffSurvey item in entities)
            {
                var report = new CreateStaffSurvey();
                report.StaffSurveyId = item.StaffSurveyId;
                report.Date = item.Date;
                report.NextCheckDate = item.NextCheckDate;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffId).Select(s => s.Fullname).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.Attachment = item.Attachment;
                reports.Add(report);
            }
            return View(reports);
        }
        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffSurvey();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.StaffId = staffId.Value;
            return View(model);

        }
        public async Task<IActionResult> View(int surveyId)
        {
            var Survey = _StaffSurveyService.Get(surveyId);
            var putEntity = new CreateStaffSurvey
            {
                Attachment = Survey.Result.Attachment,
                Reference = Survey.Result.Reference,
                Date = Survey.Result.Date,
                Deadline = Survey.Result.Deadline,
                URL = Survey.Result.URL,
                OfficerName = Survey.Result.OfficerToAct.Select(s => s.StaffName).ToList(),
                Remarks = Survey.Result.Remarks,
                Status = Survey.Result.Status,
                ActionRequired = Survey.Result.ActionRequired,
                NextCheckDate = Survey.Result.NextCheckDate,
                WorkteamName = Survey.Result.Workteam.Select(s => s.StaffName).ToList(),
                StaffId = Survey.Result.StaffId,
                AccessToPolicies = Survey.Result.AccessToPolicies,
                AdequateTrainingReceived = Survey.Result.AdequateTrainingReceived,
                AreaRequiringImprovements = Survey.Result.AreaRequiringImprovements,
                CompanyManagement = Survey.Result.CompanyManagement,
                HealthCareServicesSatisfaction = Survey.Result.HealthCareServicesSatisfaction,
                SupportFromCompany = Survey.Result.SupportFromCompany,
                WorkEnvironmentSuggestions = Survey.Result.WorkEnvironmentSuggestions,
                Details = Survey.Result.Details,
                OfficerToAct = Survey.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                WorkTeam = Survey.Result.Workteam.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = Survey.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                WorkteamList = Survey.Result.Workteam.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int surveyId, string sender, string password, string recipient, string Smtp)
        {
            var survey = await _StaffSurveyService.Get(surveyId);
            var json = JsonConvert.SerializeObject(survey);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffSurvey.pdf");
            string subject = "StaffSurvey";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int surveyId)
        {
            var survey = await _StaffSurveyService.Get(surveyId);
            var staff = _staffService.GetStaffs();
            var json = JsonConvert.SerializeObject(survey);
            byte[] byte1 = GeneratePdf(json);
            return File(byte1, "application/pdf", "StaffSurvey.pdf");
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
        public async Task<IActionResult> Edit(int surveyId)
        {
            var Survey = _StaffSurveyService.Get(surveyId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffSurvey
            {
                StaffSurveyId = Survey.Result.StaffSurveyId,
                Attachment = Survey.Result.Attachment,
                OfficerToAct = Survey.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Reference = Survey.Result.Reference,
                Date = Survey.Result.Date,
                Deadline = Survey.Result.Deadline,
                URL = Survey.Result.URL,
                Remarks = Survey.Result.Remarks,
                WorkTeam = Survey.Result.Workteam.Select(s => s.StaffPersonalInfoId).ToList(),
                Status = Survey.Result.Status,
                ActionRequired = Survey.Result.ActionRequired,
                NextCheckDate = Survey.Result.NextCheckDate,
                StaffId = Survey.Result.StaffId,
                AccessToPolicies = Survey.Result.AccessToPolicies,
                AdequateTrainingReceived = Survey.Result.AdequateTrainingReceived,
                AreaRequiringImprovements = Survey.Result.AreaRequiringImprovements,
                CompanyManagement = Survey.Result.CompanyManagement,
                HealthCareServicesSatisfaction = Survey.Result.HealthCareServicesSatisfaction,
                SupportFromCompany = Survey.Result.SupportFromCompany,
                WorkEnvironmentSuggestions = Survey.Result.WorkEnvironmentSuggestions,
                Details = Survey.Result.Details,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffSurvey model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.StaffId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffsurvey";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

            var post = new PostStaffSurvey();
                post.Attachment = model.Attachment;
                post.Reference = model.Reference;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostSurveyOfficerToAct { StaffPersonalInfoId = o, StaffSurveyId = model.StaffSurveyId }).ToList();
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.NextCheckDate = model.NextCheckDate;
                post.Workteam = model.WorkTeam.Select(o => new PostSurveyWorkteam { StaffPersonalInfoId = o, StaffSurveyId = model.StaffSurveyId }).ToList();
                post.StaffId = model.StaffId;
                post.AccessToPolicies = model.AccessToPolicies;
                post.AdequateTrainingReceived = model.AdequateTrainingReceived;
                post.AreaRequiringImprovements = model.AreaRequiringImprovements;
                post.CompanyManagement = model.CompanyManagement;
                post.HealthCareServicesSatisfaction = model.HealthCareServicesSatisfaction;
                post.SupportFromCompany = model.SupportFromCompany;
                post.WorkEnvironmentSuggestions = model.WorkEnvironmentSuggestions;
                post.Details = model.Details;

            var result = await _StaffSurveyService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Survey successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffSurvey model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string extention = model.StaffId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffsurvey";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(),model.Attach.ContentType);
                model.Attachment = path;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion

            var put = new PutStaffSurvey();
                put.Attachment = model.Attachment;
                put.StaffSurveyId = model.StaffSurveyId;
                put.Reference = model.Reference;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutSurveyOfficerToAct { StaffPersonalInfoId = o, StaffSurveyId = model.StaffSurveyId }).ToList();
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.Workteam = model.WorkTeam.Select(o => new PutSurveyWorkteam { StaffPersonalInfoId = o, StaffSurveyId = model.StaffSurveyId }).ToList();
                put.StaffId = model.StaffId;
                put.AccessToPolicies = model.AccessToPolicies;
                put.AdequateTrainingReceived = model.AdequateTrainingReceived;
                put.AreaRequiringImprovements = model.AreaRequiringImprovements;
                put.CompanyManagement = model.CompanyManagement;
                put.HealthCareServicesSatisfaction = model.HealthCareServicesSatisfaction;
                put.SupportFromCompany = model.SupportFromCompany;
                put.WorkEnvironmentSuggestions = model.WorkEnvironmentSuggestions;
                put.Details = model.Details;

            var entity = await _StaffSurveyService.Put(put);
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
