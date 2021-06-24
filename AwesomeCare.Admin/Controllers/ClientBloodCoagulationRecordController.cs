using AwesomeCare.Admin.Services.ClientBloodCoagulationRecord;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord;
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
    public class ClientBloodCoagulationRecordController : BaseController
    {
        private IClientBloodCoagulationRecordService _clientBloodCoagulationRecordService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientBloodCoagulationRecordController(IClientBloodCoagulationRecordService clientBloodCoagulationRecordService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientBloodCoagulationRecordService = clientBloodCoagulationRecordService;
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
            var entities = await _clientBloodCoagulationRecordService.Get();
            
            var client = await _clientService.GetClientDetail();
            List<CreateClientBloodCoagulationRecord> reports = new List<CreateClientBloodCoagulationRecord>();
            foreach (GetClientBloodCoagulationRecord item in entities)
            {
                var report = new CreateClientBloodCoagulationRecord();
                report.BloodRecordId = item.BloodRecordId;
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
            var model = new CreateClientBloodCoagulationRecord();           
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s=>s.ClientId==clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int bloodId)
        {
            var BloodCoagulationRecord = _clientBloodCoagulationRecordService.Get(bloodId);
            var putEntity = new CreateClientBloodCoagulationRecord
            {
                BloodRecordId = BloodCoagulationRecord.Result.BloodRecordId,
                Reference = BloodCoagulationRecord.Result.Reference,
                ClientId = BloodCoagulationRecord.Result.ClientId,
                Date = BloodCoagulationRecord.Result.Date,
                Time = BloodCoagulationRecord.Result.Time,
                Indication = BloodCoagulationRecord.Result.Indication,
                TargetINR = BloodCoagulationRecord.Result.TargetINR,
                StartDate = BloodCoagulationRecord.Result.StartDate,
                CurrentDose = BloodCoagulationRecord.Result.CurrentDose,
                INR = BloodCoagulationRecord.Result.INR,
                NewDose = BloodCoagulationRecord.Result.NewDose,
                NewINR = BloodCoagulationRecord.Result.NewINR,
                BloodStatus = BloodCoagulationRecord.Result.BloodStatus,
                Comment = BloodCoagulationRecord.Result.Comment,
                Staff_Name = BloodCoagulationRecord.Result.StaffName.Select(s => s.StaffName).ToList(),
                PhysicianName = BloodCoagulationRecord.Result.Physician.Select(s => s.StaffName).ToList(),
                PhysicianResponce = BloodCoagulationRecord.Result.PhysicianResponce,
                OfficerToActName = BloodCoagulationRecord.Result.OfficerToAct.Select(s => s.StaffName).ToList(),
                Deadline = BloodCoagulationRecord.Result.Deadline,
                Remark = BloodCoagulationRecord.Result.Remark,
                Status = BloodCoagulationRecord.Result.Status,
                TargetINRAttach = BloodCoagulationRecord.Result.TargetINRAttach,
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int bloodId,string sender,string password, string recipient, string Smtp)
        {
            var BloodCoagulationRecord = await _clientBloodCoagulationRecordService.Get(bloodId);
            var json = JsonConvert.SerializeObject(BloodCoagulationRecord);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientBloodCoagulationRecord.pdf");
            string subject = "ClientBloodCoagulationRecord";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int bloodId)
        {
            var BloodCoagulationRecord = await _clientBloodCoagulationRecordService.Get(bloodId);
            var json = JsonConvert.SerializeObject(BloodCoagulationRecord);
            byte[] byte1 = GeneratePdf(json);
    
            return File(byte1, "application/pdf","ClientBloodCoagulationRecord.pdf");
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
                        var para = new Paragraph(paragraphs.Replace("{","").Replace("}","").Replace("\"","").Replace(",","\n"));
                        document.Add(para);
                        buffer = memStream.ToArray();
                        document.Close();
                    }
                }
                buffer = memStream.ToArray();
            }
            return buffer;
        }
        public async Task<IActionResult> Edit(int BloodRecordId)
        {
            var BloodCoagulationRecord = _clientBloodCoagulationRecordService.Get(BloodRecordId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientBloodCoagulationRecord
            {
                BloodRecordId = BloodCoagulationRecord.Result.BloodRecordId,
                Reference = BloodCoagulationRecord.Result.Reference,
                ClientId = BloodCoagulationRecord.Result.ClientId,
                Date = BloodCoagulationRecord.Result.Date,
                Time = BloodCoagulationRecord.Result.Time,
                Indication = BloodCoagulationRecord.Result.Indication,
                TargetINR = BloodCoagulationRecord.Result.TargetINR,
                StartDate = BloodCoagulationRecord.Result.StartDate,
                CurrentDose = BloodCoagulationRecord.Result.CurrentDose,
                INR = BloodCoagulationRecord.Result.INR,
                NewDose = BloodCoagulationRecord.Result.NewDose,
                NewINR = BloodCoagulationRecord.Result.NewINR,
                BloodStatus = BloodCoagulationRecord.Result.BloodStatus,
                Comment = BloodCoagulationRecord.Result.Comment,
                StaffName = BloodCoagulationRecord.Result.StaffName.Select(s=>s.StaffPersonalInfoId).ToList(),
                Physician = BloodCoagulationRecord.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponce = BloodCoagulationRecord.Result.PhysicianResponce,
                OfficerToAct = BloodCoagulationRecord.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = BloodCoagulationRecord.Result.Deadline,
                Remark = BloodCoagulationRecord.Result.Remark,
                Status = BloodCoagulationRecord.Result.Status,
                TargetINRAttach = BloodCoagulationRecord.Result.TargetINRAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientBloodCoagulationRecord model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientBloodCoagulationRecord postlog = new PostClientBloodCoagulationRecord();

            #region Attachment
            if (model.TargetINRAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TargetINR_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetINRAttachment.OpenReadStream());
                model.TargetINRAttach = path;
            }
            else
            {
                model.TargetINRAttach = "No Image";
            }
            #endregion
                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Indication = model.Indication;
                postlog.StartDate = model.StartDate;
                postlog.TargetINR = model.TargetINR;
                postlog.CurrentDose = model.CurrentDose;
                postlog.INR = model.INR;
                postlog.NewDose = model.NewDose;
                postlog.NewINR = model.NewINR;
                postlog.BloodStatus = model.BloodStatus;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostBloodCoagStaffName{StaffPersonalInfoId=o,BloodRecordId = model.BloodRecordId}).ToList();
                postlog.Physician = model.Physician.Select(o => new PostBloodCoagPhysician{StaffPersonalInfoId=o,BloodRecordId = model.BloodRecordId}).ToList();
                postlog.PhysicianResponce = model.PhysicianResponce;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostBloodCoagOfficerToAct{ StaffPersonalInfoId = o, BloodRecordId = model.BloodRecordId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remark = model.Remark;
                postlog.Status = model.Status;
                postlog.TargetINRAttach = model.TargetINRAttach;
            
            var result = await _clientBloodCoagulationRecordService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Coagulation successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientBloodCoagulationRecord model)
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
            if (model.TargetINRAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_TargetINR_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TargetINRAttachment.OpenReadStream());
                model.TargetINRAttach = path;
            }
            else
            {
                model.TargetINRAttach = model.TargetINRAttach;
            }
            #endregion

            PutClientBloodCoagulationRecord put = new PutClientBloodCoagulationRecord();
            put.BloodRecordId = model.BloodRecordId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Indication = model.Indication;
            put.StartDate = model.StartDate;
            put.TargetINR = model.TargetINR;
            put.CurrentDose = model.CurrentDose;
            put.INR = model.INR;
            put.NewDose = model.NewDose;
            put.NewINR = model.NewINR;
            put.BloodStatus = model.BloodStatus;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutBloodCoagStaffName { StaffPersonalInfoId = o, BloodRecordId = model.BloodRecordId }).ToList();
            put.Physician = model.Physician.Select(o => new PutBloodCoagPhysician { StaffPersonalInfoId = o, BloodRecordId = model.BloodRecordId }).ToList();
            put.PhysicianResponce = model.PhysicianResponce;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutBloodCoagOfficerToAct { StaffPersonalInfoId = o, BloodRecordId = model.BloodRecordId }).ToList();
            put.Deadline = model.Deadline;
            put.Remark = model.Remark;
            put.Status = model.Status;
            put.TargetINRAttach = model.TargetINRAttach;
            var json = JsonConvert.SerializeObject(put);
            var entity = await _clientBloodCoagulationRecordService.Put(put);
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
