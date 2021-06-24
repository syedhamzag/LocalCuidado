using AutoMapper;
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

namespace AwesomeCare.Admin.Controllers
{
    public class StaffSurveyController : BaseController
    {
        private IStaffSurveyService _StaffSurveyService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffSurveyController(IStaffSurveyService StaffSurveyService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffSurveyService = StaffSurveyService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSurveyService.Get();
            return View(entities);
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
                Details = Survey.Result.Details
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
                Attachment = Survey.Result.Attachment,
                Reference = Survey.Result.Reference,
                Date = Survey.Result.Date,
                Deadline = Survey.Result.Deadline,
                URL = Survey.Result.URL,
                OfficerToAct = Survey.Result.OfficerToAct.Select(s=> s.StaffPersonalInfoId).ToList(),
                Remarks = Survey.Result.Remarks,
                Status = Survey.Result.Status,
                ActionRequired = Survey.Result.ActionRequired,
                NextCheckDate = Survey.Result.NextCheckDate,
                WorkTeam = Survey.Result.Workteam.Select(s => s.StaffPersonalInfoId).ToList(),
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
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
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
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostSurveyOfficerToAct { StaffPersonalInfoId = o, SurveyId = model.StaffSurveyId }).ToList();
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.NextCheckDate = model.NextCheckDate;
                post.Workteam = model.WorkTeam.Select(o => new PostSurveyWorkteam { StaffPersonalInfoId = o, SurveyId = model.StaffSurveyId }).ToList();
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

                var put = new PutStaffSurvey();
                put.Attachment = model.Attachment;
                put.Reference = model.Reference;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutSurveyOfficerToAct { StaffPersonalInfoId = o, SurveyId = model.StaffSurveyId }).ToList();
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.Workteam = model.WorkTeam.Select(o => new PutSurveyWorkteam { StaffPersonalInfoId = o, SurveyId = model.StaffSurveyId }).ToList();
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
