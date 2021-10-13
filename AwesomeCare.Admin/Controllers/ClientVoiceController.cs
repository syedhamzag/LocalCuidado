using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientVoice;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientVoice;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
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
    public class ClientVoiceController : BaseController
    {
        private IClientVoiceService _clientVoiceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private IBaseRecordService _baseService;
        public ClientVoiceController(IClientVoiceService clientVoiceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientVoiceService = clientVoiceService;
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
            var entities = await _clientVoiceService.Get();
            var client = await _clientService.GetClientDetail();
            List<CreateClientVoice> reports = new List<CreateClientVoice>();
            foreach (GetClientVoice item in entities)
            {
                var report = new CreateClientVoice();
                report.VoiceId = item.VoiceId;
                report.Date = item.Date;
                report.NextCheckDate = item.NextCheckDate;
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
            var model = new CreateClientVoice();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(int vId)
        {
            var Voice = await _clientVoiceService.Get(vId);
            var putEntity = new CreateClientVoice
            {
                VoiceId = Voice.VoiceId,
                ClientId = Voice.ClientId,
                Reference = Voice.Reference,
                Attachment = Voice.Attachment,
                Date = Voice.Date,
                Deadline = Voice.Deadline,
                EvidenceOfActionTaken = Voice.EvidenceOfActionTaken,
                LessonLearntAndShared = Voice.LessonLearntAndShared,
                URL = Voice.URL,
                Remarks = Voice.Remarks,
                RotCause = Voice.RotCause,
                Status = Voice.Status,
                ActionRequired = Voice.ActionRequired,
                ActionsTakenByMPCC = Voice.ActionsTakenByMPCC,
                AreasOfImprovements = Voice.AreasOfImprovements,
                HealthGoalLongTerm = Voice.HealthGoalLongTerm,
                HealthGoalShortTerm = Voice.HealthGoalShortTerm,
                InterestedInPrograms = Voice.InterestedInPrograms,
                NextCheckDate = Voice.NextCheckDate,
                OfficeStaffSupport = Voice.OfficeStaffSupport,
                RateServiceRecieving = Voice.RateServiceRecieving,
                RateStaffAttending = Voice.RateStaffAttending,
                SomethingSpecial = Voice.SomethingSpecial,
                NameOfCaller = Voice.CallerName.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToAct = Voice.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffBestSupport = Voice.GoodStaff.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffPoorSupport = Voice.PoorStaff.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = Voice.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                CallerList = Voice.CallerName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                BestSupportList = Voice.GoodStaff.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PoorSupportList = Voice.PoorStaff.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int vId, string sender, string password, string recipient, string Smtp)
        {
            var Voice = await _clientVoiceService.Get(vId);
            var json = JsonConvert.SerializeObject(Voice);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientVoice.pdf");
            string subject = "ClientVoice";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int vId)
        {
            var Voice = await _clientVoiceService.Get(vId);
            var json = JsonConvert.SerializeObject(Voice);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientVoice.pdf");
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

        public async Task<IActionResult> Edit(int VoiceId)
        {
            var Voice = _clientVoiceService.Get(VoiceId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientVoice
            {
                VoiceId = Voice.Result.VoiceId,
                ClientId = Voice.Result.ClientId,
                Reference = Voice.Result.Reference,
                Attachment = Voice.Result.Attachment,
                Date = Voice.Result.Date,
                Deadline = Voice.Result.Deadline,
                EvidenceOfActionTaken = Voice.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = Voice.Result.LessonLearntAndShared,
                URL = Voice.Result.URL,
                NameOfCaller = Voice.Result.CallerName.Select(s=>s.StaffPersonalInfoId).ToList(),
                OfficerToAct = Voice.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = Voice.Result.Remarks,
                RotCause = Voice.Result.RotCause,
                Status = Voice.Result.Status,
                ActionRequired = Voice.Result.ActionRequired,
                ActionsTakenByMPCC = Voice.Result.ActionsTakenByMPCC,
                AreasOfImprovements = Voice.Result.AreasOfImprovements,
                HealthGoalLongTerm = Voice.Result.HealthGoalLongTerm,
                HealthGoalShortTerm = Voice.Result.HealthGoalShortTerm,
                InterestedInPrograms = Voice.Result.InterestedInPrograms,
                NextCheckDate = Voice.Result.NextCheckDate,
                OfficeStaffSupport = Voice.Result.OfficeStaffSupport,
                RateServiceRecieving = Voice.Result.RateServiceRecieving,
                RateStaffAttending = Voice.Result.RateStaffAttending,
                SomethingSpecial = Voice.Result.SomethingSpecial,
                StaffBestSupport = Voice.Result.GoodStaff.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffPoorSupport = Voice.Result.PoorStaff.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientVoice model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientVoice post = new PostClientVoice();

            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientvoice";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                model.Attachment = "No Image";
            }
            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientvoice";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;

            }
            else
            {
                model.EvidenceOfActionTaken = "No Image";
            }

            #endregion

            
                post.ClientId = model.ClientId;
                post.Reference = model.Reference;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                post.LessonLearntAndShared = model.LessonLearntAndShared;
                post.URL = model.URL;
                post.CallerName = model.OfficerToAct.Select(o => new PostVoiceCallerName{ StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList();
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostVoiceOfficerToAct { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList();
                post.Remarks = model.Remarks;
                post.RotCause = model.RotCause;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.ActionsTakenByMPCC = model.ActionsTakenByMPCC;
                post.AreasOfImprovements = model.AreasOfImprovements;
                post.HealthGoalLongTerm = model.HealthGoalLongTerm;
                post.HealthGoalShortTerm = model.HealthGoalShortTerm;
                post.InterestedInPrograms = model.InterestedInPrograms;
                post.NextCheckDate = model.NextCheckDate;
                post.OfficeStaffSupport = model.OfficeStaffSupport;
                post.RateServiceRecieving = model.RateServiceRecieving;
                post.RateStaffAttending = model.RateStaffAttending;
                post.SomethingSpecial = model.SomethingSpecial;
                post.GoodStaff = model.StaffBestSupport.Select(o => new PostVoiceGoodStaff { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList();
                post.PoorStaff = model.StaffPoorSupport.Select(o => new PostVoicePoorStaff { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList(); ;
                
            var result = await _clientVoiceService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Voice successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientVoice model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Evidence
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientvoice";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                model.Attachment = model.Attachment;
            }

            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientvoice";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;

            }
            else
            {
                model.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
            }
            #endregion

            PutClientVoice put = new PutClientVoice();
                put.VoiceId = model.VoiceId;
                put.ClientId = model.ClientId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                put.LessonLearntAndShared = model.LessonLearntAndShared;
                put.URL = model.URL;
                put.CallerName = model.NameOfCaller.Select(o => new PutVoiceCallerName { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList();
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutVoiceOfficerToAct { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList();
                put.Remarks = model.Remarks;
                put.RotCause = model.RotCause;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.ActionsTakenByMPCC = model.ActionsTakenByMPCC;
                put.AreasOfImprovements = model.AreasOfImprovements;
                put.HealthGoalLongTerm = model.HealthGoalLongTerm;
                put.HealthGoalShortTerm = model.HealthGoalShortTerm;
                put.InterestedInPrograms = model.InterestedInPrograms;
                put.NextCheckDate = model.NextCheckDate;
                put.OfficeStaffSupport = model.OfficeStaffSupport;
                put.RateServiceRecieving = model.RateServiceRecieving;
                put.RateStaffAttending = model.RateStaffAttending;
                put.SomethingSpecial = model.SomethingSpecial;
                put.GoodStaff = model.StaffBestSupport.Select(o => new PutVoiceGoodStaff { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList(); ;
                put.PoorStaff = model.StaffPoorSupport.Select(o => new PutVoicePoorStaff { StaffPersonalInfoId = o, VoiceId = model.VoiceId }).ToList();

            var entity = await _clientVoiceService.Put(put);
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
