using AwesomeCare.Admin.Services.ClientOxygenLvl;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl;
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
    public class ClientOxygenLvlController : BaseController
    {
        private IClientOxygenLvlService _clientOxygenLvlService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientOxygenLvlController(IClientOxygenLvlService clientOxygenLvlService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientOxygenLvlService = clientOxygenLvlService;
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
            var entities = await _clientOxygenLvlService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientOxygenLvl> reports = new List<CreateClientOxygenLvl>();
            foreach (GetClientOxygenLvl item in entities)
            {
                var report = new CreateClientOxygenLvl();
                report.OxygenLvlId = item.OxygenLvlId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.TargetOxygenAttach = item.TargetOxygenAttach;
                report.SeeChartAttach = item.SeeChartAttach;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientOxygenLvl();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int oxygenId)
        {
            var OxygenLvl = _clientOxygenLvlService.Get(oxygenId);
            var putEntity = new CreateClientOxygenLvl
            {
                OxygenLvlId = OxygenLvl.Result.OxygenLvlId,
                Reference = OxygenLvl.Result.Reference,
                ClientId = OxygenLvl.Result.ClientId,
                Date = OxygenLvl.Result.Date,
                Time = OxygenLvl.Result.Time,
                TargetOxygen = OxygenLvl.Result.TargetOxygen,
                CurrentReading = OxygenLvl.Result.CurrentReading,
                SeeChart = OxygenLvl.Result.SeeChart,
                Comment = OxygenLvl.Result.Comment,
                PhysicianResponse = OxygenLvl.Result.PhysicianResponse,
                Deadline = OxygenLvl.Result.Deadline,
                Remarks = OxygenLvl.Result.Remarks,
                Status = OxygenLvl.Result.Status,
                SeeChartAttach = OxygenLvl.Result.SeeChartAttach,
                TargetOxygenAttach = OxygenLvl.Result.TargetOxygenAttach,
                OfficerToAct = OxygenLvl.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = OxygenLvl.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = OxygenLvl.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = OxygenLvl.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = OxygenLvl.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = OxygenLvl.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int oxygenId, string sender, string password, string recipient, string Smtp)
        {
            var OxygenLvl = await _clientOxygenLvlService.Get(oxygenId);
            var json = JsonConvert.SerializeObject(OxygenLvl);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientOxygenLvl.pdf");
            string subject = "ClientOxygenLvl";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int oxygenId)
        {
            var OxygenLvl = await _clientOxygenLvlService.Get(oxygenId);
            var json = JsonConvert.SerializeObject(OxygenLvl);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientOxygenLvl.pdf");
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
        public async Task<IActionResult> Edit(int OxygenLvlId)
        {
            var OxygenLvl = _clientOxygenLvlService.Get(OxygenLvlId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientOxygenLvl
            {
                OxygenLvlId = OxygenLvl.Result.OxygenLvlId,
                Reference = OxygenLvl.Result.Reference,
                ClientId = OxygenLvl.Result.ClientId,
                Date = OxygenLvl.Result.Date,
                Time = OxygenLvl.Result.Time,
                TargetOxygen = OxygenLvl.Result.TargetOxygen,
                CurrentReading = OxygenLvl.Result.CurrentReading,
                SeeChart = OxygenLvl.Result.SeeChart,
                Comment = OxygenLvl.Result.Comment,
                StaffName = OxygenLvl.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = OxygenLvl.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = OxygenLvl.Result.PhysicianResponse,
                OfficerToAct = OxygenLvl.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = OxygenLvl.Result.Deadline,
                Remarks = OxygenLvl.Result.Remarks,
                Status = OxygenLvl.Result.Status,
                SeeChartAttach = OxygenLvl.Result.SeeChartAttach,
                TargetOxygenAttach = OxygenLvl.Result.TargetOxygenAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientOxygenLvl model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientOxygenLvl postlog = new PostClientOxygenLvl();

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientoxygenlevel";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = "No Image";
            }

            if (model.TargetOxygenAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetOxygenAttachment.FileName);
                string folder = "clientoxygenlevel";
                string filename = string.Concat(folder, "_TargetOxygenAttachment_",extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetOxygenAttachment.OpenReadStream());
                model.TargetOxygenAttach = path;
            }
            else
            {
                model.TargetOxygenAttach = "No Image";
            }

            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.TargetOxygen = model.TargetOxygen;
                postlog.CurrentReading = model.CurrentReading;
                postlog.SeeChart= model.SeeChart;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostOxygenLvlStaffName { StaffPersonalInfoId = o, OxygenLvlId = model.OxygenLvlId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostOxygenLvlPhysician { StaffPersonalInfoId = o, OxygenLvlId = model.OxygenLvlId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostOxygenLvlOfficerToAct { StaffPersonalInfoId = o, OxygenLvlId = model.OxygenLvlId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.SeeChartAttach = model.SeeChartAttach;
                postlog.TargetOxygenAttach = model.TargetOxygenAttach;

            var result = await _clientOxygenLvlService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Oxygen Level successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientOxygenLvl model)
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
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientoxygenlevel";
                string filename = string.Concat(folder, "_SeeChartAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = model.SeeChartAttach;
            }

            if (model.TargetOxygenAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetOxygenAttachment.FileName);
                string folder = "clientoxygenlevel";
                string filename = string.Concat(folder, "_TargetOxygen_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetOxygenAttachment.OpenReadStream());
                model.TargetOxygenAttach = path;
            }
            else
            {
                model.TargetOxygenAttach = model.TargetOxygenAttach;
            }

            #endregion

            PutClientOxygenLvl put = new PutClientOxygenLvl();
            put.OxygenLvlId = model.OxygenLvlId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.TargetOxygen = model.TargetOxygen;
            put.CurrentReading = model.CurrentReading;
            put.SeeChart = model.SeeChart;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutOxygenLvlStaffName { StaffPersonalInfoId = o, OxygenLvlId = model.OxygenLvlId }).ToList();
            put.Physician = model.Physician.Select(o => new PutOxygenLvlPhysician { StaffPersonalInfoId = o, OxygenLvlId = model.OxygenLvlId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutOxygenLvlOfficerToAct { StaffPersonalInfoId = o, OxygenLvlId = model.OxygenLvlId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.SeeChartAttach = model.SeeChartAttach;
            put.TargetOxygenAttach = model.TargetOxygenAttach;

            var entity = await _clientOxygenLvlService.Put(put);
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
