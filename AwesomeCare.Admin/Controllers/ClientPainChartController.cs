using AwesomeCare.Admin.Services.ClientPainChart;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientPainChart;
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
    public class ClientPainChartController : BaseController
    {
        private IClientPainChartService _clientPainChartService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientPainChartController(IClientPainChartService clientPainChartService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientPainChartService = clientPainChartService;
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
            var entities = await _clientPainChartService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientPainChart> reports = new List<CreateClientPainChart>();
            foreach (GetClientPainChart item in entities)
            {
                var report = new CreateClientPainChart();
                report.PainChartId = item.PainChartId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientPainChart();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int painId)
        {
            var PainChart = _clientPainChartService.Get(painId);
            var putEntity = new CreateClientPainChart
            {
                PainChartId = PainChart.Result.PainChartId,
                Reference = PainChart.Result.Reference,
                ClientId = PainChart.Result.ClientId,
                Date = PainChart.Result.Date,
                Time = PainChart.Result.Time,
                Type = PainChart.Result.Type,
                Location = PainChart.Result.Location,
                PainLvl = PainChart.Result.PainLvl,
                StatusImage = PainChart.Result.StatusImage,
                StatusAttach = PainChart.Result.StatusAttach,
                Comment = PainChart.Result.Comment,
                PhysicianResponse = PainChart.Result.PhysicianResponse,
                Deadline = PainChart.Result.Deadline,
                Remarks = PainChart.Result.Remarks,
                Status = PainChart.Result.Status,
                TypeAttach = PainChart.Result.TypeAttach,
                LocationAttach = PainChart.Result.LocationAttach,
                OfficerToAct = PainChart.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = PainChart.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = PainChart.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = PainChart.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = PainChart.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = PainChart.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int painId, string sender, string password, string recipient, string Smtp)
        {
            var PainChart = await _clientPainChartService.Get(painId);
            var json = JsonConvert.SerializeObject(PainChart);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientPainChart.pdf");
            string subject = "ClientPainChart";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int painId)
        {
            var PainChart = await _clientPainChartService.Get(painId);
            var json = JsonConvert.SerializeObject(PainChart);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientPainChart.pdf");
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
        public async Task<IActionResult> Edit(int PainChartId)
        {
            var PainChart = _clientPainChartService.Get(PainChartId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientPainChart
            {
                PainChartId = PainChart.Result.PainChartId,
                Reference = PainChart.Result.Reference,
                ClientId = PainChart.Result.ClientId,
                Date = PainChart.Result.Date,
                Time = PainChart.Result.Time,
                Type = PainChart.Result.Type,
                Location = PainChart.Result.Location,
                PainLvl = PainChart.Result.PainLvl,
                StatusImage = PainChart.Result.StatusImage,
                StatusAttach = PainChart.Result.StatusAttach,
                Comment = PainChart.Result.Comment,
                StaffName = PainChart.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = PainChart.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = PainChart.Result.PhysicianResponse,
                OfficerToAct = PainChart.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = PainChart.Result.Deadline,
                Remarks = PainChart.Result.Remarks,
                Status = PainChart.Result.Status,
                TypeAttach = PainChart.Result.TypeAttach,
                LocationAttach = PainChart.Result.LocationAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientPainChart model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientPainChart postlog = new PostClientPainChart();

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

            if (model.TypeAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TypeAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TypeAttachment.OpenReadStream());
                model.TypeAttach = path;
            }
            else
            {
                model.TypeAttach = "No Image";
            }

            if (model.LocationAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_LocationAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.LocationAttachment.OpenReadStream());
                model.LocationAttach = path;
            }
            else
            {
                model.LocationAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Type = model.Type;
                postlog.Location = model.Location;
                postlog.PainLvl = model.PainLvl;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostPainChartStaffName { StaffPersonalInfoId = o, PainChartId = model.PainChartId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostPainChartPhysician { StaffPersonalInfoId = o, PainChartId = model.PainChartId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostPainChartOfficerToAct { StaffPersonalInfoId = o, PainChartId = model.PainChartId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.TypeAttach = model.TypeAttach;
                postlog.LocationAttach = model.LocationAttach;

            var result = await _clientPainChartService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Pain Chart successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientPainChart model)
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
                string filename = string.Concat(folder, "_StatusAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }

            if (model.TypeAttachment != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Type_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.TypeAttachment.OpenReadStream());
                model.TypeAttach = pathA;
            }
            else
            {
                model.TypeAttach = model.TypeAttach;
            }

            if (model.LocationAttachment != null)
            {
                string folderB = "clientcomplain";
                string filenameB = string.Concat(folderB, "_Location_", model.ClientId);
                string pathB = await _fileUpload.UploadFile(folderB, true, filenameB, model.LocationAttachment.OpenReadStream());
                model.LocationAttach = pathB;
            }
            else
            {
                model.LocationAttach = model.LocationAttach;
            }
            #endregion

            PutClientPainChart put = new PutClientPainChart();
            put.PainChartId = model.PainChartId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Type = model.Type;
            put.Location = model.Location;
            put.PainLvl = model.PainLvl;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutPainChartStaffName { StaffPersonalInfoId = o, PainChartId = model.PainChartId }).ToList();
            put.Physician = model.Physician.Select(o => new PutPainChartPhysician { StaffPersonalInfoId = o, PainChartId = model.PainChartId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutPainChartOfficerToAct { StaffPersonalInfoId = o, PainChartId = model.PainChartId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.TypeAttach = model.TypeAttach;
            put.LocationAttach = model.LocationAttach;

            var entity = await _clientPainChartService.Put(put);
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
