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

namespace AwesomeCare.Admin.Controllers
{
    public class StaffMedCompController : BaseController
    {
        private IStaffMedCompService _StaffMedCompService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffMedCompController(IStaffMedCompService StaffMedCompService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffMedCompService = StaffMedCompService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffMedCompService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffMedComp();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetClients = clientNames;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffMedCompService.GetByRef(Reference);
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
            var logAudit = await _StaffMedCompService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffMedComp.pdf");
            string subject = "StaffMedComp";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffMedCompService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

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


        public async Task<IActionResult> Edit(string Reference)
        {
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetClients = clientNames;
            List<int> officer = new List<int>();
            List<int> Ids = new List<int>();
            var MedComp = _StaffMedCompService.GetByRef(Reference);
            foreach (var item in MedComp.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.MedCompId);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffMedComp
            {
                MedCompIds = Ids,
                ClientId = MedComp.Result.FirstOrDefault().ClientId,
                Attachment = MedComp.Result.FirstOrDefault().Attachment,
                Date = MedComp.Result.FirstOrDefault().Date,
                Deadline = MedComp.Result.FirstOrDefault().Deadline,
                URL = MedComp.Result.FirstOrDefault().URL,
                OfficerToAct = officer,
                Remarks = MedComp.Result.FirstOrDefault().Remarks,
                Status = MedComp.Result.FirstOrDefault().Status,
                ActionRequired = MedComp.Result.FirstOrDefault().ActionRequired,
                NextCheckDate = MedComp.Result.FirstOrDefault().NextCheckDate,
                StaffId = MedComp.Result.FirstOrDefault().StaffId,
                RateStaff = MedComp.Result.FirstOrDefault().RateStaff,
                UnderstandingofMedication = MedComp.Result.FirstOrDefault().UnderstandingofMedication,
                UnderstandingofRights = MedComp.Result.FirstOrDefault().UnderstandingofRights,
                ReadingMedicalPrescriptions = MedComp.Result.FirstOrDefault().ReadingMedicalPrescriptions,
                Details = MedComp.Result.FirstOrDefault().Details,
                CarePlan = MedComp.Result.FirstOrDefault().CarePlan,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()

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
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetClients = clientNames;
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
            #endregion

            List<PostStaffMedComp> posts = new List<PostStaffMedComp>();

            foreach (var officer in model.OfficerToAct)
            {
                var post = new PostStaffMedComp();
                post.Reference = model.Reference;
                post.ClientId = model.ClientId;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = officer;
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
                posts.Add(post);
            }

                var result = await _StaffMedCompService.Create(posts);
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
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetClients = clientNames;
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
            List<PutStaffMedComp> puts = new List<PutStaffMedComp>();
            int count = model.MedCompIds.Count;
            int i = 0;
            foreach (var officer in model.OfficerToAct)
            {
                var put = new PutStaffMedComp();
                if (i < count)
                    put.MedCompId = model.MedCompIds[i];
                put.Reference = model.Reference;
                put.ClientId = model.ClientId;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = officer;
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
                puts.Add(put);
            }
            var entity = await _StaffMedCompService.Put(puts);
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
