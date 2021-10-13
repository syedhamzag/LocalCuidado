using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientProgram;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using Newtonsoft.Json;
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
    public class ClientProgramController : BaseController
    {
        private IClientProgramService _clientProgramService;
        private IBaseRecordService _baseService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientProgramController(IClientProgramService clientProgramService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientProgramService = clientProgramService;
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
            var entities = await _clientProgramService.Get();
            var client = await _clientService.GetClientDetail();
            List<CreateClientProgram> reports = new List<CreateClientProgram>();
            foreach (GetClientProgram item in entities)
            {
                var report = new CreateClientProgram();
                report.ProgramId = item.ProgramId;
                report.Date = item.Date;
                report.NextCheckDate = item.NextCheckDate;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.Attachment = item.Attachment;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientProgram();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }

        public async Task<IActionResult> View(int progId)
        {
            var Program = await _clientProgramService.Get(progId);
            var putEntity = new CreateClientProgram
            {
                ProgramId = Program.ProgramId,
                ClientId = Program.ClientId,
                Reference = Program.Reference,
                Attachment = Program.Attachment,
                Date = Program.Date,
                NextCheckDate = Program.NextCheckDate,
                Deadline = Program.Deadline,
                ProgramOfChoice = Program.ProgramOfChoice,
                URL = Program.URL,
                DaysOfChoice = Program.DaysOfChoice,
                Remarks = Program.Remarks,
                DetailsOfProgram = Program.DetailsOfProgram,
                Status = Program.Status,
                ActionRequired = Program.ActionRequired,
                Observation = Program.Observation,
                PlaceLocationProgram = Program.PlaceLocationProgram,
                OfficerToTakeAction = Program.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = Program.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),

            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int progId, string sender, string password, string recipient, string Smtp)
        {
            var Program = await _clientProgramService.Get(progId);
            var json = JsonConvert.SerializeObject(Program);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientProgram.pdf");
            string subject = "ClientProgram";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int progId)
        {
            var Program = await _clientProgramService.Get(progId);
            var json = JsonConvert.SerializeObject(Program);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientProgram.pdf");
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

        public async Task<IActionResult> Edit(int ProgramId)
        {
            var Program = _clientProgramService.Get(ProgramId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientProgram
            {
                ProgramId = Program.Result.ProgramId,
                ClientId = Program.Result.ClientId,
                Reference = Program.Result.Reference,
                Attachment = Program.Result.Attachment,
                Date = Program.Result.Date,
                NextCheckDate = Program.Result.NextCheckDate,
                Deadline = Program.Result.Deadline,
                ProgramOfChoice = Program.Result.ProgramOfChoice,
                URL = Program.Result.URL,
                DaysOfChoice = Program.Result.DaysOfChoice,
                OfficerToTakeAction = Program.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Remarks = Program.Result.Remarks,
                DetailsOfProgram = Program.Result.DetailsOfProgram,
                Status = Program.Result.Status,
                ActionRequired = Program.Result.ActionRequired,
                Observation = Program.Result.Observation,
                PlaceLocationProgram = Program.Result.PlaceLocationProgram,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()

            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientProgram model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientProgram postlog = new PostClientProgram();

            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientprogram";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Attachment = model.Attachment;
                postlog.Date = model.Date;
                postlog.NextCheckDate = model.NextCheckDate;
                postlog.Deadline = model.Deadline;
                postlog.ProgramOfChoice = model.ProgramOfChoice;
                postlog.URL = model.URL;
                postlog.DaysOfChoice = model.DaysOfChoice;
                postlog.OfficerToAct = model.OfficerToTakeAction.Select(o => new PostProgramOfficerToAct { StaffPersonalInfoId = o, ProgramId = model.ProgramId }).ToList();
                postlog.Remarks = model.Remarks;
                postlog.DetailsOfProgram = model.DetailsOfProgram;
                postlog.Status = model.Status;
                postlog.ActionRequired = model.ActionRequired;
                postlog.Observation = model.Observation;
                postlog.PlaceLocationProgram = model.PlaceLocationProgram;
                    
            var result = await _clientProgramService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result != null ? "New Program successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientProgram model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            #region Evidence
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientprogram";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion

                PutClientProgram put = new PutClientProgram();
                put.ProgramId = model.ProgramId;
                put.ClientId = model.ClientId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.NextCheckDate = model.NextCheckDate;
                put.Deadline = model.Deadline;
                put.ProgramOfChoice = model.ProgramOfChoice;
                put.URL = model.URL;
                put.DaysOfChoice = model.DaysOfChoice;
                put.OfficerToAct = model.OfficerToTakeAction.Select(o => new PutProgramOfficerToAct { StaffPersonalInfoId = o, ProgramId = model.ProgramId }).ToList();
                put.Remarks = model.Remarks;
                put.DetailsOfProgram = model.DetailsOfProgram;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.Observation = model.Observation;
                put.PlaceLocationProgram = model.PlaceLocationProgram;

            var json = JsonConvert.SerializeObject(put);
            var entity = await _clientProgramService.Put(put);
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
