using AwesomeCare.Admin.Services.ClientWoundCare;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;
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
    public class ClientWoundCareController : BaseController
    {
        private IClientWoundCareService _clientWoundCareService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientWoundCareController(IClientWoundCareService clientWoundCareService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientWoundCareService = clientWoundCareService;
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
            var entities = await _clientWoundCareService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientWoundCare> reports = new List<CreateClientWoundCare>();
            foreach (GetClientWoundCare item in entities)
            {
                var report = new CreateClientWoundCare();
                report.WoundCareId = item.WoundCareId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.UlcerStageAttach = item.UlcerStageAttach;
                report.StatusAttach = item.StatusAttach;
                report.MeasurementAttach = item.MeasurementAttach;
                report.TypeAttach = item.TypeAttach;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientWoundCare();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int woundId)
        {
            var WoundCare = await _clientWoundCareService.Get(woundId);
            var putEntity = new CreateClientWoundCare
            {
                WoundCareId = WoundCare.WoundCareId,
                Reference = WoundCare.Reference,
                ClientId = WoundCare.ClientId,
                Date = WoundCare.Date,
                Time = WoundCare.Time,
                Type = WoundCare.Type,
                Goal = WoundCare.Goal,
                Measurment = WoundCare.Measurment,
                UlcerStage = WoundCare.UlcerStage,
                PainLvl = WoundCare.PainLvl,
                Location = WoundCare.Location,
                WoundCause = WoundCare.WoundCause,
                StatusImage = WoundCare.StatusImage,
                StatusAttach = WoundCare.StatusAttach,
                Comment = WoundCare.Comment,
                PhysicianResponse = WoundCare.PhysicianResponse,
                Deadline = WoundCare.Deadline,
                Remarks = WoundCare.Remarks,
                Status = WoundCare.Status,
                LocationAttach = WoundCare.LocationAttach,
                MeasurementAttach = WoundCare.MeasurementAttach,
                TypeAttach = WoundCare.TypeAttach,
                UlcerStageAttach = WoundCare.UlcerStageAttach,
                OfficerToAct = WoundCare.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = WoundCare.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = WoundCare.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = WoundCare.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = WoundCare.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = WoundCare.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int woundId, string sender, string password, string recipient, string Smtp)
        {
            var WoundCare = await _clientWoundCareService.Get(woundId);
            var json = JsonConvert.SerializeObject(WoundCare);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientWoundCare.pdf");
            string subject = "ClientWoundCare";
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
        public async Task<IActionResult> Edit(int WoundCareId)
        {
            var WoundCare = _clientWoundCareService.Get(WoundCareId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientWoundCare
            {
                WoundCareId = WoundCare.Result.WoundCareId,
                Reference = WoundCare.Result.Reference,
                ClientId = WoundCare.Result.ClientId,
                Date = WoundCare.Result.Date,
                Time = WoundCare.Result.Time,
                Type = WoundCare.Result.Type,
                Goal = WoundCare.Result.Goal,
                Measurment = WoundCare.Result.Measurment,
                UlcerStage = WoundCare.Result.UlcerStage,
                PainLvl = WoundCare.Result.PainLvl,
                Location = WoundCare.Result.Location,
                WoundCause = WoundCare.Result.WoundCause,
                StatusImage = WoundCare.Result.StatusImage,
                StatusAttach = WoundCare.Result.StatusAttach,
                Comment = WoundCare.Result.Comment,
                StaffName = WoundCare.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = WoundCare.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = WoundCare.Result.PhysicianResponse,
                OfficerToAct = WoundCare.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = WoundCare.Result.Deadline,
                Remarks = WoundCare.Result.Remarks,
                Status = WoundCare.Result.Status,
                LocationAttach = WoundCare.Result.LocationAttach,
                MeasurementAttach = WoundCare.Result.MeasurementAttach,
                TypeAttach = WoundCare.Result.TypeAttach,
                UlcerStageAttach = WoundCare.Result.UlcerStageAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientWoundCare model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientWoundCare postlog = new PostClientWoundCare();

            #region Attachment
            if (model.StatusAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.StatusAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_StatusAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream(), model.StatusAttachment.ContentType);
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = "No Image";
            }

            if (model.TypeAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TypeAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_TypeAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TypeAttachment.OpenReadStream(), model.TypeAttachment.ContentType);
                model.TypeAttach = path;
            }
            else
            {
                model.TypeAttach = "No Image";
            }

            if (model.LocationAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.LocationAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_LocationAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.LocationAttachment.OpenReadStream(), model.LocationAttachment.ContentType);
                model.LocationAttach = path;
            }
            else
            {
                model.LocationAttach = "No Image";
            }

            if (model.UlcerStageAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.UlcerStageAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_UlcerStageAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.UlcerStageAttachment.OpenReadStream(), model.UlcerStageAttachment.ContentType);
                model.UlcerStageAttach = path;
            }
            else
            {
                model.UlcerStageAttach = "No Image";
            }

            if (model.MeasurementAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.MeasurementAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_MeasurementAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.MeasurementAttachment.OpenReadStream(), model.MeasurementAttachment.ContentType);
                model.MeasurementAttach = path;
            }
            else
            {
                model.MeasurementAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Type = model.Type;
                postlog.Goal = model.Goal;
                postlog.Measurment = model.Measurment;
                postlog.UlcerStage = model.UlcerStage;
                postlog.PainLvl = model.PainLvl;
                postlog.WoundCause = model.WoundCause;
                postlog.Location = model.Location;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostWoundCareStaffName { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostWoundCarePhysician { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostWoundCareOfficerToAct { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.LocationAttach = model.LocationAttach;
                postlog.MeasurementAttach = model.MeasurementAttach;
                postlog.TypeAttach = model.TypeAttach;
                postlog.UlcerStageAttach = model.UlcerStageAttach;

            var result = await _clientWoundCareService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Wound Care successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientWoundCare model)
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
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_StatusAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream(), model.StatusAttachment.ContentType);
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }

            if (model.TypeAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TypeAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_TypeAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TypeAttachment.OpenReadStream(), model.TypeAttachment.ContentType);
                model.TypeAttach = path;
            }
            else
            {
                model.TypeAttach = model.TypeAttach;
            }

            if (model.LocationAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.LocationAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_LocationAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.LocationAttachment.OpenReadStream(),model.LocationAttachment.ContentType);
                model.LocationAttach = path;
            }
            else
            {
                model.LocationAttach = model.LocationAttach;
            }

            if (model.UlcerStageAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.UlcerStageAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_UlcerStageAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.UlcerStageAttachment.OpenReadStream(), model.UlcerStageAttachment.ContentType);
                model.UlcerStageAttach = path;
            }
            else
            {
                model.UlcerStageAttach = model.UlcerStageAttach;
            }

            if (model.MeasurementAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.MeasurementAttachment.FileName);
                string folder = "clientwoundcare";
                string filename = string.Concat(folder, "_MeasurementAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.MeasurementAttachment.OpenReadStream(), model.MeasurementAttachment.ContentType);
                model.MeasurementAttach = path;
            }
            else
            {
                model.MeasurementAttach = model.MeasurementAttach;
            }
            #endregion

            PutClientWoundCare put = new PutClientWoundCare();
            put.WoundCareId = model.WoundCareId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Type = model.Type;
            put.Goal = model.Goal;
            put.UlcerStage = model.UlcerStage;
            put.Measurment = model.Measurment;
            put.Location = model.Location;
            put.PainLvl = model.PainLvl;
            put.WoundCause = model.WoundCause;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutWoundCareStaffName { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
            put.Physician = model.Physician.Select(o => new PutWoundCarePhysician { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutWoundCareOfficerToAct { StaffPersonalInfoId = o, WoundCareId = model.WoundCareId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.LocationAttach = model.LocationAttach;
            put.MeasurementAttach = model.MeasurementAttach;
            put.TypeAttach = model.TypeAttach;
            put.UlcerStageAttach = model.UlcerStageAttach;


            var entity = await _clientWoundCareService.Put(put);
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
        public async Task<IActionResult> Download(int woundId)
        {
            var entity = await GetDownload(woundId);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
        public async Task<CreateClientWoundCare> GetDownload(int Id)
        {
            var i = await _clientWoundCareService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateClientWoundCare
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                Reference = i.Reference,
                Date = i.Date,
                Time = i.Time,
                TypeAttach = i.TypeAttach,
                UlcerStageAttach = i.UlcerStageAttach,
                MeasurementAttach = i.MeasurementAttach,
                LocationAttach = i.LocationAttach,
                StatusAttach = i.StatusAttach,
                Comment = i.Comment,
                PhysicianResponse = i.PhysicianResponse,
                Deadline = i.Deadline,
                Remarks = i.Remarks,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
                GoalName = _baseService.GetBaseRecordItemById(i.Goal).Result.ValueName,
                TypeName = _baseService.GetBaseRecordItemById(i.Type).Result.ValueName,
                UlcerStageName = _baseService.GetBaseRecordItemById(i.UlcerStage).Result.ValueName,
                MeasurmentName = _baseService.GetBaseRecordItemById(i.Measurment).Result.ValueName,
                PainLvlName = _baseService.GetBaseRecordItemById(i.PainLvl).Result.ValueName,
                LocationName = _baseService.GetBaseRecordItemById(i.Location).Result.ValueName,
                WoundCauseName = _baseService.GetBaseRecordItemById(i.WoundCause).Result.ValueName,
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
