using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffMedComp;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
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
    public class StaffMedCompController : BaseController
    {
        private IStaffMedCompService _StaffMedCompService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private IBaseRecordService _baseService;

        public StaffMedCompController(IStaffMedCompService StaffMedCompService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _StaffMedCompService = StaffMedCompService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffMedCompService.Get();
            
            var staff = await _staffService.GetStaffs();
            List<CreateStaffMedComp> reports = new List<CreateStaffMedComp>();
            foreach (GetStaffMedComp item in entities)
            {
                var report = new CreateStaffMedComp();
                report.MedCompId = item.MedCompId;
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
            var model = new CreateStaffMedComp();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.StaffId = staffId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int medCompId)
        {
            var client = await _clientService.GetClientDetail();
            var MedComp = await _StaffMedCompService.Get(medCompId);
            var putEntity = new CreateStaffMedComp
            {
                ClientId = MedComp.ClientId,
                Reference = MedComp.Reference,
                Attachment = MedComp.Attachment,
                Date = MedComp.Date,
                Deadline = MedComp.Deadline,
                URL = MedComp.URL,
                Remarks = MedComp.Remarks,
                Status = MedComp.Status,
                ActionRequired = MedComp.ActionRequired,
                NextCheckDate = MedComp.NextCheckDate,
                StaffId = MedComp.StaffId,
                RateStaff = MedComp.RateStaff,
                UnderstandingofMedication = MedComp.UnderstandingofMedication,
                UnderstandingofRights = MedComp.UnderstandingofRights,
                ReadingMedicalPrescriptions = MedComp.ReadingMedicalPrescriptions,
                Details = MedComp.Details,
                CarePlan = MedComp.CarePlan,
                OfficerToAct = MedComp.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = MedComp.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int medCompId, string sender, string password, string recipient, string Smtp)
        {
            var medComp = await _StaffMedCompService.Get(medCompId);
            var json = JsonConvert.SerializeObject(medComp);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffMedComp.pdf");
            string subject = "StaffMedComp";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int medCompId)
        {
            var medComp = await _StaffMedCompService.Get(medCompId);
            var json = JsonConvert.SerializeObject(medComp);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "StaffMedComp.pdf");
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
        public async Task<IActionResult> Edit(int medCompId)
        {
            var client = await _clientService.GetClientDetail();
            var MedComp = _StaffMedCompService.Get(medCompId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffMedComp
            {
                ClientId = MedComp.Result.ClientId,
                Reference = MedComp.Result.Reference,
                Attachment = MedComp.Result.Attachment,
                Date = MedComp.Result.Date,
                Deadline = MedComp.Result.Deadline,
                URL = MedComp.Result.URL,
                OfficerToAct = MedComp.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = MedComp.Result.Remarks,
                Status = MedComp.Result.Status,
                ActionRequired = MedComp.Result.ActionRequired,
                NextCheckDate = MedComp.Result.NextCheckDate,
                StaffId = MedComp.Result.StaffId,
                RateStaff = MedComp.Result.RateStaff,
                UnderstandingofMedication = MedComp.Result.UnderstandingofMedication,
                UnderstandingofRights = MedComp.Result.UnderstandingofRights,
                ReadingMedicalPrescriptions = MedComp.Result.ReadingMedicalPrescriptions,
                Details = MedComp.Result.Details,
                CarePlan = MedComp.Result.CarePlan,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList()

            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffMedComp model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                var client = await _clientService.GetClientDetail();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            if (model.Attach != null)
            {
                #region Attachment
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
                var post = new PostStaffMedComp();
                post.Reference = model.Reference;
                post.ClientId = model.ClientId;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = model.OfficerToAct.Select(s => new PostMedCompOfficerToAct { StaffPersonalInfoId = s, MedCompId = model.MedCompId}).ToList();
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.NextCheckDate = model.NextCheckDate;
                post.StaffId = model.StaffId;
                post.RateStaff = model.RateStaff;
                post.UnderstandingofMedication = model.UnderstandingofMedication;
                post.UnderstandingofRights = model.UnderstandingofRights;
                post.ReadingMedicalPrescriptions = model.ReadingMedicalPrescriptions;
                post.Details = model.Details;
                post.CarePlan = model.CarePlan;

                var result = await _StaffMedCompService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Med Comp Observation successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffMedComp model)
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

                var put = new PutStaffMedComp();
                put.MedCompId = model.MedCompId;
                put.Reference = model.Reference;
                put.ClientId = model.ClientId;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = model.OfficerToAct.Select(s => new PutMedCompOfficerToAct { StaffPersonalInfoId = s, MedCompId = model.MedCompId }).ToList();
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.StaffId = model.StaffId;
                put.RateStaff = model.RateStaff;
                put.UnderstandingofMedication = model.UnderstandingofMedication;
                put.UnderstandingofRights = model.UnderstandingofRights;
                put.ReadingMedicalPrescriptions = model.ReadingMedicalPrescriptions;
                put.Details = model.Details;
                put.CarePlan = model.CarePlan;

            var entity = await _StaffMedCompService.Put(put);
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
