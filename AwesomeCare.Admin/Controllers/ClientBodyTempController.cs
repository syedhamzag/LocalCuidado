using AwesomeCare.Admin.Services.ClientBodyTemp;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp;
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
    public class ClientBodyTempController : BaseController
    {
        private IClientBodyTempService _clientBodyTempService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientBodyTempController(IClientBodyTempService clientBodyTempService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientBodyTempService = clientBodyTempService;
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
            var entities = await _clientBodyTempService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientBodyTemp> reports = new List<CreateClientBodyTemp>();
            foreach (GetClientBodyTemp item in entities)
            {
                var report = new CreateClientBodyTemp();
                report.BodyTempId = item.BodyTempId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.TargetTempAttach = item.TargetTempAttach;
                report.SeeChartAttach = item.SeeChartAttach;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientBodyTemp();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int TempId)
        {
            var BodyTemp = _clientBodyTempService.Get(TempId);
            var putEntity = new CreateClientBodyTemp
            {
                BodyTempId = BodyTemp.Result.BodyTempId,
                Reference = BodyTemp.Result.Reference,
                ClientId = BodyTemp.Result.ClientId,
                Date = BodyTemp.Result.Date,
                Time = BodyTemp.Result.Time,
                TargetTemp = BodyTemp.Result.TargetTemp,
                CurrentReading = BodyTemp.Result.CurrentReading,
                SeeChart = BodyTemp.Result.SeeChart,
                Comment = BodyTemp.Result.Comment,
                PhysicianResponse = BodyTemp.Result.PhysicianResponse,
                Deadline = BodyTemp.Result.Deadline,
                Remarks = BodyTemp.Result.Remarks,
                Status = BodyTemp.Result.Status,
                SeeChartAttach = BodyTemp.Result.SeeChartAttach,
                TargetTempAttach = BodyTemp.Result.TargetTempAttach,
                OfficerToAct = BodyTemp.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = BodyTemp.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BodyTemp.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = BodyTemp.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = BodyTemp.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = BodyTemp.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int TempId, string sender, string password, string recipient, string Smtp)
        {
            var BodyTemp = await _clientBodyTempService.Get(TempId);
            var json = JsonConvert.SerializeObject(BodyTemp);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientBodyTemp.pdf");
            string subject = "ClientBodyTemp";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int TempId)
        {
            var BodyTemp = await _clientBodyTempService.Get(TempId);
            var json = JsonConvert.SerializeObject(BodyTemp);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientBodyTemp.pdf");
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
        public async Task<IActionResult> Edit(int BodyTempId)
        {
            var BodyTemp = _clientBodyTempService.Get(BodyTempId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientBodyTemp
            {
                BodyTempId = BodyTemp.Result.BodyTempId,
                Reference = BodyTemp.Result.Reference,
                ClientId = BodyTemp.Result.ClientId,
                Date = BodyTemp.Result.Date,
                Time = BodyTemp.Result.Time,
                TargetTemp = BodyTemp.Result.TargetTemp,
                CurrentReading = BodyTemp.Result.CurrentReading,
                SeeChart = BodyTemp.Result.SeeChart,
                Comment = BodyTemp.Result.Comment,
                StaffName = BodyTemp.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BodyTemp.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = BodyTemp.Result.PhysicianResponse,
                OfficerToAct = BodyTemp.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = BodyTemp.Result.Deadline,
                Remarks = BodyTemp.Result.Remarks,
                Status = BodyTemp.Result.Status,
                SeeChartAttach = BodyTemp.Result.SeeChartAttach,
                TargetTempAttach = BodyTemp.Result.TargetTempAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientBodyTemp model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientBodyTemp postlog = new PostClientBodyTemp();

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.SeeChartAttachment.FileName);
                string folder = "clientbodytemp";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = "No Image";
            }

            if (model.TargetTempAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetTempAttachment.FileName);
                string folder = "clientbodytemp";
                string filename = string.Concat(folder, "_TargetTempAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetTempAttachment.OpenReadStream());
                model.TargetTempAttach = path;
            }
            else
            {
                model.TargetTempAttach = "No Image";
            }

            #endregion

            
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.TargetTemp = model.TargetTemp;
                postlog.CurrentReading = model.CurrentReading;
                postlog.SeeChart = model.SeeChart;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostBodyTempStaffName { StaffPersonalInfoId = o, BodyTempId = model.BodyTempId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostBodyTempPhysician { StaffPersonalInfoId = o, BodyTempId = model.BodyTempId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostBodyTempOfficerToAct { StaffPersonalInfoId = o, BodyTempId = model.BodyTempId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.SeeChartAttach = model.SeeChartAttach;
                postlog.TargetTempAttach = model.TargetTempAttach;

            var result = await _clientBodyTempService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Body Temperature successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientBodyTemp model)
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
                string folder = "clientbodytemp";
                string filename = string.Concat(folder, "_SeeChartAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = model.SeeChartAttach;
            }

            if (model.TargetTempAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TargetTempAttachment.FileName);
                string folder = "clientbodytemp";
                string filename = string.Concat(folder, "_TargetTempAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetTempAttachment.OpenReadStream());
                model.TargetTempAttach = path;
            }
            else
            {
                model.TargetTempAttach = model.TargetTempAttach;
            }

            #endregion

            PutClientBodyTemp put = new PutClientBodyTemp();
            put.BodyTempId = model.BodyTempId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.TargetTemp = model.TargetTemp;
            put.CurrentReading = model.CurrentReading;
            put.SeeChart = model.SeeChart;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutBodyTempStaffName { StaffPersonalInfoId = o, BodyTempId = model.BodyTempId }).ToList();
            put.Physician = model.Physician.Select(o => new PutBodyTempPhysician { StaffPersonalInfoId = o, BodyTempId = model.BodyTempId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutBodyTempOfficerToAct { StaffPersonalInfoId = o, BodyTempId = model.BodyTempId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.SeeChartAttach = model.SeeChartAttach;
            put.TargetTempAttach = model.TargetTempAttach;
            var entity = await _clientBodyTempService.Put(put);
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