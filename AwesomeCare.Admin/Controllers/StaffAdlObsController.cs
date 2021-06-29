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
using AwesomeCare.Admin.Services.Admin;

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
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.StaffId = staffId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            return View(model);
        }
        public async Task<IActionResult> View(int obsId)
        {
            var AdlObs = _StaffAdlObsService.Get(obsId);
            var putEntity = new CreateStaffAdlObs
            {
                ObservationID = AdlObs.Result.ObservationID,
                Reference = AdlObs.Result.Reference,
                ClientId = AdlObs.Result.ClientId,
                Attachment = AdlObs.Result.Attachment,
                Date = AdlObs.Result.Date,
                Deadline = AdlObs.Result.Deadline,
                URL = AdlObs.Result.URL,
                Remarks = AdlObs.Result.Remarks,
                Status = AdlObs.Result.Status,
                ActionRequired = AdlObs.Result.ActionRequired,
                Details = AdlObs.Result.Details,
                UnderstandingofControl = AdlObs.Result.UnderstandingofControl,
                UnderstandingofEquipment = AdlObs.Result.UnderstandingofEquipment,
                StaffId = AdlObs.Result.StaffId,
                UnderstandingofService = AdlObs.Result.UnderstandingofService,
                NextCheckDate = AdlObs.Result.NextCheckDate,
                FivePrinciples = AdlObs.Result.FivePrinciples,
                Comments = AdlObs.Result.Comments,
                OfficerName = AdlObs.Result.OfficerToAct.Select(s => s.StaffName).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int obsId, string sender, string password, string recipient, string Smtp)
        {
            var AdlObs = await _StaffAdlObsService.Get(obsId);
            var json = JsonConvert.SerializeObject(AdlObs);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffAdlObs.pdf");
            string subject = "StaffAdlObs";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int obsId)
        {
            var Adlobs = await _StaffAdlObsService.Get(obsId);
            var staff = _staffService.GetStaffs();
            var json = JsonConvert.SerializeObject(Adlobs);
            byte[] byte1 = GeneratePdf(json);
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
        public async Task<IActionResult> Edit(int obsId)
        {
            var AdlObs = _StaffAdlObsService.Get(obsId);
            var client = await _clientService.GetClientDetail();
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateStaffAdlObs
            {
                ObservationID = AdlObs.Result.ObservationID,
                Reference = AdlObs.Result.Reference,
                ClientId = AdlObs.Result.ClientId,
                Attachment = AdlObs.Result.Attachment,
                Date = AdlObs.Result.Date,
                Deadline = AdlObs.Result.Deadline,
                URL = AdlObs.Result.URL,
                OfficerToAct = AdlObs.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = AdlObs.Result.Remarks,
                Status = AdlObs.Result.Status,
                ActionRequired = AdlObs.Result.ActionRequired,
                Details = AdlObs.Result.Details,
                UnderstandingofControl = AdlObs.Result.UnderstandingofControl,
                UnderstandingofEquipment = AdlObs.Result.UnderstandingofEquipment,
                StaffId = AdlObs.Result.StaffId,
                UnderstandingofService = AdlObs.Result.UnderstandingofService,
                NextCheckDate = AdlObs.Result.NextCheckDate,
                FivePrinciples = AdlObs.Result.FivePrinciples,
                Comments = AdlObs.Result.Comments,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffAdlObs model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            PostStaffAdlObs postlog = new PostStaffAdlObs();

            #region Attachment
            if (model.Attach != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TargetINR_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

            var post = new PostStaffAdlObs();
                post.Reference = model.Reference;
                post.ClientId = model.ClientId;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostAdlObsOfficerToAct { StaffPersonalInfoId = o, ObservationId = model.ObservationID }).ToList();
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


            var result = await _StaffAdlObsService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New AdlObs successfully registered" : "An Error Occurred" });
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

            var put = new PutStaffAdlObs();
                put.ObservationID = model.ObservationID;
                put.Reference = model.Reference;
                put.ClientId = model.ClientId;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutAdlObsOfficerToAct { StaffPersonalInfoId = o, ObservationId = model.ObservationID }).ToList();
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

            var entity = await _StaffAdlObsService.Put(put);
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
