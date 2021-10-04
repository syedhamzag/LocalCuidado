using AwesomeCare.Admin.Services.ClientLogAudit;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
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
using AwesomeCare.Admin.ViewModels.ClientLogAudit;
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
    public class ClientLogAuditController : BaseController
    {
        private IClientLogAuditService _clientlogAuditService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientLogAuditController(IClientLogAuditService clientlogAuditService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientlogAuditService = clientlogAuditService;
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
            var entities = await _clientlogAuditService.Get();
            
            var client = await _clientService.GetClientDetail();
            List<CreateClientLogAudit> reports = new List<CreateClientLogAudit>();         
            foreach (GetClientLogAudit item in entities)
            {
                var report = new CreateClientLogAudit();
                report.LogAuditId = item.LogAuditId;
                report.Date = item.Date;
                report.NextDueDate = item.NextDueDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientLogAudit();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int logId)
        {
            var LogAudit = await _clientlogAuditService.Get(logId);
            var putEntity = new CreateClientLogAudit
            {
                LogAuditId = LogAudit.LogAuditId,
                Reference = LogAudit.Reference,
                ClientId = LogAudit.ClientId,
                ActionRecommended = LogAudit.ActionRecommended,
                ActionTaken = LogAudit.ActionTaken,
                EvidenceFilePath = LogAudit.EvidenceFilePath,
                Date = LogAudit.Date,
                NextDueDate = LogAudit.NextDueDate,
                Deadline = LogAudit.Deadline,
                EvidenceOfActionTaken = LogAudit.EvidenceOfActionTaken,
                LessonLearntAndShared = LogAudit.LessonLearntAndShared,
                LogURL = LogAudit.LogURL,
                NameOfAuditor = LogAudit.NameOfAuditor,
                Observations = LogAudit.Observations,
                Remarks = LogAudit.Remarks,
                RepeatOfIncident = LogAudit.RepeatOfIncident,
                RotCause = LogAudit.RotCause,
                Status = LogAudit.Status,
                ThinkingServiceUsers = LogAudit.ThinkingServiceUsers,
                Communication = LogAudit.Communication,
                ImproperDocumentation = LogAudit.ImproperDocumentation,
                IsCareDifference = LogAudit.IsCareDifference,
                IsCareExpected = LogAudit.IsCareExpected,
                ProperDocumentation = LogAudit.ProperDocumentation,
                ThinkingStaff = LogAudit.ThinkingStaff,
                ThinkingStaffStop = LogAudit.ThinkingStaffStop,
                OfficerToTakeAction = LogAudit.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                OFFICERTOACT = LogAudit.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int logId, string sender,string password, string recipient, string Smtp)
        {
            List<string> rcpt = new List<string>();
            var LogAudit = await _clientlogAuditService.Get(logId);
            var json = JsonConvert.SerializeObject(LogAudit);
            byte[] byte1 = GeneratePdf(json);
            string filename = "ClientLogAudit.pdf";
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), filename);
            string subject = "ClientLogAudit";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            await _emailService.SendAsync(rcpt, subject, body, byte1, filename, "pdf", true);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int logId)
        {
            var LogAudit = await _clientlogAuditService.Get(logId);
            var json = JsonConvert.SerializeObject(LogAudit);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientLogAudit.pdf");
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
                        var para = new Paragraph(paragraphs.Replace("{","").Replace("}","").Replace("\"","").Replace(",","\n"));
                        document.Add(para);
                        buffer = memStream.ToArray();
                        document.Close();
                    }
                }
                buffer = memStream.ToArray();
            }
            return buffer;
        }
        public async Task<IActionResult> Edit(int LogAuditId)
        {
            var LogAudit = _clientlogAuditService.Get(LogAuditId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientLogAudit
            {
                LogAuditId = LogAudit.Result.LogAuditId,
                Reference = LogAudit.Result.Reference,
                ClientId = LogAudit.Result.ClientId,
                ActionRecommended = LogAudit.Result.ActionRecommended,
                ActionTaken = LogAudit.Result.ActionTaken,
                EvidenceFilePath = LogAudit.Result.EvidenceFilePath,
                Date = LogAudit.Result.Date,
                NextDueDate = LogAudit.Result.NextDueDate,
                Deadline = LogAudit.Result.Deadline,
                EvidenceOfActionTaken = LogAudit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = LogAudit.Result.LessonLearntAndShared,
                LogURL = LogAudit.Result.LogURL,
                NameOfAuditor = LogAudit.Result.NameOfAuditor,
                Observations = LogAudit.Result.Observations,
                OfficerToTakeAction = LogAudit.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = LogAudit.Result.Remarks,
                RepeatOfIncident = LogAudit.Result.RepeatOfIncident,
                RotCause = LogAudit.Result.RotCause,
                Status = LogAudit.Result.Status,
                ThinkingServiceUsers = LogAudit.Result.ThinkingServiceUsers,
                Communication = LogAudit.Result.Communication,
                ImproperDocumentation = LogAudit.Result.ImproperDocumentation,
                IsCareDifference = LogAudit.Result.IsCareDifference,
                IsCareExpected = LogAudit.Result.IsCareExpected,
                ProperDocumentation = LogAudit.Result.ProperDocumentation,
                ThinkingStaff = LogAudit.Result.ThinkingStaff,
                ThinkingStaffStop = LogAudit.Result.ThinkingStaffStop,
                OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientLogAudit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientLogAudit postlog = new PostClientLogAudit();

            #region Evidence
            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            else
            {
                model.EvidenceOfActionTaken = "No Image";
            }
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.EvidenceFilePath = path;
            }
            else
            {
                model.EvidenceFilePath = "No Image";
            }
            #endregion
            
                postlog.ActionRecommended = model.ActionRecommended;
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.ActionRecommended = model.ActionRecommended;
                postlog.ActionTaken = model.ActionTaken;
                postlog.EvidenceFilePath = model.EvidenceFilePath;
                postlog.Date = model.Date;
                postlog.NextDueDate = model.NextDueDate;
                postlog.Deadline = model.Deadline;
                postlog.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                postlog.LessonLearntAndShared = model.LessonLearntAndShared;
                postlog.LogURL = model.LogURL;
                postlog.NameOfAuditor = model.NameOfAuditor;
                postlog.Observations = model.Observations;
                postlog.OfficerToAct = model.OfficerToTakeAction.Select(o => new PostLogAuditOfficerToAct { StaffPersonalInfoId = o, LogAuditId = model.LogAuditId }).ToList();
                postlog.Remarks = model.Remarks;
                postlog.RepeatOfIncident = model.RepeatOfIncident;
                postlog.RotCause = model.RotCause;
                postlog.Status = model.Status;
                postlog.ThinkingServiceUsers = model.ThinkingServiceUsers;
                postlog.Communication = model.Communication;
                postlog.ImproperDocumentation = model.ImproperDocumentation;
                postlog.IsCareDifference = model.IsCareDifference;
                postlog.IsCareExpected = model.IsCareExpected;
                postlog.ProperDocumentation = model.ProperDocumentation;
                postlog.ThinkingStaff = model.ThinkingStaff;
                postlog.ThinkingStaffStop = model.ThinkingStaffStop;

            var result = await _clientlogAuditService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientLogAudit model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Evidence
            if (model.Evidence != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Evidence_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            else
            {
                model.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
            }
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.EvidenceFilePath = pathA;

            }
            else
            {
                model.EvidenceFilePath = model.EvidenceFilePath;
            }
            #endregion

            PutClientLogAudit put = new PutClientLogAudit();
            put.LogAuditId = model.LogAuditId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.ActionRecommended = model.ActionRecommended;
            put.ActionTaken = model.ActionTaken;
            put.EvidenceFilePath = model.EvidenceFilePath;
            put.Date = model.Date;
            put.NextDueDate = model.NextDueDate;
            put.Deadline = model.Deadline;
            put.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
            put.LessonLearntAndShared = model.LessonLearntAndShared;
            put.LogURL = model.LogURL;
            put.NameOfAuditor = model.NameOfAuditor;
            put.Observations = model.Observations;
            put.OfficerToAct = model.OfficerToTakeAction.Select(o => new PutLogAuditOfficerToAct { StaffPersonalInfoId = o, LogAuditId = model.LogAuditId }).ToList();
            put.Remarks = model.Remarks;
            put.RepeatOfIncident = model.RepeatOfIncident;
            put.RotCause = model.RotCause;
            put.Status = model.Status;
            put.ThinkingServiceUsers = model.ThinkingServiceUsers;
            put.Communication = model.Communication;
            put.ImproperDocumentation = model.ImproperDocumentation;
            put.IsCareDifference = model.IsCareDifference;
            put.IsCareExpected = model.IsCareExpected;
            put.ProperDocumentation = model.ProperDocumentation;
            put.ThinkingStaff = model.ThinkingStaff;
            put.ThinkingStaffStop = model.ThinkingStaffStop;
            
            var entity = await _clientlogAuditService.Put(put);
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
