using AwesomeCare.Admin.Services.ClientEyeHealthMonitoring;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring;
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
    public class ClientEyeHealthMonitoringController : BaseController
    {
        private IClientEyeHealthMonitoringService _clientEyeHealthMonitoringService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientEyeHealthMonitoringController(IClientEyeHealthMonitoringService clientEyeHealthMonitoringService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientEyeHealthMonitoringService = clientEyeHealthMonitoringService;
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
            var entities = await _clientEyeHealthMonitoringService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientEyeHealthMonitoring> reports = new List<CreateClientEyeHealthMonitoring>();
            foreach (GetClientEyeHealthMonitoring item in entities)
            {
                var report = new CreateClientEyeHealthMonitoring();
                report.EyeHealthId = item.EyeHealthId;
                report.Reference = item.Reference;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientEyeHealthMonitoring();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int eyeId)
        {
            var EyeHealthMonitoring = _clientEyeHealthMonitoringService.Get(eyeId);
            var putEntity = new CreateClientEyeHealthMonitoring
            {
                EyeHealthId = EyeHealthMonitoring.Result.EyeHealthId,
                Reference = EyeHealthMonitoring.Result.Reference,
                ClientId = EyeHealthMonitoring.Result.ClientId,
                Date = EyeHealthMonitoring.Result.Date,
                Time = EyeHealthMonitoring.Result.Time,
                ToolUsed = EyeHealthMonitoring.Result.ToolUsed,
                MethodUsed = EyeHealthMonitoring.Result.MethodUsed,
                TargetSet = EyeHealthMonitoring.Result.TargetSet,
                CurrentScore = EyeHealthMonitoring.Result.CurrentScore,
                PatientGlasses = EyeHealthMonitoring.Result.PatientGlasses,
                StatusImage = EyeHealthMonitoring.Result.StatusImage,
                StatusAttach = EyeHealthMonitoring.Result.StatusAttach,
                Comment = EyeHealthMonitoring.Result.Comment,
                Staff_Name = EyeHealthMonitoring.Result.StaffName.Select(s => s.StaffName).ToList(),
                PhysicianName = EyeHealthMonitoring.Result.Physician.Select(s => s.StaffName).ToList(),
                PhysicianResponse = EyeHealthMonitoring.Result.PhysicianResponse,
                OfficerName = EyeHealthMonitoring.Result.OfficerToAct.Select(s => s.StaffName).ToList(),
                Deadline = EyeHealthMonitoring.Result.Deadline,
                Remarks = EyeHealthMonitoring.Result.Remarks,
                Status = EyeHealthMonitoring.Result.Status,
                MethodUsedAttach = EyeHealthMonitoring.Result.MethodUsedAttach,
                ToolUsedAttach = EyeHealthMonitoring.Result.ToolUsedAttach
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int eyeId, string sender, string password, string recipient, string Smtp)
        {
            var EyeHealthMonitoring = await _clientEyeHealthMonitoringService.Get(eyeId);
            var json = JsonConvert.SerializeObject(EyeHealthMonitoring);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientEyeHealthMonitoring.pdf");
            string subject = "ClientEyeHealthMonitoring";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int eyeId)
        {
            var EyeHealthMonitoring = await _clientEyeHealthMonitoringService.Get(eyeId);
            var json = JsonConvert.SerializeObject(EyeHealthMonitoring);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientEyeHealthMonitoring.pdf");
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
        public async Task<IActionResult> Edit(int EyeHealthId)
        {
            var EyeHealthMonitoring = _clientEyeHealthMonitoringService.Get(EyeHealthId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientEyeHealthMonitoring
            {
                EyeHealthId = EyeHealthMonitoring.Result.EyeHealthId,
                Reference = EyeHealthMonitoring.Result.Reference,
                ClientId = EyeHealthMonitoring.Result.ClientId,
                Date = EyeHealthMonitoring.Result.Date,
                Time = EyeHealthMonitoring.Result.Time,
                ToolUsed = EyeHealthMonitoring.Result.ToolUsed,
                MethodUsed = EyeHealthMonitoring.Result.MethodUsed,
                TargetSet = EyeHealthMonitoring.Result.TargetSet,
                CurrentScore = EyeHealthMonitoring.Result.CurrentScore,
                PatientGlasses = EyeHealthMonitoring.Result.PatientGlasses,
                StatusImage = EyeHealthMonitoring.Result.StatusImage,
                StatusAttach = EyeHealthMonitoring.Result.StatusAttach,
                Comment = EyeHealthMonitoring.Result.Comment,
                StaffName = EyeHealthMonitoring.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = EyeHealthMonitoring.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = EyeHealthMonitoring.Result.PhysicianResponse,
                OfficerToAct = EyeHealthMonitoring.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = EyeHealthMonitoring.Result.Deadline,
                Remarks = EyeHealthMonitoring.Result.Remarks,
                Status = EyeHealthMonitoring.Result.Status,
                MethodUsedAttach = EyeHealthMonitoring.Result.MethodUsedAttach,
                ToolUsedAttach = EyeHealthMonitoring.Result.ToolUsedAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientEyeHealthMonitoring model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientEyeHealthMonitoring postlog = new PostClientEyeHealthMonitoring();

            #region Attachment
            if (model.StatusAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_StatusAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = "No Image";
            }

            if (model.ToolUsedAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_ToolUsedAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ToolUsedAttachment.OpenReadStream());
                model.ToolUsedAttach = path;
            }
            else
            {
                model.ToolUsedAttach = "No Image";
            }

            if (model.MethodUsedAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_MethodUsedAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.MethodUsedAttachment.OpenReadStream());
                model.MethodUsedAttach = path;
            }
            else
            {
                model.MethodUsedAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.ToolUsed = model.ToolUsed;
                postlog.MethodUsed = model.MethodUsed;
                postlog.TargetSet = model.TargetSet;
                postlog.CurrentScore = model.CurrentScore;
                postlog.PatientGlasses = model.PatientGlasses;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostEyeHealthStaffName { StaffPersonalInfoId = o, EyeHealthId = model.EyeHealthId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostEyeHealthPhysician { StaffPersonalInfoId = o, EyeHealthId = model.EyeHealthId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostEyeHealthOfficerToAct { StaffPersonalInfoId = o, EyeHealthId = model.EyeHealthId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.ToolUsedAttach = model.ToolUsedAttach;
                postlog.MethodUsedAttach = model.MethodUsedAttach;

            var result = await _clientEyeHealthMonitoringService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Eye Health Monitoring successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientEyeHealthMonitoring model)
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
            if (model.StatusAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_Status_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }

            if (model.ToolUsedAttachment != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_ToolUsed_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.ToolUsedAttachment.OpenReadStream());
                model.ToolUsedAttach = pathA;
            }
            else
            {
                model.ToolUsedAttach = model.ToolUsedAttach;
            }

            if (model.MethodUsedAttachment != null)
            {
                string folderB = "clientcomplain";
                string filenameB = string.Concat(folderB, "_MethodUsed_", model.ClientId);
                string pathB = await _fileUpload.UploadFile(folderB, true, filenameB, model.MethodUsedAttachment.OpenReadStream());
                model.MethodUsedAttach = pathB;
            }
            else
            {
                model.MethodUsedAttach = model.MethodUsedAttach;
            }
            #endregion

            PutClientEyeHealthMonitoring put = new PutClientEyeHealthMonitoring();
            put.EyeHealthId = model.EyeHealthId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.ToolUsed = model.ToolUsed;
            put.MethodUsed= model.MethodUsed;
            put.TargetSet= model.TargetSet;
            put.CurrentScore = model.CurrentScore;
            put.PatientGlasses = model.PatientGlasses;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutEyeHealthStaffName { StaffPersonalInfoId = o, EyeHealthId = model.EyeHealthId }).ToList();
            put.Physician = model.Physician.Select(o => new PutEyeHealthPhysician { StaffPersonalInfoId = o, EyeHealthId = model.EyeHealthId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutEyeHealthOfficerToAct { StaffPersonalInfoId = o, EyeHealthId = model.EyeHealthId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.MethodUsedAttach = model.MethodUsedAttach;
            put.ToolUsedAttach = model.ToolUsedAttach;

            var entity = await _clientEyeHealthMonitoringService.Put(put);
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
