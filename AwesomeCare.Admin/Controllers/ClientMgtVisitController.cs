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
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.ClientVisit;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientMgtVisitController : BaseController
    {
        private IClientMgtVisitService _clientMgtVisitService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientMgtVisitController(IClientMgtVisitService clientMgtVisitService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientMgtVisitService = clientMgtVisitService;
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
            var entities = await _clientMgtVisitService.Get();
            var client = await _clientService.GetClientDetail();
            List<CreateClientMgtVisit> reports = new List<CreateClientMgtVisit>();
            foreach (GetClientMgtVisit item in entities)
            {
                var report = new CreateClientMgtVisit();
                report.VisitId = item.VisitId;
                report.Reference = item.Reference;
                report.NextCheckDate = item.NextCheckDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientMgtVisit();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(int mgtId)
        {
            var MgtVisit = await _clientMgtVisitService.Get(mgtId);
            var putEntity = new CreateClientMgtVisit
            {
                VisitId = MgtVisit.VisitId,
                Reference = MgtVisit.Reference,
                ClientId = MgtVisit.ClientId,
                Attachment = MgtVisit.Attachment,
                Date = MgtVisit.Date,
                Deadline = MgtVisit.Deadline,
                EvidenceOfActionTaken = MgtVisit.EvidenceOfActionTaken,
                LessonLearntAndShared = MgtVisit.LessonLearntAndShared,
                URL = MgtVisit.URL,
                HowToComplain = MgtVisit.HowToComplain,
                Remarks = MgtVisit.Remarks,
                RotCause = MgtVisit.RotCause,
                Status = MgtVisit.Status,
                ActionRequired = MgtVisit.ActionRequired,
                ActionsTakenByMPCC = MgtVisit.ActionsTakenByMPCC,
                ImprovementExpect = MgtVisit.ImprovementExpect,
                Observation = MgtVisit.Observation,
                RateManagers = MgtVisit.RateManagers,
                ServiceRecommended = MgtVisit.ServiceRecommended,
                NextCheckDate = MgtVisit.NextCheckDate,
                RateServiceRecieving = MgtVisit.RateServiceRecieving,
                OfficerToAct = MgtVisit.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffBestSupport = MgtVisit.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = MgtVisit.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffList = MgtVisit.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int mgtId, string sender, string password, string recipient, string Smtp)
        {
            var MgtVisit = await _clientMgtVisitService.Get(mgtId);
            var json = JsonConvert.SerializeObject(MgtVisit);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientMgtVisit.pdf");
            string subject = "ClientMgtVisit";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int mgtId)
        {
            var MgtVisit = await _clientMgtVisitService.Get(mgtId);
            var json = JsonConvert.SerializeObject(MgtVisit);
            byte[] byte1 = GeneratePdf(json);

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

        public async Task<IActionResult> Edit(int VisitId)
        {
            var MgtVisit = _clientMgtVisitService.Get(VisitId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientMgtVisit
            {
                VisitId = MgtVisit.Result.VisitId,
                Reference = MgtVisit.Result.Reference,
                ClientId = MgtVisit.Result.ClientId,
                Attachment = MgtVisit.Result.Attachment,
                Date = MgtVisit.Result.Date,
                Deadline = MgtVisit.Result.Deadline,
                EvidenceOfActionTaken = MgtVisit.Result.EvidenceOfActionTaken,
                LessonLearntAndShared = MgtVisit.Result.LessonLearntAndShared,
                URL = MgtVisit.Result.URL,
                HowToComplain = MgtVisit.Result.HowToComplain,
                OfficerToAct = MgtVisit.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = MgtVisit.Result.Remarks,
                RotCause = MgtVisit.Result.RotCause,
                Status = MgtVisit.Result.Status,
                ActionRequired = MgtVisit.Result.ActionRequired,
                ActionsTakenByMPCC = MgtVisit.Result.ActionsTakenByMPCC,
                ImprovementExpect = MgtVisit.Result.ImprovementExpect,
                Observation = MgtVisit.Result.Observation,
                RateManagers = MgtVisit.Result.RateManagers,
                ServiceRecommended = MgtVisit.Result.ServiceRecommended,
                NextCheckDate = MgtVisit.Result.NextCheckDate,
                RateServiceRecieving = MgtVisit.Result.RateServiceRecieving,
                StaffBestSupport = MgtVisit.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
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
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientMgtVisit postlog = new PostClientMgtVisit();

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
                model.EvidenceOfActionTaken = "No Image";
            }
            #endregion
            #region Attachment
            if (model.Attach != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Attach_", model.ClientId);
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
                postlog.Attachment = model.Attachment;
                postlog.Date = model.Date;
                postlog.Deadline = model.Deadline;
                postlog.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                postlog.LessonLearntAndShared = model.LessonLearntAndShared;
                postlog.URL = model.URL;
                postlog.HowToComplain = model.HowToComplain;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostVisitOfficerToAct { StaffPersonalInfoId = o, VisitId = model.VisitId }).ToList();
                postlog.Remarks = model.Remarks;
                postlog.RotCause = model.RotCause;
                postlog.Status = model.Status;
                postlog.ActionRequired = model.ActionRequired;
                postlog.ActionsTakenByMPCC = model.ActionsTakenByMPCC;
                postlog.ImprovementExpect = model.ImprovementExpect;
                postlog.Observation = model.Observation;
                postlog.RateManagers = model.RateManagers;
                postlog.ServiceRecommended = model.ServiceRecommended;
                postlog.NextCheckDate = model.NextCheckDate;
                postlog.RateServiceRecieving = model.RateServiceRecieving;
                postlog.StaffName = model.StaffBestSupport.Select(o => new PostVisitStaffName { StaffPersonalInfoId = o, VisitId = model.VisitId }).ToList();

            var result = await _clientMgtVisitService.Create(postlog);
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

                PutClientMgtVisit put = new PutClientMgtVisit();
                put.VisitId = model.VisitId;
                put.Reference = model.Reference;
                put.ClientId = model.ClientId;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.EvidenceOfActionTaken = model.EvidenceOfActionTaken;
                put.LessonLearntAndShared = model.LessonLearntAndShared;
                put.URL = model.URL;
                put.HowToComplain = model.HowToComplain;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutVisitOfficerToAct { StaffPersonalInfoId = o, VisitId = model.VisitId }).ToList();
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
                put.StaffName = model.StaffBestSupport.Select(o => new PutVisitStaffName { StaffPersonalInfoId = o, VisitId = model.VisitId }).ToList();
            var json = JsonConvert.SerializeObject(put);
            var entity = await _clientMgtVisitService.Put(put);
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
