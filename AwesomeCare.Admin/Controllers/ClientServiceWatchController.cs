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
using AwesomeCare.DataTransferObject.DTOs.ClientService;

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
            var client = await _clientService.GetClientDetail();
            List<CreateClientServiceWatch> reports = new List<CreateClientServiceWatch>();
            foreach (GetClientServiceWatch item in entities)
            {
                var report = new CreateClientServiceWatch();
                report.WatchId = item.WatchId;
                report.Date = item.Date;
                report.NextCheckDate = item.NextCheckDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientServiceWatch();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(int serviceId)
        {
            var ServiceWatch = await _clientServiceWatchService.Get(serviceId);
            var putEntity = new CreateClientServiceWatch
            {
                WatchId = ServiceWatch.WatchId,
                ClientId = ServiceWatch.ClientId,
                Reference = ServiceWatch.Reference,
                Attachment = ServiceWatch.Attachment,
                Date = ServiceWatch.Date,
                NextCheckDate = ServiceWatch.NextCheckDate,
                Deadline = ServiceWatch.Deadline,
                Contact = ServiceWatch.Contact,
                Details = ServiceWatch.Details,
                URL = ServiceWatch.URL,
                Incident = ServiceWatch.Incident,
                Remarks = ServiceWatch.Remarks,
                Observation = ServiceWatch.Observation,
                Status = ServiceWatch.Status,
                ActionRequired = ServiceWatch.ActionRequired,
                PersonInvolved = ServiceWatch.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToAct = ServiceWatch.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = ServiceWatch.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PersonList = ServiceWatch.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int serviceId, string sender, string password, string recipient, string Smtp)
        {
            var ServiceWatch = await _clientServiceWatchService.Get(serviceId);
            var json = JsonConvert.SerializeObject(ServiceWatch);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientServiceWatch.pdf");
            string subject = "ClientServiceWatch";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int serviceId)
        {
            var ServiceWatch = await _clientServiceWatchService.Get(serviceId);
            var json = JsonConvert.SerializeObject(ServiceWatch);
            byte[] byte1 = GeneratePdf(json);

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

        public async Task<IActionResult> Edit(int WatchId)
        {
            var ServiceWatch = _clientServiceWatchService.Get(WatchId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientServiceWatch
            {
                WatchId = ServiceWatch.Result.WatchId,
                ClientId = ServiceWatch.Result.ClientId,
                Reference = ServiceWatch.Result.Reference,
                Attachment = ServiceWatch.Result.Attachment,
                Date = ServiceWatch.Result.Date,
                NextCheckDate = ServiceWatch.Result.NextCheckDate,
                Deadline = ServiceWatch.Result.Deadline,
                Contact = ServiceWatch.Result.Contact,
                Details = ServiceWatch.Result.Details,
                URL = ServiceWatch.Result.URL,
                Incident = ServiceWatch.Result.Incident,
                OfficerToAct = ServiceWatch.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = ServiceWatch.Result.Remarks,
                Observation = ServiceWatch.Result.Observation,
                Status = ServiceWatch.Result.Status,
                ActionRequired = ServiceWatch.Result.ActionRequired,
                PersonInvolved = ServiceWatch.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
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
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientServiceWatch postlog = new PostClientServiceWatch();

            #region Attachment
            if (model.Attach != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Attach_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

            
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Attachment = model.Attachment;
                postlog.Date = model.Date;
                postlog.NextCheckDate = model.NextCheckDate;
                postlog.Deadline = model.Deadline;
                postlog.Contact = model.Contact;
                postlog.Details = model.Details;
                postlog.URL = model.URL;
                postlog.Incident = model.Incident;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostServiceOfficerToAct { StaffPersonalInfoId = o, ServiceId = model.WatchId }).ToList();
                postlog.Remarks = model.Remarks;
                postlog.Observation = model.Observation;
                postlog.Status = model.Status;
                postlog.ActionRequired = model.ActionRequired;
                postlog.StaffName = model.PersonInvolved.Select(o => new PostServiceStaffName { StaffPersonalInfoId = o, ServiceId = model.WatchId }).ToList();

            var result = await _clientServiceWatchService.Create(postlog);
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

                PutClientServiceWatch put = new PutClientServiceWatch();
                put.WatchId = model.WatchId;
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
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutServiceOfficerToAct { StaffPersonalInfoId = o, ServiceId = model.WatchId }).ToList();
                put.Remarks = model.Remarks;
                put.Observation = model.Observation;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.StaffName = model.PersonInvolved.Select(o => new PutServiceStaffName { StaffPersonalInfoId = o, ServiceId = model.WatchId }).ToList();
            var json = JsonConvert.SerializeObject(put);
            var entity = await _clientServiceWatchService.Put(put);
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
