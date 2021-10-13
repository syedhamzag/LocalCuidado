using AwesomeCare.Admin.Services.ClientMedAudit;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
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
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Buffers;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientMedAudit;
using System.Net.Mail;
using System.Net;
using System.IO;
using OperationStatus = AwesomeCare.Admin.Models.OperationStatus;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using AwesomeCare.Model.Models;
using iText.Kernel.Geom;
using iText.Html2pdf;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.ClientMedAudit;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientMedAuditController : BaseController
    {
        private IClientMedAuditService _clientMedAuditService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private IBaseRecordService _baseService;

        public ClientMedAuditController(IClientMedAuditService clientMedAuditService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientMedAuditService = clientMedAuditService;
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
            var entities = await _clientMedAuditService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientMedAudit> reports = new List<CreateClientMedAudit>();
            foreach (GetClientMedAudit item in entities)
            {
                var report = new CreateClientMedAudit();
                report.MedAuditId = item.MedAuditId;
                report.Date = item.Date;
                report.NextDueDate = item.NextDueDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.Attachment = item.Attachment;
                report.EvidenceOfActionTaken = item.EvidenceOfActionTaken;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientMedAudit();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OFFICERTOACTList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(int medId)
        {
            var MedAudit = await _clientMedAuditService.Get(medId);
            var putEntity = new CreateClientMedAudit
            {
                MedAuditId = MedAudit.MedAuditId,
                Reference = MedAudit.Reference,
                ClientId = MedAudit.ClientId,
                ActionRecommended = MedAudit.ActionRecommended,
                ActionTaken = MedAudit.ActionTaken,
                Attachment = MedAudit.Attachment,
                Date = MedAudit.Date,
                NextDueDate = MedAudit.NextDueDate,
                Deadline = MedAudit.Deadline,
                EvidenceOfActionTaken = MedAudit.EvidenceOfActionTaken,
                LessonLearntAndShared = MedAudit.LessonLearntAndShared,
                LogURL = MedAudit.LogURL,
                Observations = MedAudit.Observations,
                Remarks = MedAudit.Remarks,
                RepeatOfIncident = MedAudit.RepeatOfIncident,
                RotCause = MedAudit.RotCause,
                Status = MedAudit.Status,
                ThinkingServiceUsers = MedAudit.ThinkingServiceUsers,
                GapsInAdmistration = MedAudit.GapsInAdmistration,
                RightsOfMedication = MedAudit.RightsOfMedication,
                MarChartReview = MedAudit.MarChartReview,
                MedicationConcern = MedAudit.MedicationConcern,
                HardCopyReview = MedAudit.HardCopyReview,
                MedicationSupplyEfficiency = MedAudit.MedicationSupplyEfficiency,
                MedicationInfoUploadEefficiency = MedAudit.MedicationInfoUploadEefficiency,
                OfficerToTakeAction = MedAudit.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                NameOfAuditor = MedAudit.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                OFFICERTOACTList = MedAudit.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                AuditorList = MedAudit.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int medId, string sender, string password, string recipient, string Smtp)
        {
            var MedAudit = await _clientMedAuditService.Get(medId);
            var json = JsonConvert.SerializeObject(MedAudit);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientMedAudit.pdf");
            string subject = "ClientMedAudit";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int medId)
        {
            var MedAudit = await _clientMedAuditService.Get(medId);
            var json = JsonConvert.SerializeObject(MedAudit);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientMedAudit.pdf");
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

        public async Task<IActionResult> Edit(int MedAuditId)
        {
            var MedAudit = _clientMedAuditService.Get(MedAuditId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientMedAudit
            {
                MedAuditId = MedAudit.Result.MedAuditId,
                Reference = MedAudit.Result.Reference,
                ClientId = MedAudit.Result.ClientId,
                ActionRecommended = MedAudit.Result.ActionRecommended,
                ActionTaken = MedAudit.Result.ActionTaken,
                Attachment = MedAudit.Result.Attachment,
                Date = MedAudit.Result.Date,
                NextDueDate = MedAudit.Result.NextDueDate,
                Deadline = MedAudit.Result.Deadline,
                EvidenceOfActionTaken = MedAudit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = MedAudit.Result.LessonLearntAndShared,
                LogURL = MedAudit.Result.LogURL,
                NameOfAuditor = MedAudit.Result.StaffName.Select(s=>s.StaffPersonalInfoId).ToList(),
                Observations = MedAudit.Result.Observations,
                OfficerToTakeAction = MedAudit.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = MedAudit.Result.Remarks,
                RepeatOfIncident = MedAudit.Result.RepeatOfIncident,
                RotCause = MedAudit.Result.RotCause,
                Status = MedAudit.Result.Status,
                ThinkingServiceUsers = MedAudit.Result.ThinkingServiceUsers,
                GapsInAdmistration = MedAudit.Result.GapsInAdmistration,
                RightsOfMedication = MedAudit.Result.RightsOfMedication,
                MarChartReview = MedAudit.Result.MarChartReview,
                MedicationConcern = MedAudit.Result.MedicationConcern,
                HardCopyReview = MedAudit.Result.HardCopyReview,
                MedicationSupplyEfficiency = MedAudit.Result.MedicationSupplyEfficiency,
                MedicationInfoUploadEefficiency = MedAudit.Result.MedicationInfoUploadEefficiency,
                OFFICERTOACTList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientMedAudit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACTList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientMedAudit postlog = new PostClientMedAudit();

            #region Evidence
            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientmedaudit";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            else
            {
                model.EvidenceOfActionTaken = "No Image";
            }
            #endregion
            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientmedaudit";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

                postlog.Reference = model.Reference;
                postlog.ClientId = model.ClientId;
                postlog.ActionRecommended = model.ActionRecommended;
                postlog.ActionTaken = model.ActionTaken;
                postlog.Attachment = model.Attachment;
                postlog.Date = model.Date;
                postlog.NextDueDate = model.NextDueDate;
                postlog.Deadline = model.Deadline;
                postlog.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                postlog.LessonLearntAndShared = model.LessonLearntAndShared;
                postlog.LogURL = model.LogURL;
                postlog.StaffName = model.NameOfAuditor.Select(o => new PostMedAuditStaffName { StaffPersonalInfoId = o, MedAuditId = model.MedAuditId }).ToList(); ;
                postlog.Observations = model.Observations;
                postlog.OfficerToAct = model.OfficerToTakeAction.Select(o => new PostMedAuditOfficerToAct { StaffPersonalInfoId = o, MedAuditId = model.MedAuditId }).ToList();
                postlog.Remarks = model.Remarks;
                postlog.RepeatOfIncident = model.RepeatOfIncident;
                postlog.RotCause = model.RotCause;
                postlog.Status = model.Status;
                postlog.ThinkingServiceUsers = model.ThinkingServiceUsers;
                postlog.GapsInAdmistration = model.GapsInAdmistration;
                postlog.RightsOfMedication = model.RightsOfMedication;
                postlog.MarChartReview = model.MarChartReview;
                postlog.MedicationConcern = model.MedicationConcern;
                postlog.HardCopyReview = model.HardCopyReview;
                postlog.MedicationSupplyEfficiency = model.MedicationSupplyEfficiency;
                postlog.MedicationInfoUploadEefficiency = model.MedicationInfoUploadEefficiency;
               
            var result = await _clientMedAuditService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Med Audit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientMedAudit model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OFFICERTOACTList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Evidence
            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientmedaudit";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            else
            {
                model.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
            }
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientmedaudit";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion

            PutClientMedAudit put = new PutClientMedAudit();
            put.MedAuditId = model.MedAuditId;
            put.Reference = model.Reference;
            put.ClientId = model.ClientId;
            put.ActionRecommended = model.ActionRecommended;
            put.ActionTaken = model.ActionTaken;
            put.Attachment = model.Attachment;
            put.Date = model.Date;
            put.NextDueDate = model.NextDueDate;
            put.Deadline = model.Deadline;
            put.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
            put.LessonLearntAndShared = model.LessonLearntAndShared;
            put.LogURL = model.LogURL;
            put.StaffName = model.NameOfAuditor.Select(o => new PutMedAuditStaffName { StaffPersonalInfoId = o, MedAuditId = model.MedAuditId}).ToList();
            put.Observations = model.Observations;
            put.OfficerToAct = model.OfficerToTakeAction.Select(o => new PutMedAuditOfficerToAct { StaffPersonalInfoId = o, MedAuditId = model.MedAuditId }).ToList();
            put.Remarks = model.Remarks;
            put.RepeatOfIncident = model.RepeatOfIncident;
            put.RotCause = model.RotCause;
            put.Status = model.Status;
            put.ThinkingServiceUsers = model.ThinkingServiceUsers;
            put.GapsInAdmistration = model.GapsInAdmistration;
            put.RightsOfMedication = model.RightsOfMedication;
            put.MarChartReview = model.MarChartReview;
            put.MedicationConcern = model.MedicationConcern;
            put.HardCopyReview = model.HardCopyReview;
            put.MedicationSupplyEfficiency = model.MedicationSupplyEfficiency;
            put.MedicationInfoUploadEefficiency = model.MedicationInfoUploadEefficiency;

            var json = JsonConvert.SerializeObject(put);
            var entity = await _clientMedAuditService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode == true ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode == true)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
