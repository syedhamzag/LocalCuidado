using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientMgtVisit;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientMgtVisit;
using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
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
    public class ClientMgtVisitController : BaseController
    {
        private IClientMgtVisitService _clientMgtVisitService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientMgtVisitController(IClientMgtVisitService clientMgtVisitService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _clientMgtVisitService = clientMgtVisitService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientMgtVisitService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientMgtVisit();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var MgtVisit = await _clientMgtVisitService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in MgtVisit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(MgtVisit.FirstOrDefault());
            var newJson = json + staffName;
            return View(MgtVisit.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference, string sender, string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var MgtVisit = await _clientMgtVisitService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in MgtVisit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(MgtVisit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientMgtVisit.pdf");
            string subject = "ClientMgtVisit";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var MgtVisit = await _clientMgtVisitService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in MgtVisit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(MgtVisit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "ClientMgtVisit.pdf");
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
            var MgtVisit = _clientMgtVisitService.GetByRef(Reference);
            foreach (var item in MgtVisit.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.VisitId);     //MgtVisitId > VisitID to remove Error
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientMgtVisit
            {
                VisitIds = Ids,
                Reference = MgtVisit.Result.FirstOrDefault().Reference,
                ClientId = MgtVisit.Result.FirstOrDefault().ClientId,
                Attachment = MgtVisit.Result.FirstOrDefault().Attachment,
                Date = MgtVisit.Result.FirstOrDefault().Date,
                Deadline = MgtVisit.Result.FirstOrDefault().Deadline,
                EvidenceOfActionTaken = MgtVisit.Result.FirstOrDefault().EvidenceOfActionTaken,
                LessonLearntAndShared = MgtVisit.Result.FirstOrDefault().LessonLearntAndShared,
                URL = MgtVisit.Result.FirstOrDefault().URL,
                HowToComplain = MgtVisit.Result.FirstOrDefault().HowToComplain,
                OfficerToAct = officer,
                Remarks = MgtVisit.Result.FirstOrDefault().Remarks,
                RotCause = MgtVisit.Result.FirstOrDefault().RotCause,
                Status = MgtVisit.Result.FirstOrDefault().Status,
                ActionRequired = MgtVisit.Result.FirstOrDefault().ActionRequired,
                ActionsTakenByMPCC = MgtVisit.Result.FirstOrDefault().ActionsTakenByMPCC,
                ImprovementExpect = MgtVisit.Result.FirstOrDefault().ImprovementExpect,
                Observation = MgtVisit.Result.FirstOrDefault().Observation,
                RateManagers = MgtVisit.Result.FirstOrDefault().RateManagers,
                ServiceRecommended = MgtVisit.Result.FirstOrDefault().ServiceRecommended,
                NextCheckDate = MgtVisit.Result.FirstOrDefault().NextCheckDate,
                RateServiceRecieving = MgtVisit.Result.FirstOrDefault().RateServiceRecieving,
                StaffBestSupport = MgtVisit.Result.FirstOrDefault().StaffBestSupport,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientMgtVisit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Evidence != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Evidence_", model.ClientId, model.NextCheckDate.TimeOfDay);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.EvidenceOfActionTaken = path;
            }
            #endregion
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId, model.NextCheckDate.TimeOfDay);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
            }
            #endregion
            List<PostClientMgtVisit> posts = new List<PostClientMgtVisit>();
            foreach (var item in model.OfficerToAct)
            {
                var post = new PostClientMgtVisit();
                post.Reference = model.Reference;
                post.ClientId = model.ClientId;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                post.LessonLearntAndShared = model.LessonLearntAndShared;
                post.URL = model.URL;
                post.HowToComplain = model.HowToComplain;
                post.OfficerToAct = item;
                post.Remarks = model.Remarks;
                post.RotCause = model.RotCause;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.ActionsTakenByMPCC = model.ActionsTakenByMPCC;
                post.ImprovementExpect = model.ImprovementExpect;
                post.Observation = model.Observation;
                post.RateManagers = model.RateManagers;
                post.ServiceRecommended = model.ServiceRecommended;
                post.NextCheckDate = model.NextCheckDate;
                post.RateServiceRecieving = model.RateServiceRecieving;
                post.StaffBestSupport = model.StaffBestSupport;
                posts.Add(post);
            }

            var result = await _clientMgtVisitService.Create(posts);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Mgt Visit successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientMgtVisit model)
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
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
            int count = model.VisitIds.Count;
            int i = 0;
            List<PutClientMgtVisit> puts = new List<PutClientMgtVisit>();
            foreach (var item in model.OfficerToAct)
            {
                var put = new PutClientMgtVisit();
                if (i < count)
                    put.VisitId = model.VisitIds[i];
                put.Reference = model.Reference;
                put.ClientId = model.ClientId;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                put.LessonLearntAndShared = model.LessonLearntAndShared;
                put.URL = model.URL;
                put.HowToComplain = model.HowToComplain;
                put.OfficerToAct = item;
                put.Remarks = model.Remarks;
                put.RotCause = model.RotCause;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.ActionsTakenByMPCC = model.ActionsTakenByMPCC;
                put.ImprovementExpect = model.ImprovementExpect;
                put.Observation = model.Observation;
                put.RateManagers = model.RateManagers;
                put.ServiceRecommended = model.ServiceRecommended;
                put.NextCheckDate = model.NextCheckDate;
                put.RateServiceRecieving = model.RateServiceRecieving;
                put.StaffBestSupport = model.StaffBestSupport;
                puts.Add(put);
            }
            var entity = await _clientMgtVisitService.Put(puts);
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
