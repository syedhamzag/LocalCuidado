using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffOneToOne;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
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
    public class StaffOneToOneController : BaseController
    {
        private IStaffOneToOneService _StaffOneToOneService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffOneToOneController(IStaffOneToOneService StaffOneToOneService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffOneToOneService = StaffOneToOneService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffOneToOneService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffOneToOne();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.StaffId = staffId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffOneToOneService.GetByRef(Reference);
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
            var logAudit = await _StaffOneToOneService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffOneToOne.pdf");
            string subject = "StaffOneToOne";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffOneToOneService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "StaffOneToOne.pdf");
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
            var OneToOne = _StaffOneToOneService.GetByRef(Reference);
            foreach (var item in OneToOne.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.OneToOneId);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffOneToOne
            {
                OneToOneIds = Ids,
                Attachment = OneToOne.Result.FirstOrDefault().Attachment,
                Date = OneToOne.Result.FirstOrDefault().Date,
                Deadline = OneToOne.Result.FirstOrDefault().Deadline,
                URL = OneToOne.Result.FirstOrDefault().URL,
                OfficerToAct = officer,
                Remarks = OneToOne.Result.FirstOrDefault().Remarks,
                Status = OneToOne.Result.FirstOrDefault().Status,
                ActionRequired = OneToOne.Result.FirstOrDefault().ActionRequired,
                CurrentEventArea = OneToOne.Result.FirstOrDefault().CurrentEventArea,
                DecisionsReached = OneToOne.Result.FirstOrDefault().DecisionsReached,
                ImprovementRecorded = OneToOne.Result.FirstOrDefault().ImprovementRecorded,
                StaffImprovedInAreas = OneToOne.Result.FirstOrDefault().StaffImprovedInAreas,
                StaffId = OneToOne.Result.FirstOrDefault().StaffId,
                StaffConclusion = OneToOne.Result.FirstOrDefault().StaffConclusion,
                Purpose = OneToOne.Result.FirstOrDefault().Purpose,
                PreviousSupervision = OneToOne.Result.FirstOrDefault().PreviousSupervision,
                NextCheckDate = OneToOne.Result.FirstOrDefault().NextCheckDate,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()

            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffOneToOne model)
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

            List<PostStaffOneToOne> posts = new List<PostStaffOneToOne>();

            foreach (var officer in model.OfficerToAct)
            {
                var post = new PostStaffOneToOne();
                post.Attachment = model.Attachment;
                post.Reference = model.Reference;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = officer;
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.CurrentEventArea = model.CurrentEventArea;
                post.DecisionsReached = model.DecisionsReached;
                post.ImprovementRecorded = model.ImprovementRecorded;
                post.StaffImprovedInAreas = model.StaffImprovedInAreas;
                post.StaffId = model.StaffId;
                post.StaffConclusion = model.StaffConclusion;
                post.Purpose = model.Purpose;
                post.PreviousSupervision = model.PreviousSupervision;
                post.NextCheckDate = model.NextCheckDate;
                posts.Add(post);
            }

                var result = await _StaffOneToOneService.Create(posts);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New One to One successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffOneToOne model)
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
            List<PutStaffOneToOne> puts = new List<PutStaffOneToOne>();
            int count = model.OneToOneIds.Count;
            int i = 0;
            foreach (var officer in model.OfficerToAct)
            {
                var put = new PutStaffOneToOne();
                if (i < count)
                    put.OneToOneId = model.OneToOneIds[i];
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = officer;
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.CurrentEventArea = model.CurrentEventArea;
                put.DecisionsReached = model.DecisionsReached;
                put.ImprovementRecorded = model.ImprovementRecorded;
                put.StaffImprovedInAreas = model.StaffImprovedInAreas;
                put.StaffId = model.StaffId;
                put.StaffConclusion = model.StaffConclusion;
                put.Purpose = model.Purpose;
                put.PreviousSupervision = model.PreviousSupervision;
                put.NextCheckDate = model.NextCheckDate;
                puts.Add(put);
            }
            var entity = await _StaffOneToOneService.Put(puts);
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
