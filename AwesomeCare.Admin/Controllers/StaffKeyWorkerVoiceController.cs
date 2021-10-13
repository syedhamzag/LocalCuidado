using AutoMapper;
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
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffKeyWorkerVoiceController : BaseController
    {
        private IStaffKeyWorkerVoiceService _StaffKeyWorkerVoiceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private IBaseRecordService _baseService;

        public StaffKeyWorkerVoiceController(IStaffKeyWorkerVoiceService StaffKeyWorkerVoiceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _StaffKeyWorkerVoiceService = StaffKeyWorkerVoiceService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffKeyWorkerVoiceService.Get();
            
            var staff = await _staffService.GetStaffs();
            List<CreateStaffKeyWorkerVoice> reports = new List<CreateStaffKeyWorkerVoice>();
            foreach (GetStaffKeyWorkerVoice item in entities)
            {
                var report = new CreateStaffKeyWorkerVoice();
                report.KeyWorkerId = item.KeyWorkerId;
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
            var model = new CreateStaffKeyWorkerVoice();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int workerId)
        {
            var client = await _clientService.GetClientDetail();
            var keyWorker = await _StaffKeyWorkerVoiceService.Get(workerId);
            var putEntity = new CreateStaffKeyWorkerVoice
            {
                KeyWorkerId = keyWorker.KeyWorkerId,
                Reference = keyWorker.Reference,
                Attachment = keyWorker.Attachment,
                Date = keyWorker.Date,
                Deadline = keyWorker.Deadline,
                URL = keyWorker.URL,
                Remarks = keyWorker.Remarks,
                Status = keyWorker.Status,
                ActionRequired = keyWorker.ActionRequired,
                NextCheckDate = keyWorker.NextCheckDate,
                ChangesWeNeed = keyWorker.ChangesWeNeed,
                Details = keyWorker.Details,
                NotComfortableServices = keyWorker.NotComfortableServices,
                MovingAndHandling = keyWorker.MovingAndHandling,
                OfficerName = keyWorker.OfficerToAct.Select(s => s.StaffName).ToList(),
                ServicesRequiresTime = keyWorker.ServicesRequiresTime,
                MedicationChanges = keyWorker.MedicationChanges,
                NutritionalChanges = keyWorker.NutritionalChanges,
                RiskAssessment = keyWorker.RiskAssessment,
                ServicesRequiresServices = keyWorker.ServicesRequiresServices,
                WellSupportedServices = keyWorker.WellSupportedServices,
                WorkteamName = keyWorker.Workteam.Select(s => s.StaffName).ToList(),
                StaffId = keyWorker.StaffId,
                HealthAndWellNessChanges = keyWorker.HealthAndWellNessChanges,
                OfficerToAct = keyWorker.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                TeamYouWorkFor = keyWorker.Workteam.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = keyWorker.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                WorkList = keyWorker.Workteam.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList(),
            };
            return View(putEntity);
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
            var client = await _clientService.GetClientDetail();
            var staffs = await _staffService.GetStaffs();
            var KeyWorker = _StaffKeyWorkerVoiceService.Get(workerId);

            var putEntity = new CreateStaffKeyWorkerVoice
            {
                KeyWorkerId = KeyWorker.Result.KeyWorkerId,
                Reference = KeyWorker.Result.Reference,
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
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList()
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
                var client = await _clientService.GetClientDetail();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.StaffId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffkeyworker";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
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
                var client = await _clientService.GetClientDetail();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string extention = model.StaffId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffkeyworker";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

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
