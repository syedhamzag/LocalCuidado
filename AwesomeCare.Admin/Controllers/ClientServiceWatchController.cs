using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientServiceWatch;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientServiceWatch;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
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
    public class ClientServiceWatchController : BaseController
    {
        private IClientServiceWatchService _clientServiceWatchService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private IBaseRecordService _baseService;

        public ClientServiceWatchController(IClientServiceWatchService clientServiceWatchService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientServiceWatchService = clientServiceWatchService;
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
            var entities = await _clientServiceWatchService.Get();
            var client = await _clientService.GetClients();
            var baserecord = await _baseService.GetBaseRecordsWithItems();
            List<CreateClientServiceWatch> reports = new List<CreateClientServiceWatch>();
            foreach (GetClientServiceWatch item in entities)
            {
                var report = new CreateClientServiceWatch();
                report.WatchId = item.WatchId;
                report.Reference = item.Reference;
                report.NextCheckDate = item.NextCheckDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.Firstname).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientServiceWatch();
            var client = await _clientService.GetClient(clientId.Value);
            model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;          
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var ServiceWatch = await _clientServiceWatchService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in ServiceWatch)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(ServiceWatch.FirstOrDefault());
            var newJson = json + staffName;
            return View(ServiceWatch.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference, string sender, string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var ServiceWatch = await _clientServiceWatchService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in ServiceWatch)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(ServiceWatch.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientServiceWatch.pdf");
            string subject = "ClientServiceWatch";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var ServiceWatch = await _clientServiceWatchService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in ServiceWatch)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(ServiceWatch.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "ClientServiceWatch.pdf");
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
            var ServiceWatch = _clientServiceWatchService.GetByRef(Reference);
            foreach (var item in ServiceWatch.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.OfficerToAct);
            }
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientServiceWatch
            {
                WatchIds = Ids,
                ClientId = ServiceWatch.Result.FirstOrDefault().ClientId,
                Reference = ServiceWatch.Result.FirstOrDefault().Reference,
                Attachment = ServiceWatch.Result.FirstOrDefault().Attachment,
                Date = ServiceWatch.Result.FirstOrDefault().Date,
                NextCheckDate = ServiceWatch.Result.FirstOrDefault().NextCheckDate,
                Deadline = ServiceWatch.Result.FirstOrDefault().Deadline,
                Contact = ServiceWatch.Result.FirstOrDefault().Contact,
                Details = ServiceWatch.Result.FirstOrDefault().Details,
                URL = ServiceWatch.Result.FirstOrDefault().URL,
                Incident = ServiceWatch.Result.FirstOrDefault().Incident,
                OfficerToAct = officer,
                Remarks = ServiceWatch.Result.FirstOrDefault().Remarks,
                Observation = ServiceWatch.Result.FirstOrDefault().Observation,
                Status = ServiceWatch.Result.FirstOrDefault().Status,
                ActionRequired = ServiceWatch.Result.FirstOrDefault().ActionRequired,
                PersonInvolved = ServiceWatch.Result.FirstOrDefault().PersonInvolved,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientServiceWatch model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
            }
            #endregion

            List<PostClientServiceWatch> posts = new List<PostClientServiceWatch>();
            foreach (var item in model.OfficerToAct)
            {
                var post = new PostClientServiceWatch();
                post.ClientId = model.ClientId;
                post.Reference = model.Reference;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.NextCheckDate = model.NextCheckDate;
                post.Deadline = model.Deadline;
                post.Contact = model.Contact;
                post.Details = model.Details;
                post.URL = model.URL;
                post.Incident = model.Incident;
                post.OfficerToAct = item;
                post.Remarks = model.Remarks;
                post.Observation = model.Observation;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.PersonInvolved = model.PersonInvolved;
                posts.Add(post);
            }

            var result = await _clientServiceWatchService.Create(posts);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result != null ? "New Service Watch successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientServiceWatch model)
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

            int count = model.WatchIds.Count;
            int i = 0;
            List<PutClientServiceWatch> puts = new List<PutClientServiceWatch>();
            foreach (var item in model.OfficerToAct)
            {
                var put = new PutClientServiceWatch();
                if (i < count)
                    put.WatchId = model.WatchIds[i];
                put.ClientId = model.ClientId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.NextCheckDate = model.NextCheckDate;
                put.Deadline = model.Deadline;
                put.Contact = model.Contact;
                put.Details = model.Details;
                put.URL = model.URL;
                put.Incident = model.Incident;
                put.OfficerToAct = item;
                put.Remarks = model.Remarks;
                put.Observation = model.Observation;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.PersonInvolved = model.PersonInvolved;
                puts.Add(put);
            }

            var entity = await _clientServiceWatchService.Put(puts);
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
