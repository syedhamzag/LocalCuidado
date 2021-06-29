using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffSpotCheck;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
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
    public class StaffSpotCheckController : BaseController
    {
        private IStaffSpotCheckService _StaffSpotCheckService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffSpotCheckController(IStaffSpotCheckService StaffSpotCheckService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _StaffSpotCheckService = StaffSpotCheckService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffSpotCheckService.Get();
            return View(entities);
        }
        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffSpotCheck();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            model.StaffId = staffId.Value;
            return View(model);
        }
        public async Task<IActionResult> View(int spotCheckId)
        {
            var SpotCheck = _StaffSpotCheckService.Get(spotCheckId);
            var putEntity = new CreateStaffSpotCheck
            {
                SpotCheckId = SpotCheck.Result.SpotCheckId,
                Reference = SpotCheck.Result.Reference,
                ClientId = SpotCheck.Result.ClientId,
                Attachment = SpotCheck.Result.Attachment,
                Date = SpotCheck.Result.Date,
                Deadline = SpotCheck.Result.Deadline,
                URL = SpotCheck.Result.URL,
                OfficerName = SpotCheck.Result.OfficerToAct.Select(s => s.StaffName).ToList(),
                Remarks = SpotCheck.Result.Remarks,
                Status = SpotCheck.Result.Status,
                ActionRequired = SpotCheck.Result.ActionRequired,
                NextCheckDate = SpotCheck.Result.NextCheckDate,
                AreaComments = SpotCheck.Result.AreaComments,
                StaffDressCode = SpotCheck.Result.StaffDressCode,
                StaffArriveOnTime = SpotCheck.Result.StaffArriveOnTime,
                StaffId = SpotCheck.Result.StaffId,
                Details = SpotCheck.Result.Details
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int spotCheckId, string sender, string password, string recipient, string Smtp)
        {
            var spotCheck = await _StaffSpotCheckService.Get(spotCheckId);

            var json = JsonConvert.SerializeObject(spotCheck);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffSpotCheck.pdf");
            string subject = "StaffSpotCheck";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int spotCheckId)
        {
            var spotCheck = await _StaffSpotCheckService.Get(spotCheckId);

            var json = JsonConvert.SerializeObject(spotCheck);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "StaffSpotCheck.pdf");
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

        public async Task<IActionResult> Edit(int spotCheckId)
        {
            List<GetClient> clientNames = await _clientService.GetClients();
            ViewBag.GetClients = clientNames;


            var SpotCheck = _StaffSpotCheckService.Get(spotCheckId);
            var staffs = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateStaffSpotCheck
            {
                SpotCheckId = SpotCheck.Result.SpotCheckId,
                Reference = SpotCheck.Result.Reference,
                ClientId = SpotCheck.Result.ClientId,
                Attachment = SpotCheck.Result.Attachment,
                Date = SpotCheck.Result.Date,
                Deadline = SpotCheck.Result.Deadline,
                URL = SpotCheck.Result.URL,
                OfficerToAct = SpotCheck.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = SpotCheck.Result.Remarks,
                Status = SpotCheck.Result.Status,
                ActionRequired = SpotCheck.Result.ActionRequired,
                NextCheckDate = SpotCheck.Result.NextCheckDate,
                AreaComments = SpotCheck.Result.AreaComments,
                StaffDressCode = SpotCheck.Result.StaffDressCode,
                StaffArriveOnTime = SpotCheck.Result.StaffArriveOnTime,
                StaffId = SpotCheck.Result.StaffId,
                Details = SpotCheck.Result.Details,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffSpotCheck model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                var client = await _clientService.GetClientDetail();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
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
                var post = new PostStaffSpotCheck();
                post.SpotCheckId = model.SpotCheckId;
                post.Reference = model.Reference;
                post.ClientId = model.ClientId;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.Deadline = model.Deadline;
                post.URL = model.URL;
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostSpotCheckOfficerToAct { StaffPersonalInfoId = o, SpotCheckId = model.SpotCheckId }).ToList();
                post.Remarks = model.Remarks;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.NextCheckDate = model.NextCheckDate;
                post.AreaComments = model.AreaComments;
                post.StaffDressCode = model.StaffDressCode;
                post.StaffArriveOnTime = model.StaffArriveOnTime;
                post.StaffId = model.StaffId;
                post.Details = model.Details;

            var result = await _StaffSpotCheckService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Spot Check successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffSpotCheck model)
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
                var put = new PutStaffSpotCheck();
                put.SpotCheckId = model.SpotCheckId;
                put.Reference = model.Reference;
                put.ClientId = model.ClientId;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.Deadline = model.Deadline;
                put.URL = model.URL;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutSpotCheckOfficerToAct { StaffPersonalInfoId = o, SpotCheckId = model.SpotCheckId }).ToList();
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.NextCheckDate = model.NextCheckDate;
                put.AreaComments = model.AreaComments;
                put.StaffDressCode = model.StaffDressCode;
                put.StaffArriveOnTime = model.StaffArriveOnTime;
                put.StaffId = model.StaffId;
                put.Details = model.Details;

            var entity = await _StaffSpotCheckService.Put(put);
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
