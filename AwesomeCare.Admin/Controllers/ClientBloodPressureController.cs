using AwesomeCare.Admin.Services.ClientBloodPressure;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure;
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
    public class ClientBloodPressureController : BaseController
    {
        private IClientBloodPressureService _clientBloodPressureService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientBloodPressureController(IClientBloodPressureService clientBloodPressureService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientBloodPressureService = clientBloodPressureService;
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
            var entities = await _clientBloodPressureService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientBloodPressure> reports = new List<CreateClientBloodPressure>();
            foreach (GetClientBloodPressure item in entities)
            {
                var report = new CreateClientBloodPressure();
                report.BloodPressureId = item.BloodPressureId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.StatusAttach = item.StatusAttach;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientBloodPressure();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int bloodId)
        {
            var BloodPressure = _clientBloodPressureService.Get(bloodId);
            var putEntity = new CreateClientBloodPressure
            {
                BloodPressureId = BloodPressure.Result.BloodPressureId,
                Reference = BloodPressure.Result.Reference,
                ClientId = BloodPressure.Result.ClientId,
                Date = BloodPressure.Result.Date,
                Time = BloodPressure.Result.Time,
                GoalSystolic = BloodPressure.Result.GoalSystolic,
                GoalDiastolic = BloodPressure.Result.GoalDiastolic,
                ReadingSystolic = BloodPressure.Result.ReadingSystolic,
                ReadingDiastolic = BloodPressure.Result.ReadingDiastolic,
                StatusImage = BloodPressure.Result.StatusImage,
                StatusAttach = BloodPressure.Result.StatusAttach,
                Comment = BloodPressure.Result.Comment,
                PhysicianResponse = BloodPressure.Result.PhysicianResponse,
                Deadline = BloodPressure.Result.Deadline,
                Remarks = BloodPressure.Result.Remarks,
                Status = BloodPressure.Result.Status,
                OfficerToAct = BloodPressure.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = BloodPressure.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BloodPressure.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = BloodPressure.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = BloodPressure.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = BloodPressure.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int bloodId, string sender, string password, string recipient, string Smtp)
        {
            var BloodPressure = await _clientBloodPressureService.Get(bloodId);
            var json = JsonConvert.SerializeObject(BloodPressure);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientBloodPressure.pdf");
            string subject = "ClientBloodPressure";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
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
        public async Task<IActionResult> Edit(int BloodPressureId)
        {
            var BloodPressure = _clientBloodPressureService.Get(BloodPressureId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientBloodPressure
            {
                BloodPressureId = BloodPressure.Result.BloodPressureId,
                Reference = BloodPressure.Result.Reference,
                ClientId = BloodPressure.Result.ClientId,
                Date = BloodPressure.Result.Date,
                Time = BloodPressure.Result.Time,
                GoalSystolic = BloodPressure.Result.GoalSystolic,
                GoalDiastolic = BloodPressure.Result.GoalDiastolic,
                ReadingSystolic = BloodPressure.Result.ReadingSystolic,
                ReadingDiastolic = BloodPressure.Result.ReadingDiastolic,
                StatusImage = BloodPressure.Result.StatusImage,
                StatusAttach = BloodPressure.Result.StatusAttach,
                Comment = BloodPressure.Result.Comment,
                StaffName = BloodPressure.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BloodPressure.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = BloodPressure.Result.PhysicianResponse,
                OfficerToAct = BloodPressure.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = BloodPressure.Result.Deadline,
                Remarks = BloodPressure.Result.Remarks,
                Status = BloodPressure.Result.Status,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientBloodPressure model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientBloodPressure postlog = new PostClientBloodPressure();

            #region Attachment
            if (model.StatusAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.StatusAttachment.FileName);
                string folder = "clientbloodpressure";
                string filename = string.Concat(folder, "_Status_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream(), model.StatusAttachment.ContentType);
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = "No Image";
            }
            #endregion

            postlog.ClientId = model.ClientId;
            postlog.Reference = model.Reference;
            postlog.Date = model.Date;
            postlog.Time = model.Time;
            postlog.GoalSystolic = model.GoalSystolic;
            postlog.GoalDiastolic = model.GoalDiastolic;
            postlog.ReadingSystolic = model.ReadingSystolic;
            postlog.ReadingDiastolic = model.ReadingDiastolic;
            postlog.StatusImage = model.StatusImage;
            postlog.StatusAttach = model.StatusAttach;
            postlog.Comment = model.Comment;
            postlog.StaffName = model.StaffName.Select(o => new PostBloodPressureStaffName { StaffPersonalInfoId = o, BloodPressureId = model.BloodPressureId }).ToList();
            postlog.Physician = model.Physician.Select(o => new PostBloodPressurePhysician { StaffPersonalInfoId = o, BloodPressureId = model.BloodPressureId }).ToList();
            postlog.PhysicianResponse = model.PhysicianResponse;
            postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostBloodPressureOfficerToAct { StaffPersonalInfoId = o, BloodPressureId = model.BloodPressureId }).ToList();
            postlog.Deadline = model.Deadline;
            postlog.Remarks = model.Remarks;
            postlog.Status = model.Status;

            var result = await _clientBloodPressureService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Pressure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientBloodPressure model)
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
                string extention = model.ClientId + System.IO.Path.GetExtension(model.StatusAttachment.FileName);
                string folder = "clientbloodpressure";
                string filename = string.Concat(folder, "_Status_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream(), model.StatusAttachment.ContentType);
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }
            #endregion

            PutClientBloodPressure put = new PutClientBloodPressure();
            put.BloodPressureId = model.BloodPressureId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.GoalSystolic = model.GoalSystolic;
            put.GoalDiastolic = model.GoalDiastolic;
            put.ReadingSystolic = model.ReadingSystolic;
            put.ReadingDiastolic = model.ReadingDiastolic;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutBloodPressureStaffName { StaffPersonalInfoId = o, BloodPressureId = model.BloodPressureId }).ToList();
            put.Physician = model.Physician.Select(o => new PutBloodPressurePhysician { StaffPersonalInfoId = o, BloodPressureId = model.BloodPressureId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutBloodPressureOfficerToAct { StaffPersonalInfoId = o, BloodPressureId = model.BloodPressureId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            var entity = await _clientBloodPressureService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }
        public async Task<IActionResult> Download(int bloodId)
        {
            var entity = await GetDownload(bloodId);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
        public async Task<CreateClientBloodPressure> GetDownload(int Id)
        {
            var i = await _clientBloodPressureService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateClientBloodPressure
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                Reference = i.Reference,
                Date = i.Date,
                Time = i.Time,
                StatusAttach = i.StatusAttach,
                Comment = i.Comment,
                Deadline = i.Deadline,
                PhysicianResponse = i.PhysicianResponse,
                Remarks = i.Remarks,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
                GoalSystolicName = _baseService.GetBaseRecordItemById(i.GoalSystolic).Result.ValueName,
                GoalDiastolicName = _baseService.GetBaseRecordItemById(i.GoalDiastolic).Result.ValueName,
                ReadingSystolicName = _baseService.GetBaseRecordItemById(i.ReadingSystolic).Result.ValueName,
                ReadingDiastolicName = _baseService.GetBaseRecordItemById(i.ReadingDiastolic).Result.ValueName,
                StatusImageName = _baseService.GetBaseRecordItemById(i.StatusImage).Result.ValueName,
            };
            foreach (var item in i.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.OfficerToActName))
                    putEntity.OfficerToActName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.OfficerToActName = putEntity.OfficerToActName + ", " + staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
            return putEntity;
        }
    }
}