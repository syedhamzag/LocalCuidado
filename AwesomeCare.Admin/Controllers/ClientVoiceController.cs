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
            var client = await _clientService.GetClients();
            var baserecord = await _baseService.GetBaseRecordsWithItems();
            List<CreateClientVoice> reports = new List<CreateClientVoice>();
            foreach (GetClientVoice item in entities)
            {
                var report = new CreateClientVoice();
                report.VoiceId = item.VoiceId;
                report.Reference = item.Reference;
                report.NextCheckDate = item.NextCheckDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.Firstname).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientVoice();
            var client = await _clientService.GetClient(clientId.Value);
            model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;      
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var Voice = await _clientVoiceService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in Voice)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(Voice.FirstOrDefault());
            var newJson = json + staffName;
            return View(Voice.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference, string sender, string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var Voice = await _clientVoiceService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in Voice)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(Voice.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientVoice.pdf");
            string subject = "ClientVoice";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var Voice = await _clientVoiceService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in Voice)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(Voice.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

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

        public async Task<IActionResult> Edit(string Reference)
        {
            List<int> officer = new List<int>();
            List<int> Ids = new List<int>();
            var Voice = _clientVoiceService.GetByRef(Reference);
            foreach (var item in Voice.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.VoiceId);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientVoice
            {
                VoiceIds = Ids,
                ClientId = Voice.Result.FirstOrDefault().ClientId,
                Attachment = Voice.Result.FirstOrDefault().Attachment,
                Date = Voice.Result.FirstOrDefault().Date,
                Deadline = Voice.Result.FirstOrDefault().Deadline,
                EvidenceOfActionTaken = Voice.Result.FirstOrDefault().EvidenceOfActionTaken,
                LessonLearntAndShared = Voice.Result.FirstOrDefault().LessonLearntAndShared,
                URL = Voice.Result.FirstOrDefault().URL,
                NameOfCaller = Voice.Result.FirstOrDefault().NameOfCaller,
                OfficerToAct = officer,
                Remarks = Voice.Result.FirstOrDefault().Remarks,
                RotCause = Voice.Result.FirstOrDefault().RotCause,
                Status = Voice.Result.FirstOrDefault().Status,
                ActionRequired = Voice.Result.FirstOrDefault().ActionRequired,
                ActionsTakenByMPCC = Voice.Result.FirstOrDefault().ActionsTakenByMPCC,
                AreasOfImprovements = Voice.Result.FirstOrDefault().AreasOfImprovements,
                HealthGoalLongTerm = Voice.Result.FirstOrDefault().HealthGoalLongTerm,
                HealthGoalShortTerm = Voice.Result.FirstOrDefault().HealthGoalShortTerm,
                InterestedInPrograms = Voice.Result.FirstOrDefault().InterestedInPrograms,
                NextCheckDate = Voice.Result.FirstOrDefault().NextCheckDate,
                OfficeStaffSupport = Voice.Result.FirstOrDefault().OfficeStaffSupport,
                RateServiceRecieving = Voice.Result.FirstOrDefault().RateServiceRecieving,
                RateStaffAttending = Voice.Result.FirstOrDefault().RateStaffAttending,
                SomethingSpecial = Voice.Result.FirstOrDefault().SomethingSpecial,
                StaffBestSupport = Voice.Result.FirstOrDefault().StaffBestSupport,
                StaffPoorSupport = Voice.Result.FirstOrDefault().StaffPoorSupport,
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
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            if (model.Evidence != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Evidence_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Evidence.OpenReadStream());
                model.Attachment = pathA;

            }

            #endregion

            List<PostClientVoice> posts = new List<PostClientVoice>();
            foreach (var item in model.OfficerToAct)
            {
                var post = new PostClientVoice();
                post.ClientId = model.ClientId;
                post.Reference = model.Reference;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                post.LessonLearntAndShared = model.LessonLearntAndShared;
                post.URL = model.URL;
                post.NameOfCaller = model.NameOfCaller;
                post.OfficerToAct = item;
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
                post.StaffBestSupport = model.StaffBestSupport;
                post.StaffPoorSupport = model.StaffPoorSupport;
                posts.Add(post);
            }

            var result = await _clientVoiceService.Create(posts);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Voice successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientVoice model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
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
            int count = model.VoiceIds.Count;
            int i = 0;
            List<PutClientVoice> puts = new List<PutClientVoice>();
            foreach (var item in model.OfficerToAct)
            {
                var put = new PutClientVoice();
            if (i < count)
                put.VoiceId = model.VoiceIds[i];
                put.ClientId = model.ClientId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                put.LessonLearntAndShared = model.LessonLearntAndShared;
                put.URL = model.URL;
                put.NameOfCaller = model.NameOfCaller;
                put.OfficerToAct = item;
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
                put.StaffBestSupport = model.StaffBestSupport;
                put.StaffPoorSupport = model.StaffPoorSupport;
                puts.Add(put);

        }
        var entity = await _clientVoiceService.Put(puts);
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
