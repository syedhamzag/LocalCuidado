using AwesomeCare.Admin.Services.ClientBMIChart;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientBMIChart;
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
    public class ClientBMIChartController : BaseController
    {
        private IClientBMIChartService _clientBMIChartService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;

        public ClientBMIChartController(IClientBMIChartService clientBMIChartService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientBMIChartService = clientBMIChartService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _baseService = baseService;

        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientBMIChartService.Get();
            
            var client = await _clientService.GetClientDetail();
            List<CreateClientBMIChart> reports = new List<CreateClientBMIChart>();         
            foreach (GetClientBMIChart item in entities)
            {
                var report = new CreateClientBMIChart();
                report.BMIChartId = item.BMIChartId;
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
            var model = new CreateClientBMIChart();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int BMIId)
        {
            var BMIChart = _clientBMIChartService.Get(BMIId);
            var putEntity = new CreateClientBMIChart
            {
                BMIChartId = BMIChart.Result.BMIChartId,
                Reference = BMIChart.Result.Reference,
                ClientId = BMIChart.Result.ClientId,
                Date = BMIChart.Result.Date,
                Time = BMIChart.Result.Time,
                Height = BMIChart.Result.Height,
                Weight = BMIChart.Result.Weight,
                NumberRange = BMIChart.Result.NumberRange,
                SeeChart = BMIChart.Result.SeeChart,
                Comment = BMIChart.Result.Comment,
                PhysicianResponse = BMIChart.Result.PhysicianResponse,
                Deadline = BMIChart.Result.Deadline,
                Remarks = BMIChart.Result.Remarks,
                Status = BMIChart.Result.Status,
                SeeChartAttach = BMIChart.Result.SeeChartAttach,
                OfficerToAct = BMIChart.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = BMIChart.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BMIChart.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = BMIChart.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = BMIChart.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = BMIChart.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int BMIId, string sender, string password, string recipient, string Smtp)
        {
            var BMIChart = await _clientBMIChartService.Get(BMIId);
            var json = JsonConvert.SerializeObject(BMIChart);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientBMIChart.pdf");
            string subject = "ClientBMIChart";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int BMIId)
        {
            var BMIChart = await _clientBMIChartService.Get(BMIId);
            var json = JsonConvert.SerializeObject(BMIChart);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientBMIChart.pdf");
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
        public async Task<IActionResult> Edit(int BMIChartId)
        {
            var BMIChart = _clientBMIChartService.Get(BMIChartId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientBMIChart
            {
                BMIChartId = BMIChart.Result.BMIChartId,
                Reference = BMIChart.Result.Reference,
                ClientId = BMIChart.Result.ClientId,
                Date = BMIChart.Result.Date,
                Time = BMIChart.Result.Time,
                Height = BMIChart.Result.Height,
                Weight = BMIChart.Result.Weight,
                NumberRange = BMIChart.Result.NumberRange,
                SeeChart = BMIChart.Result.SeeChart,
                Comment = BMIChart.Result.Comment,
                StaffName = BMIChart.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BMIChart.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = BMIChart.Result.PhysicianResponse,
                OfficerToAct = BMIChart.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = BMIChart.Result.Deadline,
                Remarks = BMIChart.Result.Remarks,
                Status = BMIChart.Result.Status,
                SeeChartAttach = BMIChart.Result.SeeChartAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientBMIChart model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientBMIChart postlog = new PostClientBMIChart();

            #region Attachment
            if (model.SeeChartAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_SeeChart_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = "No Image";
            }
            #endregion
            
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Height = model.Height;
                postlog.Weight = model.Weight;
                postlog.NumberRange = model.NumberRange;
                postlog.SeeChart = model.SeeChart;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostBMIChartStaffName { StaffPersonalInfoId = o, BMIChartId = model.BMIChartId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostBMIChartPhysician { StaffPersonalInfoId = o, BMIChartId = model.BMIChartId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostBMIChartOfficerToAct { StaffPersonalInfoId = o, BMIChartId = model.BMIChartId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.SeeChartAttach = model.SeeChartAttach;
            var json = JsonConvert.SerializeObject(postlog);
            var result = await _clientBMIChartService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New BMI Chart successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientBMIChart model)
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
                string filename = string.Concat(folder, "_SeeChartAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeeChartAttachment.OpenReadStream());
                model.SeeChartAttach = path;
            }
            else
            {
                model.SeeChartAttach = model.SeeChartAttach;
            }

            #endregion

            PutClientBMIChart put = new PutClientBMIChart();
            put.BMIChartId = model.BMIChartId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Height = model.Height;
            put.Weight = model.Weight;
            put.SeeChart = model.SeeChart;
            put.NumberRange= model.NumberRange;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutBMIChartStaffName { StaffPersonalInfoId = o, BMIChartId = model.BMIChartId }).ToList();
            put.Physician = model.Physician.Select(o => new PutBMIChartPhysician { StaffPersonalInfoId = o, BMIChartId = model.BMIChartId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutBMIChartOfficerToAct { StaffPersonalInfoId = o, BMIChartId = model.BMIChartId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.SeeChartAttach = model.SeeChartAttach;

            var entity = await _clientBMIChartService.Put(put);
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
