using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSupervisionAppraisal;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervisionAppraisal;
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

namespace AwesomeCare.Admin.Controllers
{
    public class StaffSupervisionAppraisalController : BaseController
    {
        private IStaffSupervisionAppraisalService _StaffSupervisionAppraisalService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffSupervisionAppraisalController(IStaffSupervisionAppraisalService StaffSupervisionAppraisalService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffSupervisionAppraisalService = StaffSupervisionAppraisalService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSupervisionAppraisalService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffSupervisionAppraisal();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffSupervisionAppraisalService.GetByRef(Reference);
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
            var logAudit = await _StaffSupervisionAppraisalService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffSupervisionAppraisal.pdf");
            string subject = "StaffSupervisionAppraisal";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffSupervisionAppraisalService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "StaffSupervisionAppraisal.pdf");
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
            var superVision = _StaffSupervisionAppraisalService.GetByRef(Reference);
            foreach (var item in superVision.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.StaffSupervisionAppraisalId);
            }
            var staffs = await _staffService.GetStaffs();
            var putEntity = new CreateStaffSupervisionAppraisal
            {
                StaffSupervisionAppraisalIds = Ids,
                Reference = superVision.Result.FirstOrDefault().Reference,
                Attachment = superVision.Result.FirstOrDefault().Attachment,
                Date = superVision.Result.FirstOrDefault().Date,
                Deadline = superVision.Result.FirstOrDefault().Deadline,
                URL = superVision.Result.FirstOrDefault().URL,
                Remarks = superVision.Result.FirstOrDefault().Remarks,
                Status = superVision.Result.FirstOrDefault().Status,
                ActionRequired = superVision.Result.FirstOrDefault().ActionRequired,
                NextCheckDate = superVision.Result.FirstOrDefault().NextCheckDate,
                WorkTeam = superVision.Result.FirstOrDefault().WorkTeam,
                OfficerToAct = officer,
                CondourAndWhistleBlowing = superVision.Result.FirstOrDefault().CondourAndWhistleBlowing,
                Details = superVision.Result.FirstOrDefault().Details,
                FiveStarRating = superVision.Result.FirstOrDefault().FiveStarRating,
                NoAbilityToSupport = superVision.Result.FirstOrDefault().NoAbilityToSupport,
                NoCondourAndWhistleBlowing = superVision.Result.FirstOrDefault().NoCondourAndWhistleBlowing,
                ProfessionalDevelopment = superVision.Result.FirstOrDefault().ProfessionalDevelopment,
                StaffAbility = superVision.Result.FirstOrDefault().StaffAbility,
                StaffComplaints = superVision.Result.FirstOrDefault().StaffComplaints,
                StaffDevelopment = superVision.Result.FirstOrDefault().StaffDevelopment,
                StaffRating = superVision.Result.FirstOrDefault().StaffRating,
                StaffSupportAreas = superVision.Result.FirstOrDefault().StaffSupportAreas,
                StaffId = superVision.Result.FirstOrDefault().StaffId,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffSupervisionAppraisal model)
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
            List<PostStaffSupervisionAppraisal> posts = new List<PostStaffSupervisionAppraisal>();
            foreach (var item in model.OfficerToAct)
            {
                var post = new PostStaffSupervisionAppraisal();
                post.Reference = model.Reference;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.NextCheckDate = model.NextCheckDate;
                post.WorkTeam = model.WorkTeam;
                post.OfficerToAct = item;
                post.CondourAndWhistleBlowing = model.CondourAndWhistleBlowing;
                post.Details = model.Details;
                post.FiveStarRating = model.FiveStarRating;
                post.NoAbilityToSupport = model.NoAbilityToSupport;
                post.NoCondourAndWhistleBlowing = model.NoCondourAndWhistleBlowing;
                post.ProfessionalDevelopment = model.ProfessionalDevelopment;
                post.StaffAbility = model.StaffAbility;
                post.StaffComplaints = model.StaffComplaints;
                post.StaffDevelopment = model.StaffDevelopment;
                post.StaffRating = model.StaffRating;
                post.StaffSupportAreas = model.StaffSupportAreas;
                post.StaffId = model.StaffId;
                posts.Add(post);
            }
            var result = await _StaffSupervisionAppraisalService.Create(posts);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Supervision Appraisal successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffSupervisionAppraisal model)
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
            int count = model.StaffSupervisionAppraisalIds.Count;
            int i = 0;
            List<PutStaffSupervisionAppraisal> puts = new List<PutStaffSupervisionAppraisal>();
            foreach (var item in model.OfficerToAct)
            {
                var put = new PutStaffSupervisionAppraisal();
                if (i < count)
                    put.StaffSupervisionAppraisalId = model.StaffSupervisionAppraisalIds[i];
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.WorkTeam = model.WorkTeam;
                put.OfficerToAct = item;
                put.CondourAndWhistleBlowing = model.CondourAndWhistleBlowing;
                put.Details = model.Details;
                put.FiveStarRating = model.FiveStarRating;
                put.NoAbilityToSupport = model.NoAbilityToSupport;
                put.NoCondourAndWhistleBlowing = model.NoCondourAndWhistleBlowing;
                put.ProfessionalDevelopment = model.ProfessionalDevelopment;
                put.StaffAbility = model.StaffAbility;
                put.StaffComplaints = model.StaffComplaints;
                put.StaffDevelopment = model.StaffDevelopment;
                put.StaffRating = model.StaffRating;
                put.StaffSupportAreas = model.StaffSupportAreas;
                put.StaffId = model.StaffId;
                puts.Add(put);
            }
            var entity = await _StaffSupervisionAppraisalService.Put(puts);
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
