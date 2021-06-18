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

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffSurveyService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            return View(logAudit.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference, string sender, string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffSurveyService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffSurvey.pdf");
            string subject = "StaffSurvey";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffSurveyService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

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

        public async Task<IActionResult> Edit(string Reference)
        {
            List<int> officer = new List<int>();
            List<int> Ids = new List<int>();
            var Survey = _StaffSurveyService.GetByRef(Reference);
            foreach (var item in Survey.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.StaffSurveyId);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffSurvey
            {
                SurveyIds = Ids,
                Attachment = Survey.Result.FirstOrDefault().Attachment,
                Reference = Survey.Result.FirstOrDefault().Reference,
                Date = Survey.Result.FirstOrDefault().Date,
                Deadline = Survey.Result.FirstOrDefault().Deadline,
                URL = Survey.Result.FirstOrDefault().URL,
                OfficerToAct = officer,
                Remarks = Survey.Result.FirstOrDefault().Remarks,
                Status = Survey.Result.FirstOrDefault().Status,
                ActionRequired = Survey.Result.FirstOrDefault().ActionRequired,
                NextCheckDate = Survey.Result.FirstOrDefault().NextCheckDate,
                WorkTeam = Survey.Result.FirstOrDefault().WorkTeam,
                StaffId = Survey.Result.FirstOrDefault().StaffId,
                AccessToPolicies = Survey.Result.FirstOrDefault().AccessToPolicies,
                AdequateTrainingReceived = Survey.Result.FirstOrDefault().AdequateTrainingReceived,
                AreaRequiringImprovements = Survey.Result.FirstOrDefault().AreaRequiringImprovements,
                CompanyManagement = Survey.Result.FirstOrDefault().CompanyManagement,
                HealthCareServicesSatisfaction = Survey.Result.FirstOrDefault().HealthCareServicesSatisfaction,
                SupportFromCompany = Survey.Result.FirstOrDefault().SupportFromCompany,
                WorkEnvironmentSuggestions = Survey.Result.FirstOrDefault().WorkEnvironmentSuggestions,
                Details = Survey.Result.FirstOrDefault().Details,
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
            #endregion

            List<PostStaffSurvey> posts = new List<PostStaffSurvey>();
            foreach (var item in model.OfficerToAct)
            {
                var post = new PostStaffSurvey();
                post.Attachment = model.Attachment;
                post.Reference = model.Reference;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = item;
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.NextCheckDate = model.NextCheckDate;
                post.WorkTeam = model.WorkTeam;
                post.StaffId = model.StaffId;
                post.AccessToPolicies = model.AccessToPolicies;
                post.AdequateTrainingReceived = model.AdequateTrainingReceived;
                post.AreaRequiringImprovements = model.AreaRequiringImprovements;
                post.CompanyManagement = model.CompanyManagement;
                post.HealthCareServicesSatisfaction = model.HealthCareServicesSatisfaction;
                post.SupportFromCompany = model.SupportFromCompany;
                post.WorkEnvironmentSuggestions = model.WorkEnvironmentSuggestions;
                post.Details = model.Details;
                posts.Add(post);
            }

            var result = await _StaffSurveyService.Create(posts);
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
            int count = model.SurveyIds.Count;
            int i = 0;
            List<PutStaffSurvey> puts = new List<PutStaffSurvey>();
            foreach (var item in model.OfficerToAct)
            {
                var put = new PutStaffSurvey();
                if (i < count)
                    put.StaffSurveyId = model.SurveyIds[i];
                put.Attachment = model.Attachment;
                put.Reference = model.Reference;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = item;
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.WorkTeam = model.WorkTeam;
                put.StaffId = model.StaffId;
                put.AccessToPolicies = model.AccessToPolicies;
                put.AdequateTrainingReceived = model.AdequateTrainingReceived;
                put.AreaRequiringImprovements = model.AreaRequiringImprovements;
                put.CompanyManagement = model.CompanyManagement;
                put.HealthCareServicesSatisfaction = model.HealthCareServicesSatisfaction;
                put.SupportFromCompany = model.SupportFromCompany;
                put.WorkEnvironmentSuggestions = model.WorkEnvironmentSuggestions;
                put.Details = model.Details;
                puts.Add(put);
            }
            var entity = await _StaffSurveyService.Put(puts);
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
