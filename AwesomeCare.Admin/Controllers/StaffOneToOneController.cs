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
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffOneToOneController : BaseController
    {
        private IStaffOneToOneService _StaffOneToOneService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private IBaseRecordService _baseService;

        public StaffOneToOneController(IStaffOneToOneService StaffOneToOneService, IFileUpload fileUpload,
             IStaffService staffService, IBaseRecordService baseService, IEmailService emailService) : base(fileUpload)
        {
            _StaffOneToOneService = StaffOneToOneService;
            _staffService = staffService;
            _emailService = emailService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffOneToOneService.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffOneToOne> reports = new List<CreateStaffOneToOne>();
            foreach (GetStaffOneToOne item in entities)
            {
                var report = new CreateStaffOneToOne();
                report.OneToOneId = item.OneToOneId;
                report.Date = item.Date;
                report.NextCheckDate = item.NextCheckDate;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffId).Select(s => s.Fullname).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffOneToOne();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.StaffId = staffId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(int oneToOneId)
        {
            var OneToOne = _StaffOneToOneService.Get(oneToOneId);
            var putEntity = new CreateStaffOneToOne
            {
                OneToOneId = OneToOne.Result.OneToOneId,
                Attachment = OneToOne.Result.Attachment,
                Date = OneToOne.Result.Date,
                Deadline = OneToOne.Result.Deadline,
                URL = OneToOne.Result.URL,
                Remarks = OneToOne.Result.Remarks,
                Reference = OneToOne.Result.Reference,
                Status = OneToOne.Result.Status,
                ActionRequired = OneToOne.Result.ActionRequired,
                CurrentEventArea = OneToOne.Result.CurrentEventArea,
                DecisionsReached = OneToOne.Result.DecisionsReached,
                ImprovementRecorded = OneToOne.Result.ImprovementRecorded,
                StaffImprovedInAreas = OneToOne.Result.StaffImprovedInAreas,
                StaffId = OneToOne.Result.StaffId,
                StaffConclusion = OneToOne.Result.StaffConclusion,
                Purpose = OneToOne.Result.Purpose,
                PreviousSupervision = OneToOne.Result.PreviousSupervision,
                NextCheckDate = OneToOne.Result.NextCheckDate,
                OfficerToAct = OneToOne.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = OneToOne.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);

        }
        public async Task<IActionResult> Email(int oneToOneId, string sender, string password, string recipient, string Smtp)
        {
            var oneToOne = await _StaffOneToOneService.Get(oneToOneId);
            var json = JsonConvert.SerializeObject(oneToOne);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffOneToOne.pdf");
            string subject = "StaffOneToOne";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int oneToOneId)
        {
            var oneToOne = await _StaffOneToOneService.Get(oneToOneId);
            var json = JsonConvert.SerializeObject(oneToOne);
            byte[] byte1 = GeneratePdf(json);

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

        public async Task<IActionResult> Edit(int oneToOneId)
        {
            var OneToOne = _StaffOneToOneService.Get(oneToOneId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffOneToOne
            {
                OneToOneId = OneToOne.Result.OneToOneId,
                Reference = OneToOne.Result.Reference,
                Attachment = OneToOne.Result.Attachment,
                Date = OneToOne.Result.Date,
                Deadline = OneToOne.Result.Deadline,
                URL = OneToOne.Result.URL,
                OfficerToAct = OneToOne.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = OneToOne.Result.Remarks,
                Status = OneToOne.Result.Status,
                ActionRequired = OneToOne.Result.ActionRequired,
                CurrentEventArea = OneToOne.Result.CurrentEventArea,
                DecisionsReached = OneToOne.Result.DecisionsReached,
                ImprovementRecorded = OneToOne.Result.ImprovementRecorded,
                StaffImprovedInAreas = OneToOne.Result.StaffImprovedInAreas,
                StaffId = OneToOne.Result.StaffId,
                StaffConclusion = OneToOne.Result.StaffConclusion,
                Purpose = OneToOne.Result.Purpose,
                PreviousSupervision = OneToOne.Result.PreviousSupervision,
                NextCheckDate = OneToOne.Result.NextCheckDate,
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
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

                var post = new PostStaffOneToOne();
                post.Attachment = model.Attachment;
                post.Reference = model.Reference;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = model.OfficerToAct.Select(s => new PostOneToOneOfficerToAct { StaffPersonalInfoId = s, OneToOneId = model.OneToOneId }).ToList();
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


                var result = await _StaffOneToOneService.Create(post);
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
                var put = new PutStaffOneToOne();
                put.OneToOneId = model.OneToOneId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = model.OfficerToAct.Select(s => new PutOneToOneOfficerToAct { StaffPersonalInfoId = s, OneToOneId = model.OneToOneId }).ToList();
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

            var entity = await _StaffOneToOneService.Put(put);
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
