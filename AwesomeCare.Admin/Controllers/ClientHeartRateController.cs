using AwesomeCare.Admin.Services.ClientHeartRate;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Buffers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using AwesomeCare.Admin.Services.Staff;
using System.IO;
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
    public class ClientHeartRateController : BaseController
    {
        private IClientHeartRateService _clientHeartRateService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientHeartRateController(IClientHeartRateService clientHeartRateService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientHeartRateService = clientHeartRateService;
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
            var entities = await _clientHeartRateService.Get();

            var client = await _clientService.GetClientDetail();
            var baserecord = await _baseService.GetBaseRecordsWithItems();
            List<CreateClientHeartRate> reports = new List<CreateClientHeartRate>();
            foreach (GetClientHeartRate item in entities)
            {
                var report = new CreateClientHeartRate();
                report.HeartRateId = item.HeartRateId;
                report.Reference = item.Reference;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = baserecord.Select(s => s.BaseRecordItems.FirstOrDefault(s => s.BaseRecordItemId == item.Status).ValueName).FirstOrDefault();
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientHeartRate();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int HeartId)
        {
            string OfficerToAct = "";
            var HeartRate = await _clientHeartRateService.Get(HeartId);
            var staff = _staffService.GetStaffs();
            foreach (var item in HeartRate.OfficerToAct)
            {
                OfficerToAct = OfficerToAct + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname);
            }
            var json = JsonConvert.SerializeObject(HeartRate);
            return View(HeartRate);
        }
        public async Task<IActionResult> Email(int HeartId, string sender, string password, string recipient, string Smtp)
        {
            string OfficerToAct = "";
            var HeartRate = await _clientHeartRateService.Get(HeartId);
            var staff = _staffService.GetStaffs();
            foreach (var item in HeartRate.OfficerToAct)
            {
                OfficerToAct = OfficerToAct + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname);
            }
            var json = JsonConvert.SerializeObject(HeartRate);
            var newJson = json + OfficerToAct;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientHeartRate.pdf");
            string subject = "ClientHeartRate";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int HeartId)
        {
            string OfficerToAct = "";
            var HeartRate = await _clientHeartRateService.Get(HeartId);
            var staff = _staffService.GetStaffs();
            foreach (var item in HeartRate.OfficerToAct)
            {
                OfficerToAct = OfficerToAct + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).Select(s => s.Fullname);
            }
            var json = JsonConvert.SerializeObject(HeartRate);
            var newJson = json + OfficerToAct;
            byte[] byte1 = GeneratePdf(newJson);

            return File(byte1, "application/pdf", "ClientHeartRate.pdf");
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
        public async Task<IActionResult> Edit(int HeartRateId)
        {
            var HeartRate = _clientHeartRateService.Get(HeartRateId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientHeartRate
            {
                HeartRateId = HeartRate.Result.HeartRateId,
                Reference = HeartRate.Result.Reference,
                ClientId = HeartRate.Result.ClientId,
                Date = HeartRate.Result.Date,
                Time = HeartRate.Result.Time,
                TargetHR = HeartRate.Result.TargetHR,
                Gender = HeartRate.Result.Gender,
                Age = HeartRate.Result.Age,
                BeatsPerSeconds = HeartRate.Result.BeatsPerSeconds,
                SeeChart = HeartRate.Result.SeeChart,
                Comment = HeartRate.Result.Comment,
                StaffName = HeartRate.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = HeartRate.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = HeartRate.Result.PhysicianResponse,
                OfficerToAct = HeartRate.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = HeartRate.Result.Deadline,
                Remarks = HeartRate.Result.Remarks,
                Status = HeartRate.Result.Status,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientHeartRate model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientHeartRate postlog = new PostClientHeartRate();

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_SeeChartAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = "No Image";
            }

            if (model.TargetHRAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TargetHRAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetHRAttachment.OpenReadStream());
                model.TargetHRAttach = path;
            }
            else
            {
                model.TargetHRAttach = "No Image";
            }

            if (model.GenderAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_GenderAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.GenderAttachment.OpenReadStream());
                model.GenderAttach = path;
            }
            else
            {
                model.GenderAttach = "No Image";
            }
            #endregion

            
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.TargetHR = model.TargetHR;
                postlog.Gender = model.Gender;
                postlog.Age = model.Age;
                postlog.BeatsPerSeconds = model.BeatsPerSeconds;
                postlog.SeeChart = model.SeeChart;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostHeartRateStaffName { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList(); ;
                postlog.Physician = model.Physician.Select(o => new PostHeartRatePhysician { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostHeartRateOfficerToAct { StaffPersonalInfoId = o, HeartRateId = model.HeartRateId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;

            var json = JsonConvert.SerializeObject(postlog);
            var result = await _clientHeartRateService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Heart Rate successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientHeartRate model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Status Attachment
            if (model.SeeChartAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_SeeChart_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = model.SeeChartAttach;
            }

            if (model.TargetHRAttachment != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_TargetHR_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.TargetHRAttachment.OpenReadStream());
                model.TargetHRAttach = pathA;
            }
            else
            {
                model.TargetHRAttach = model.TargetHRAttach;
            }

            if (model.GenderAttachment != null)
            {
                string folderB = "clientcomplain";
                string filenameB = string.Concat(folderB, "_Gender_", model.ClientId);
                string pathB = await _fileUpload.UploadFile(folderB, true, filenameB, model.GenderAttachment.OpenReadStream());
                model.GenderAttach = pathB;
            }
            else
            {
                model.GenderAttach = model.GenderAttach;
            }
            #endregion

            PutClientHeartRate put = new PutClientHeartRate();
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.TargetHR = model.TargetHR;
            put.Gender = model.Gender;
            put.Age = model.Age;
            put.BeatsPerSeconds = model.BeatsPerSeconds;
            put.SeeChart = model.SeeChart;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutHeartRateStaffName { StaffPersonalInfoId = o }).ToList(); ;
            put.Physician = model.Physician.Select(o => new PutHeartRatePhysician { StaffPersonalInfoId = o }).ToList(); ;
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutHeartRateOfficerToAct { StaffPersonalInfoId = o }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;

            var entity = await _clientHeartRateService.Put(put);
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
