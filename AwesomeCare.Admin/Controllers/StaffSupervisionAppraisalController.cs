using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSupervisionAppraisal;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervision;
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
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffSupervisionAppraisalController : BaseController
    {
        private IStaffSupervisionAppraisalService _StaffSupervisionAppraisalService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;

        public StaffSupervisionAppraisalController(IStaffSupervisionAppraisalService StaffSupervisionAppraisalService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IBaseRecordService baseService, IEmailService emailService) : base(fileUpload)
        {
            _StaffSupervisionAppraisalService = StaffSupervisionAppraisalService;
            _baseService = baseService;
            _staffService = staffService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSupervisionAppraisalService.Get();
            var staff = await _staffService.GetStaffs();
            List<CreateStaffSupervisionAppraisal> reports = new List<CreateStaffSupervisionAppraisal>();
            foreach (GetStaffSupervisionAppraisal item in entities)
            {
                var report = new CreateStaffSupervisionAppraisal();
                report.StaffSupervisionAppraisalId = item.StaffSupervisionAppraisalId;
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
            var model = new CreateStaffSupervisionAppraisal();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(int visionId)
        {
            var superVision = _StaffSupervisionAppraisalService.Get(visionId);

            var putEntity = new CreateStaffSupervisionAppraisal
            {
                StaffSupervisionAppraisalId = superVision.Result.StaffSupervisionAppraisalId,
                Reference = superVision.Result.Reference,
                Attachment = superVision.Result.Attachment,
                Date = superVision.Result.Date,
                Deadline = superVision.Result.Deadline,
                URL = superVision.Result.URL,
                Remarks = superVision.Result.Remarks,
                Status = superVision.Result.Status,
                ActionRequired = superVision.Result.ActionRequired,
                NextCheckDate = superVision.Result.NextCheckDate,
                CondourAndWhistleBlowing = superVision.Result.CondourAndWhistleBlowing,
                Details = superVision.Result.Details,
                FiveStarRating = superVision.Result.FiveStarRating,
                NoAbilityToSupport = superVision.Result.NoAbilityToSupport,
                NoCondourAndWhistleBlowing = superVision.Result.NoCondourAndWhistleBlowing,
                ProfessionalDevelopment = superVision.Result.ProfessionalDevelopment,
                StaffAbility = superVision.Result.StaffAbility,
                StaffComplaints = superVision.Result.StaffComplaints,
                StaffDevelopment = superVision.Result.StaffDevelopment,
                StaffRating = superVision.Result.StaffRating,
                StaffSupportAreas = superVision.Result.StaffSupportAreas,
                StaffId = superVision.Result.StaffId,
                OfficerToAct = superVision.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                WorkTeam = superVision.Result.Workteam.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = superVision.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                WorkteamList = superVision.Result.Workteam.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int visionId, string sender, string password, string recipient, string Smtp)
        {
            var superVision = await _StaffSupervisionAppraisalService.Get(visionId);
            var json = JsonConvert.SerializeObject(superVision);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffSupervisionAppraisal.pdf");
            string subject = "StaffSupervisionAppraisal";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int visionId)
        {
            var superVision = await _StaffSupervisionAppraisalService.Get(visionId);
            var json = JsonConvert.SerializeObject(superVision);
            byte[] byte1 = GeneratePdf(json);

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

        public async Task<IActionResult> Edit(int visionId)
        {
            var superVision =  _StaffSupervisionAppraisalService.Get(visionId);
            var staffs = await _staffService.GetStaffs();
            var putEntity = new CreateStaffSupervisionAppraisal
            {
                StaffSupervisionAppraisalId = superVision.Result.StaffSupervisionAppraisalId,
                Reference = superVision.Result.Reference,
                Attachment = superVision.Result.Attachment,
                Date = superVision.Result.Date,
                Deadline = superVision.Result.Deadline,
                URL = superVision.Result.URL,
                Remarks = superVision.Result.Remarks,
                Status = superVision.Result.Status,
                ActionRequired = superVision.Result.ActionRequired,
                NextCheckDate = superVision.Result.NextCheckDate,
                WorkTeam = superVision.Result.Workteam.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToAct = superVision.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                CondourAndWhistleBlowing = superVision.Result.CondourAndWhistleBlowing,
                Details = superVision.Result.Details,
                FiveStarRating = superVision.Result.FiveStarRating,
                NoAbilityToSupport = superVision.Result.NoAbilityToSupport,
                NoCondourAndWhistleBlowing = superVision.Result.NoCondourAndWhistleBlowing,
                ProfessionalDevelopment = superVision.Result.ProfessionalDevelopment,
                StaffAbility = superVision.Result.StaffAbility,
                StaffComplaints = superVision.Result.StaffComplaints,
                StaffDevelopment = superVision.Result.StaffDevelopment,
                StaffRating = superVision.Result.StaffRating,
                StaffSupportAreas = superVision.Result.StaffSupportAreas,
                StaffId = superVision.Result.StaffId,
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
                string extention = model.StaffId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffsupervision";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else 
            {
                model.Attachment = "No Image";
            }
            #endregion
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
                post.Workteam = model.WorkTeam.Select(s => new PostSupervisionWorkteam { StaffPersonalInfoId = s, StaffSupervisionAppraisalId =model.StaffSupervisionAppraisalId}).ToList();
                post.OfficerToAct = model.OfficerToAct.Select(s => new PostSupervisionOfficerToAct { StaffPersonalInfoId = s, StaffSupervisionAppraisalId = model.StaffSupervisionAppraisalId }).ToList();
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

            var result = await _StaffSupervisionAppraisalService.Create(post);
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
                string extention = model.StaffId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffsupervision";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
                var put = new PutStaffSupervisionAppraisal();
                put.StaffSupervisionAppraisalId = model.StaffSupervisionAppraisalId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.Workteam = model.WorkTeam.Select(s => new PutSupervisionWorkteam { StaffPersonalInfoId = s, StaffSupervisionAppraisalId = model.StaffSupervisionAppraisalId }).ToList();
                put.OfficerToAct = model.OfficerToAct.Select(s => new PutSupervisionOfficerToAct { StaffPersonalInfoId = s, StaffSupervisionAppraisalId = model.StaffSupervisionAppraisalId }).ToList();
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
            var entity = await _StaffSupervisionAppraisalService.Put(put);
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
