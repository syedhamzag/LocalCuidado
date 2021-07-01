using AwesomeCare.Admin.Services.ClientSeizure;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientSeizure;
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
    public class ClientSeizureController : BaseController
    {
        private IClientSeizureService _clientSeizureService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientSeizureController(IClientSeizureService clientSeizureService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientSeizureService = clientSeizureService;
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
            var entities = await _clientSeizureService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientSeizure> reports = new List<CreateClientSeizure>();
            foreach (GetClientSeizure item in entities)
            {
                var report = new CreateClientSeizure();
                report.SeizureId = item.SeizureId;
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
            var model = new CreateClientSeizure();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int SeizId)
        {
            var Seizure = _clientSeizureService.Get(SeizId);
            var putEntity = new CreateClientSeizure
            {
                SeizureId = Seizure.Result.SeizureId,
                Reference = Seizure.Result.Reference,
                ClientId = Seizure.Result.ClientId,
                Date = Seizure.Result.Date,
                Time = Seizure.Result.Time,
                SeizureType = Seizure.Result.SeizureType,
                SeizureLength = Seizure.Result.SeizureLength,
                Often = Seizure.Result.Often,
                StatusImage = Seizure.Result.StatusImage,
                StatusAttach = Seizure.Result.StatusAttach,
                WhatHappened = Seizure.Result.WhatHappened,
                PhysicianResponse = Seizure.Result.PhysicianResponse,
                Deadline = Seizure.Result.Deadline,
                Remarks = Seizure.Result.Remarks,
                Status = Seizure.Result.Status,
                SeizureLengthAttach = Seizure.Result.SeizureLengthAttach,
                SeizureTypeAttach = Seizure.Result.SeizureTypeAttach,
                OfficerToAct = Seizure.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = Seizure.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = Seizure.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = Seizure.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = Seizure.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = Seizure.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int SeizId, string sender, string password, string recipient, string Smtp)
        {
            var Seizure = await _clientSeizureService.Get(SeizId);
            var json = JsonConvert.SerializeObject(Seizure);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientSeizure.pdf");
            string subject = "ClientSeizure";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int SeizId)
        {
            var Seizure = await _clientSeizureService.Get(SeizId);
            var json = JsonConvert.SerializeObject(Seizure);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientSeizure.pdf");
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
        public async Task<IActionResult> Edit(int SeizureId)
        {
            var Seizure = _clientSeizureService.Get(SeizureId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientSeizure
            {
                SeizureId = Seizure.Result.SeizureId,
                Reference = Seizure.Result.Reference,
                ClientId = Seizure.Result.ClientId,
                Date = Seizure.Result.Date,
                Time = Seizure.Result.Time,
                SeizureType = Seizure.Result.SeizureType,
                SeizureLength = Seizure.Result.SeizureLength,
                Often = Seizure.Result.Often,
                StatusImage = Seizure.Result.StatusImage,
                StatusAttach = Seizure.Result.StatusAttach,
                WhatHappened = Seizure.Result.WhatHappened,
                StaffName = Seizure.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = Seizure.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = Seizure.Result.PhysicianResponse,
                OfficerToAct = Seizure.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = Seizure.Result.Deadline,
                Remarks = Seizure.Result.Remarks,
                Status = Seizure.Result.Status,
                SeizureLengthAttach = Seizure.Result.SeizureLengthAttach,
                SeizureTypeAttach = Seizure.Result.SeizureTypeAttach,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientSeizure model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientSeizure postlog = new PostClientSeizure();

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

            if (model.SeizureTypeAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_SeizureTypeAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeizureTypeAttachment.OpenReadStream());
                model.SeizureTypeAttach = path;
            }
            else
            {
                model.SeizureTypeAttach = "No Image";
            }

            if (model.SeizureLengthAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_SeizureLengthAttachment_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.SeizureLengthAttachment.OpenReadStream());
                model.SeizureLengthAttach = path;
            }
            else
            {
                model.SeizureLengthAttach = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.SeizureLength = model.SeizureLength;
                postlog.SeizureType = model.SeizureType;
                postlog.Often = model.Often;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.WhatHappened = model.WhatHappened;
                postlog.StaffName = model.StaffName.Select(o => new PostSeizureStaffName { StaffPersonalInfoId = o, SeizureId = model.SeizureId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostSeizurePhysician { StaffPersonalInfoId = o, SeizureId = model.SeizureId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostSeizureOfficerToAct { StaffPersonalInfoId = o, SeizureId = model.SeizureId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;
                postlog.SeizureLengthAttach = model.SeizureLengthAttach;
                postlog.SeizureTypeAttach = model.SeizureTypeAttach;

            var result = await _clientSeizureService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Seizure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientSeizure model)
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
                string filename = string.Concat(folder, "_Evidence_", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.StatusAttachment.OpenReadStream());
                model.StatusAttach = path;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }

            if (model.SeizureTypeAttachment != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_SeizureType_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.SeizureTypeAttachment.OpenReadStream());
                model.SeizureTypeAttach = pathA;
            }
            else
            {
                model.SeizureTypeAttach = model.SeizureTypeAttach;
            }

            if (model.SeizureLengthAttachment != null)
            {
                string folderB = "clientcomplain";
                string filenameB = string.Concat(folderB, "_SeizureLength_", model.ClientId);
                string pathB = await _fileUpload.UploadFile(folderB, true, filenameB, model.SeizureLengthAttachment.OpenReadStream());
                model.SeizureLengthAttach = pathB;
            }
            else
            {
                model.StatusAttach = model.StatusAttach;
            }
            #endregion

                PutClientSeizure put = new PutClientSeizure();
                put.SeizureId = model.SeizureId;
                put.ClientId = model.ClientId;
                put.Reference = model.Reference;
                put.Date = model.Date;
                put.Time = model.Time;
                put.SeizureType = model.SeizureType;
                put.SeizureLength = model.SeizureLength;
                put.Often = model.Often;
                put.StatusImage = model.StatusImage;
                put.StatusAttach = model.StatusAttach;
                put.WhatHappened = model.WhatHappened;
                put.StaffName = model.StaffName.Select(o => new PutSeizureStaffName { StaffPersonalInfoId = o, SeizureId = model.SeizureId }).ToList();
                put.Physician = model.Physician.Select(o => new PutSeizurePhysician { StaffPersonalInfoId = o, SeizureId = model.SeizureId }).ToList();
                put.PhysicianResponse = model.PhysicianResponse;
                put.OfficerToAct = model.OfficerToAct.Select(o => new PutSeizureOfficerToAct { StaffPersonalInfoId = o, SeizureId = model.SeizureId }).ToList();
                put.Deadline = model.Deadline;
                put.Remarks = model.Remarks;
                put.Status = model.Status;
                put.SeizureLengthAttach = model.SeizureLengthAttach;
                put.SeizureTypeAttach = model.SeizureTypeAttach;

            var entity = await _clientSeizureService.Put(put);
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
