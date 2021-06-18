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

namespace AwesomeCare.Admin.Controllers
{
    public class ClientProgramController : BaseController
    {
        private IClientProgramService _clientProgramService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientProgramController(IClientProgramService clientProgramService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService) : base(fileUpload)
        {
            _clientProgramService = clientProgramService;
            _clientService = clientService;
            _staffService = staffService;
            _emailService = emailService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientProgramService.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientProgram();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ClientId = clientId.Value;
            return View(model);

        }

        public async Task<IActionResult> View(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var Program = await _clientProgramService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in Program)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(Program.FirstOrDefault());
            var newJson = json + staffName;
            return View(Program.FirstOrDefault());
        }
        public async Task<IActionResult> Email(string Reference, string sender, string password, string recipient, string Smtp)
        {
            string staffName = "\n OfficerToTakeAction:";
            var Program = await _clientProgramService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in Program)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(Program.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientProgram.pdf");
            string subject = "ClientProgram";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(string Reference)
        {
            string staffName = "\n OfficerToTakeAction:";
            var Program = await _clientProgramService.GetByRef(Reference);
            var staff = _staffService.GetStaffs();
            foreach (var item in Program)
            {
                staffName = staffName + "\n" + staff.Result.Where(s => s.StaffPersonalInfoId == item.OfficerToAct).Select(s => s.Fullname).FirstOrDefault();

            }
            var json = JsonConvert.SerializeObject(Program.FirstOrDefault());
            var newJson = json + staffName;
            byte[] byte1 = GeneratePdf(newJson);

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

        public async Task<IActionResult> Edit(string Reference)
        {
            List<int> officer = new List<int>();
            List<int> Ids = new List<int>();
            var Program = _clientProgramService.GetByRef(Reference);
            foreach (var item in Program.Result)
            {
                officer.Add(item.OfficerToAct);
                Ids.Add(item.ProgramId);
            }
            var staffs = await _staffService.GetStaffs();
            var putEntity = new CreateClientProgram
            {
                ProgramIds = Ids,
                ClientId = Program.Result.FirstOrDefault().ClientId,
                Reference = Program.Result.FirstOrDefault().Reference,
                Attachment = Program.Result.FirstOrDefault().Attachment,
                Date = Program.Result.FirstOrDefault().Date,
                NextCheckDate = Program.Result.FirstOrDefault().NextCheckDate,
                Deadline = Program.Result.FirstOrDefault().Deadline,
                ProgramOfChoice = Program.Result.FirstOrDefault().ProgramOfChoice,
                URL = Program.Result.FirstOrDefault().URL,
                DaysOfChoice = Program.Result.FirstOrDefault().DaysOfChoice,
                OfficerToTakeAction = officer,
                Remarks = Program.Result.FirstOrDefault().Remarks,
                DetailsOfProgram = Program.Result.FirstOrDefault().DetailsOfProgram,
                Status = Program.Result.FirstOrDefault().Status,
                ActionRequired = Program.Result.FirstOrDefault().ActionRequired,
                Observation = Program.Result.FirstOrDefault().Observation,
                PlaceLocationProgram = Program.Result.FirstOrDefault().PlaceLocationProgram,
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
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
            }
            #endregion

            List<PostClientProgram> posts = new List<PostClientProgram>();
            foreach (var item in model.OfficerToTakeAction)
            {
                var post = new PostClientProgram();
                post.ClientId = model.ClientId;
                post.Reference = model.Reference;
                post.Attachment = model.Attachment;
                post.Date = model.Date;
                post.NextCheckDate = model.NextCheckDate;
                post.Deadline = model.Deadline;
                post.ProgramOfChoice = model.ProgramOfChoice;
                post.URL = model.URL;
                post.DaysOfChoice = model.DaysOfChoice;
                post.OfficerToAct = item;
                post.Remarks = model.Remarks;
                post.DetailsOfProgram = model.DetailsOfProgram;
                post.Status = model.Status;
                post.ActionRequired = model.ActionRequired;
                post.Observation = model.Observation;
                post.PlaceLocationProgram = model.PlaceLocationProgram;
                    posts.Add(post);
            }

            var result = await _clientProgramService.Create(posts);
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
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.ClientId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
            int count = model.ProgramIds.Count;
            int i = 0;
            List<PutClientProgram> puts = new List<PutClientProgram>();
            foreach (var item in model.OfficerToTakeAction)
            {
                var put = new PutClientProgram();
                if (i < count)
                    put.ProgramId = model.ProgramIds[i];
                put.ClientId = model.ClientId;
                put.Reference = model.Reference;
                put.Attachment = model.Attachment;
                put.Date = model.Date;
                put.NextCheckDate = model.NextCheckDate;
                put.Deadline = model.Deadline;
                put.ProgramOfChoice = model.ProgramOfChoice;
                put.URL = model.URL;
                put.DaysOfChoice = model.DaysOfChoice;
                put.OfficerToAct = item;
                put.Remarks = model.Remarks;
                put.DetailsOfProgram = model.DetailsOfProgram;
                put.Status = model.Status;
                put.ActionRequired = model.ActionRequired;
                put.Observation = model.Observation;
                put.PlaceLocationProgram = model.PlaceLocationProgram;
                puts.Add(put);


            }
            var entity = await _clientProgramService.Put(puts);
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
