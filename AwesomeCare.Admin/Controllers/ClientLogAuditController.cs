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
            
            var client = await _clientService.GetClients();
            var baserecord = await _baseService.GetBaseRecordsWithItems();
            List<CreateClientLogAudit> reports = new List<CreateClientLogAudit>();         
            foreach (GetClientLogAudit item in entities)
            {
                var report = new CreateClientLogAudit();
                report.LogAuditId = item.LogAuditId;
                report.Reference = item.Reference;
                report.NextDueDate = item.NextDueDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.Firstname).FirstOrDefault();
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
            var client = await _clientService.GetClient(clientId.Value);
            model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
            model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _clientlogAuditService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToTakeAction).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            return View(logAudit.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference,string sender,string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _clientlogAuditService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToTakeAction).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientLogAudit.pdf");
            string subject = "ClientLogAudit";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _clientlogAuditService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToTakeAction).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
    
            return File(byte1, "application/pdf","ClientLogAudit.pdf");
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
        public async Task<IActionResult> Edit(string Reference)
        {
            List<int> officer = new List<int>();
            List<int> Ids = new List<int>();
            var logAudit = _clientlogAuditService.GetByRef(Reference);
            foreach (var item in logAudit.Result)
            {
                officer.Add(item.OfficerToTakeAction);
                Ids.Add(item.LogAuditId);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientLogAudit
            {
                LogAuditIds = Ids,
                Reference = logAudit.Result.FirstOrDefault().Reference,
                ClientId = logAudit.Result.FirstOrDefault().ClientId,
                ActionRecommended = logAudit.Result.FirstOrDefault().ActionRecommended,
                ActionTaken = logAudit.Result.FirstOrDefault().ActionTaken,
                EvidenceFilePath = logAudit.Result.FirstOrDefault().EvidenceFilePath,
                Date = logAudit.Result.FirstOrDefault().Date,
                NextDueDate = logAudit.Result.FirstOrDefault().NextDueDate,
                Deadline = logAudit.Result.FirstOrDefault().Deadline,
                EvidenceOfActionTaken = logAudit.Result.FirstOrDefault().EvidenceOfActionTaken,
                LessonLearntAndShared = logAudit.Result.FirstOrDefault().LessonLearntAndShared,
                LogURL = logAudit.Result.FirstOrDefault().LogURL,
                NameOfAuditor = logAudit.Result.FirstOrDefault().NameOfAuditor,
                Observations = logAudit.Result.FirstOrDefault().Observations,
                OfficerToTakeAction = officer,
                Remarks = logAudit.Result.FirstOrDefault().Remarks,
                RepeatOfIncident = logAudit.Result.FirstOrDefault().RepeatOfIncident,
                RotCause = logAudit.Result.FirstOrDefault().RotCause,
                Status = logAudit.Result.FirstOrDefault().Status,
                ThinkingServiceUsers = logAudit.Result.FirstOrDefault().ThinkingServiceUsers,
                Communication = logAudit.Result.FirstOrDefault().Communication,
                ImproperDocumentation = logAudit.Result.FirstOrDefault().ImproperDocumentation,
                IsCareDifference = logAudit.Result.FirstOrDefault().IsCareDifference,
                IsCareExpected = logAudit.Result.FirstOrDefault().IsCareExpected,
                ProperDocumentation = logAudit.Result.FirstOrDefault().ProperDocumentation,
                ThinkingStaff = logAudit.Result.FirstOrDefault().ThinkingStaff,
                ThinkingStaffStop = logAudit.Result.FirstOrDefault().ThinkingStaffStop,
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
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            List<PostClientLogAudit> postlogs = new List<PostClientLogAudit>();
            #region Evidence
            if (model.Evidence != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Evidence_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.EvidenceFilePath = pathA;
            }               
            #endregion
            foreach (var officer in model.OfficerToTakeAction)
            {
                var postlog = new PostClientLogAudit();
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
                postlog.OfficerToTakeAction = officer;
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

                postlogs.Add(postlog);

            }
            var result = await _clientlogAuditService.Create(postlogs);
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
            List<PutClientLogAudit> puts = new List<PutClientLogAudit>();
            int count = model.LogAuditIds.Count;
            int i = 0;
            foreach (var item in model.OfficerToTakeAction)
            {
                var put = new PutClientLogAudit();
                if(i < count)
                    put.LogAuditId = model.LogAuditIds[i];
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
                put.OfficerToTakeAction = item;
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
                i++;
                puts.Add(put);

            }
            var entity = await _clientlogAuditService.Put(puts);
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
