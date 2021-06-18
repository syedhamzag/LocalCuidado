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

        public ClientMedAuditController(IClientMedAuditService clientMedAuditService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _clientMedAuditService = clientMedAuditService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _clientMedAuditService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientMedAudit();
            var staffs = await _staffService.GetStaffs();
            model.OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var MedAudit = await _clientMedAuditService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in MedAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToTakeAction).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(MedAudit.FirstOrDefault());
            var newJson = json + staffName;
            return View(MedAudit.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference, string sender, string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var MedAudit = await _clientMedAuditService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in MedAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToTakeAction).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(MedAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientMedAudit.pdf");
            string subject = "ClientMedAudit";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var MedAudit = await _clientMedAuditService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in MedAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToTakeAction).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(MedAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

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

        public async Task<IActionResult> Edit(string Reference)
        {
            List<int> officer = new List<int>();
            List<int> Ids = new List<int>();
            var MedAudit = _clientMedAuditService.GetByRef(Reference);
            foreach (var item in MedAudit.Result)
            {
                officer.Add(item.OfficerToTakeAction);
                Ids.Add(item.MedAuditId);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientMedAudit
            {
                MedAuditId = MedAudit.Result.FirstOrDefault().MedAuditId,
                MedAuditIds = Ids,
                Reference = MedAudit.Result.FirstOrDefault().Reference,
                ClientId = MedAudit.Result.FirstOrDefault().ClientId,
                ActionRecommended = MedAudit.Result.FirstOrDefault().ActionRecommended,
                ActionTaken = MedAudit.Result.FirstOrDefault().ActionTaken,
                Attachment = MedAudit.Result.FirstOrDefault().Attachment,
                Date = MedAudit.Result.FirstOrDefault().Date,
                NextDueDate = MedAudit.Result.FirstOrDefault().NextDueDate,
                Deadline = MedAudit.Result.FirstOrDefault().Deadline,
                EvidenceOfActionTaken = MedAudit.Result.FirstOrDefault().EvidenceOfActionTaken,
                LessonLearntAndShared = MedAudit.Result.FirstOrDefault().LessonLearntAndShared,
                LogURL = MedAudit.Result.FirstOrDefault().LogURL,
                NameOfAuditor = MedAudit.Result.FirstOrDefault().NameOfAuditor,
                Observations = MedAudit.Result.FirstOrDefault().Observations,
                OfficerToTakeAction = officer,
                Remarks = MedAudit.Result.FirstOrDefault().Remarks,
                RepeatOfIncident = MedAudit.Result.FirstOrDefault().RepeatOfIncident,
                RotCause = MedAudit.Result.FirstOrDefault().RotCause,
                Status = MedAudit.Result.FirstOrDefault().Status,
                ThinkingServiceUsers = MedAudit.Result.FirstOrDefault().ThinkingServiceUsers,
                GapsInAdmistration = MedAudit.Result.FirstOrDefault().GapsInAdmistration,
                RightsOfMedication = MedAudit.Result.FirstOrDefault().RightsOfMedication,
                MarChartReview = MedAudit.Result.FirstOrDefault().MarChartReview,
                MedicationConcern = MedAudit.Result.FirstOrDefault().MedicationConcern,
                HardCopyReview = MedAudit.Result.FirstOrDefault().HardCopyReview,
                MedicationSupplyEfficiency = MedAudit.Result.FirstOrDefault().MedicationSupplyEfficiency,
                MedicationInfoUploadEefficiency = MedAudit.Result.FirstOrDefault().MedicationInfoUploadEefficiency,
                OFFICERTOACT = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientMedAudit model)
        {
            if (model == null || !ModelState.IsValid)
            {
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
            #endregion
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
            }
            #endregion
            List<PostClientMedAudit> postMedAudits = new List<PostClientMedAudit>();
            foreach (var officer in model.OfficerToTakeAction)
            {
                var postMedAudit = new PostClientMedAudit();

                postMedAudit.Reference = model.Reference;
                postMedAudit.ClientId = model.ClientId;
                postMedAudit.ActionRecommended = model.ActionRecommended;
                postMedAudit.ActionTaken = model.ActionTaken;
                postMedAudit.Attachment = model.Attachment;
                postMedAudit.Date = model.Date;
                postMedAudit.NextDueDate = model.NextDueDate;
                postMedAudit.Deadline = model.Deadline;
                postMedAudit.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                postMedAudit.LessonLearntAndShared = model.LessonLearntAndShared;
                postMedAudit.LogURL = model.LogURL;
                postMedAudit.NameOfAuditor = model.NameOfAuditor;
                postMedAudit.Observations = model.Observations;
                postMedAudit.OfficerToTakeAction = officer;
                postMedAudit.Remarks = model.Remarks;
                postMedAudit.RepeatOfIncident = model.RepeatOfIncident;
                postMedAudit.RotCause = model.RotCause;
                postMedAudit.Status = model.Status;
                postMedAudit.ThinkingServiceUsers = model.ThinkingServiceUsers;
                postMedAudit.GapsInAdmistration = model.GapsInAdmistration;
                postMedAudit.RightsOfMedication = model.RightsOfMedication;
                postMedAudit.MarChartReview = model.MarChartReview;
                postMedAudit.MedicationConcern = model.MedicationConcern;
                postMedAudit.HardCopyReview = model.HardCopyReview;
                postMedAudit.MedicationSupplyEfficiency = model.MedicationSupplyEfficiency;
                postMedAudit.MedicationInfoUploadEefficiency = model.MedicationInfoUploadEefficiency;
                postMedAudits.Add(postMedAudit);
            }
            var result = await _clientMedAuditService.Create(postMedAudits);
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
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
            List<PutClientMedAudit> putMedAudits = new List<PutClientMedAudit>();
            int count = model.MedAuditIds.Count;
            int i = 0;
            foreach (var item in model.OfficerToTakeAction)
            {
                var putMedAudit = new PutClientMedAudit();
                if(i < count)
                    putMedAudit.MedAuditId = model.MedAuditIds[i];
                putMedAudit.Reference = model.Reference;
                putMedAudit.ClientId = model.ClientId;
                putMedAudit.ActionRecommended = model.ActionRecommended;
                putMedAudit.ActionTaken = model.ActionTaken;
                putMedAudit.Attachment = model.Attachment;
                putMedAudit.Date = model.Date;
                putMedAudit.NextDueDate = model.NextDueDate;
                putMedAudit.Deadline = model.Deadline;
                putMedAudit.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                putMedAudit.LessonLearntAndShared = model.LessonLearntAndShared;
                putMedAudit.LogURL = model.LogURL;
                putMedAudit.NameOfAuditor = model.NameOfAuditor;
                putMedAudit.Observations = model.Observations;
                putMedAudit.OfficerToTakeAction = item;
                putMedAudit.Remarks = model.Remarks;
                putMedAudit.RepeatOfIncident = model.RepeatOfIncident;
                putMedAudit.RotCause = model.RotCause;
                putMedAudit.Status = model.Status;
                putMedAudit.ThinkingServiceUsers = model.ThinkingServiceUsers;
                putMedAudit.GapsInAdmistration = model.GapsInAdmistration;
                putMedAudit.RightsOfMedication = model.RightsOfMedication;
                putMedAudit.MarChartReview = model.MarChartReview;
                putMedAudit.MedicationConcern = model.MedicationConcern;
                putMedAudit.HardCopyReview = model.HardCopyReview;
                putMedAudit.MedicationSupplyEfficiency = model.MedicationSupplyEfficiency;
                putMedAudit.MedicationInfoUploadEefficiency = model.MedicationInfoUploadEefficiency;
                putMedAudits.Add(putMedAudit);
            }
            var json = JsonConvert.SerializeObject(putMedAudits);
            var entity = await _clientMedAuditService.Put(putMedAudits);
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
