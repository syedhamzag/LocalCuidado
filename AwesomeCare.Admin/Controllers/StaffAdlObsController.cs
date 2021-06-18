using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffAdlObs;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
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
    public class StaffAdlObsController : BaseController
    {
        private IStaffAdlObsService _StaffAdlObsService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffAdlObsController(IStaffAdlObsService StaffAdlObsService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffAdlObsService = StaffAdlObsService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffAdlObsService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffAdlObs();
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetClients = clientNames;
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.StaffId = staffId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffAdlObsService.GetByRef(Reference);
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
            var logAudit = await _StaffAdlObsService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffAdlObs.pdf");
            string subject = "StaffAdlObs";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var logAudit = await _StaffAdlObsService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in logAudit)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(logAudit.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "StaffAdlObs.pdf");
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
            var AdlObs = _StaffAdlObsService.GetByRef(Reference);
            foreach (var item in AdlObs.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.ObservationID);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffAdlObs
            {
                AdlObsIds = Ids,
                
                Reference = AdlObs.Result.FirstOrDefault().Reference,
                ObservationID = AdlObs.Result.FirstOrDefault().ObservationID,
                ClientId = AdlObs.Result.FirstOrDefault().ClientId,
                Attachment = AdlObs.Result.FirstOrDefault().Attachment,
                Date = AdlObs.Result.FirstOrDefault().Date,
                Deadline = AdlObs.Result.FirstOrDefault().Deadline,
                URL = AdlObs.Result.FirstOrDefault().URL,
                OfficerToAct = officer,
                Remarks = AdlObs.Result.FirstOrDefault().Remarks,
                Status = AdlObs.Result.FirstOrDefault().Status,
                ActionRequired = AdlObs.Result.FirstOrDefault().ActionRequired,
                Details = AdlObs.Result.FirstOrDefault().Details,
                UnderstandingofControl = AdlObs.Result.FirstOrDefault().UnderstandingofControl,
                UnderstandingofEquipment = AdlObs.Result.FirstOrDefault().UnderstandingofEquipment,
                StaffId = AdlObs.Result.FirstOrDefault().StaffId,
                UnderstandingofService = AdlObs.Result.FirstOrDefault().UnderstandingofService,
                NextCheckDate = AdlObs.Result.FirstOrDefault().NextCheckDate,
                FivePrinciples = AdlObs.Result.FirstOrDefault().FivePrinciples,
                Comments = AdlObs.Result.FirstOrDefault().Comments,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffAdlObs model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                List<GetClient> clientNames = await _clientService.GetClients();
                ViewBag.GetClients = clientNames;
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

            List<PostStaffAdlObs> posts = new List<PostStaffAdlObs>();

            foreach (var officer in model.OfficerToAct)
            {
                var post = new PostStaffAdlObs();
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
                post.Details = model.Details;
                post.UnderstandingofControl = model.UnderstandingofControl;
                post.UnderstandingofEquipment = model.UnderstandingofEquipment;
                post.StaffId = model.StaffId;
                post.UnderstandingofService = model.UnderstandingofService;
                post.NextCheckDate = model.NextCheckDate;
                post.FivePrinciples = model.FivePrinciples;
                post.Comments = model.Comments;
                posts.Add(post);
            }

                var result = await _StaffAdlObsService.Create(posts);
                var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Adl Observation successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffAdlObs model)
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
            List<PutStaffAdlObs> puts = new List<PutStaffAdlObs>();
            int count = model.AdlObsIds.Count;
            int i = 0;
            foreach (var officer in model.OfficerToAct)
            {
                var put = new PutStaffAdlObs();
                if (i < count)
                    put.ObservationID = model.AdlObsIds[i];
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
                put.Details = model.Details;
                put.UnderstandingofControl = model.UnderstandingofControl;
                put.UnderstandingofEquipment = model.UnderstandingofEquipment;
                put.StaffId = model.StaffId;
                put.UnderstandingofService = model.UnderstandingofService;
                put.NextCheckDate = model.NextCheckDate;
                put.FivePrinciples = model.FivePrinciples;
                put.Comments = model.Comments;
                puts.Add(put);
                i++;
            }
            var json = JsonConvert.SerializeObject(puts);
            var entity = await _StaffAdlObsService.Put(puts);
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
