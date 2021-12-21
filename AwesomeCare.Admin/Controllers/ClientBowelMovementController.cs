using AwesomeCare.Admin.Services.ClientBowelMovement;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement;
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
    public class ClientBowelMovementController : BaseController
    {
        private IClientBowelMovementService _clientBowelMovementService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientBowelMovementController(IClientBowelMovementService clientBowelMovementService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientBowelMovementService = clientBowelMovementService;
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
            var entities = await _clientBowelMovementService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientBowelMovement> reports = new List<CreateClientBowelMovement>();
            foreach (GetClientBowelMovement item in entities)
            {
                var report = new CreateClientBowelMovement();
                report.BowelMovementId = item.BowelMovementId;
                report.Date = item.Date;
                report.Deadline = item.Deadline;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.StatusAttach = item.StatusAttach;
                report.ColorAttach = item.ColorAttach;
                report.TypeAttach = item.TypeAttach;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientBowelMovement();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int bowelId)
        {
            var BowelMovement = _clientBowelMovementService.Get(bowelId);
            var putEntity = new CreateClientBowelMovement
            {
                BowelMovementId = BowelMovement.Result.BowelMovementId,
                ClientId = BowelMovement.Result.ClientId,
                Date = BowelMovement.Result.Date,
                Time = BowelMovement.Result.Time,
                Type = BowelMovement.Result.Type,
                Color = BowelMovement.Result.Color,
                Size = BowelMovement.Result.Size,
                StatusImage = BowelMovement.Result.StatusImage,
                StatusAttach = BowelMovement.Result.StatusAttach,
                Comment = BowelMovement.Result.Comment,
                PhysicianResponse = BowelMovement.Result.PhysicianResponse,
                Deadline = BowelMovement.Result.Deadline,
                Remarks = BowelMovement.Result.Remarks,
                Status = BowelMovement.Result.Status,
                Reference = BowelMovement.Result.Reference,
                ColorAttach = BowelMovement.Result.ColorAttach,
                TypeAttach = BowelMovement.Result.TypeAttach,
                OfficerToAct = BowelMovement.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = BowelMovement.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BowelMovement.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = BowelMovement.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = BowelMovement.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = BowelMovement.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int bowelId, string sender, string password, string recipient, string Smtp)
        {
            var BowelMovement = await _clientBowelMovementService.Get(bowelId);
            var json = JsonConvert.SerializeObject(BowelMovement);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientBowelMovement.pdf");
            string subject = "ClientBowelMovement";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int bowelId)
        {
            var BowelMovement = await _clientBowelMovementService.Get(bowelId);
            var json = JsonConvert.SerializeObject(BowelMovement);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientBowelMovement.pdf");
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
        public async Task<IActionResult> Edit(int BowelMovementId)
        {
            var BowelMovement = _clientBowelMovementService.Get(BowelMovementId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientBowelMovement
            {
                BowelMovementId = BowelMovement.Result.BowelMovementId,
                ClientId = BowelMovement.Result.ClientId,
                Date = BowelMovement.Result.Date,
                Time = BowelMovement.Result.Time,
                Type = BowelMovement.Result.Type,
                Color = BowelMovement.Result.Color,
                Size = BowelMovement.Result.Size,
                StatusImage = BowelMovement.Result.StatusImage,
                StatusAttach = BowelMovement.Result.StatusAttach,
                Comment = BowelMovement.Result.Comment,
                StaffName = BowelMovement.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = BowelMovement.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = BowelMovement.Result.PhysicianResponse,
                OfficerToAct = BowelMovement.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = BowelMovement.Result.Deadline,
                Remarks = BowelMovement.Result.Remarks,
                Status = BowelMovement.Result.Status,
                Reference = BowelMovement.Result.Reference,
                ColorAttach = BowelMovement.Result.ColorAttach,
                TypeAttach = BowelMovement.Result.TypeAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientBowelMovement model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientBowelMovement postlog = new PostClientBowelMovement();

            #region Attachment
            if (model.StatusAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.StatusAttachment.FileName);
                string folder = "clientbowelmovement";
                string filename = string.Concat(folder, "_StatusAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = "No Image";
            }

            if (model.ColorAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.ColorAttachment.FileName);
                string folder = "clientbowelmovement";
                string filename = string.Concat(folder, "_ColorAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ColorAttachment.OpenReadStream());
                model.ColorAttach = path;
            }
            else
            {
                model.ColorAttach = "No Image";
            }

            if (model.TypeAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TypeAttachment.FileName);
                string folder = "clientbowelmovement";
                string filename = string.Concat(folder, "_TypeAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TypeAttachment.OpenReadStream());
                model.TypeAttach = path;
            }
            else
            {
                model.TypeAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Type = model.Type;
                postlog.Color = model.Color;
                postlog.Size = model.Size;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostBowelMovementStaffName { StaffPersonalInfoId = o, BowelMovementId = model.BowelMovementId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostBowelMovementPhysician { StaffPersonalInfoId = o, BowelMovementId = model.BowelMovementId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostBowelMovementOfficerToAct { StaffPersonalInfoId = o, BowelMovementId = model.BowelMovementId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.ColorAttach = model.ColorAttach;
                postlog.TypeAttach = model.TypeAttach;
                postlog.StatusAttach = model.StatusAttach;
            
            var result = await _clientBowelMovementService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Bowel Movement successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientBowelMovement model)
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
                string folder = "clientbowelmovement";
                string filename = string.Concat(folder, "_StatusAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }

            if (model.TypeAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.TypeAttachment.FileName);
                string folder = "clientbowelmovement";
                string filename = string.Concat(folder, "_TypeAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.TypeAttachment.OpenReadStream());
                model.TypeAttach = path;
            }
            else
            {
                model.TypeAttach = model.TypeAttach;
            }

            if (model.ColorAttachment != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.ColorAttachment.FileName);
                string folder = "clientbowelmovement";
                string filename = string.Concat(folder, "_ColorAttachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ColorAttachment.OpenReadStream());
                model.ColorAttach = path;
            }
            else
            {
                model.ColorAttach = model.ColorAttach;
            }
            #endregion

            
            PutClientBowelMovement put = new PutClientBowelMovement();
            put.BowelMovementId = model.BowelMovementId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Type = model.Type;
            put.Color = model.Color;
            put.Size = model.Size;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutBowelMovementStaffName { StaffPersonalInfoId = o, BowelMovementId = model.BowelMovementId }).ToList();
            put.Physician = model.Physician.Select(o => new PutBowelMovementPhysician { StaffPersonalInfoId = o, BowelMovementId = model.BowelMovementId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutBowelMovementOfficerToAct { StaffPersonalInfoId = o, BowelMovementId = model.BowelMovementId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;
            put.ColorAttach = model.ColorAttach;
            put.TypeAttach = model.TypeAttach;
            put.StatusAttach = model.StatusAttach;
            var entity = await _clientBowelMovementService.Put(put);
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
