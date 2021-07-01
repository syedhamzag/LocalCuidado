using AwesomeCare.Admin.Services.ClientFoodIntake;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake;
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
    public class ClientFoodIntakeController : BaseController
    {
        private IClientFoodIntakeService _clientFoodIntakeService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ClientFoodIntakeController(IClientFoodIntakeService clientFoodIntakeService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache,  IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientFoodIntakeService = clientFoodIntakeService;
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
            var entities = await _clientFoodIntakeService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientFoodIntake> reports = new List<CreateClientFoodIntake>();
            foreach (GetClientFoodIntake item in entities)
            {
                var report = new CreateClientFoodIntake();
                report.FoodIntakeId = item.FoodIntakeId;
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
            var model = new CreateClientFoodIntake();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int foodId)
        {
            var FoodIntake = _clientFoodIntakeService.Get(foodId);
            var putEntity = new CreateClientFoodIntake
            {
                FoodIntakeId = FoodIntake.Result.FoodIntakeId,
                Reference = FoodIntake.Result.Reference,
                ClientId = FoodIntake.Result.ClientId,
                Date = FoodIntake.Result.Date,
                Time = FoodIntake.Result.Time,
                Goal = FoodIntake.Result.Goal,
                CurrentIntake = FoodIntake.Result.CurrentIntake,
                StatusImage = FoodIntake.Result.StatusImage,
                StatusAttach = FoodIntake.Result.StatusAttach,
                Comment = FoodIntake.Result.Comment,
                PhysicianResponse = FoodIntake.Result.PhysicianResponse,
                Deadline = FoodIntake.Result.Deadline,
                Remarks = FoodIntake.Result.Remarks,
                Status = FoodIntake.Result.Status,
                OfficerToAct = FoodIntake.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = FoodIntake.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = FoodIntake.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                OfficerToActList = FoodIntake.Result.OfficerToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                StaffNameList = FoodIntake.Result.StaffName.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
                PhysicianList = FoodIntake.Result.Physician.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList(),
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int foodId, string sender, string password, string recipient, string Smtp)
        {
            var FoodIntake = await _clientFoodIntakeService.Get(foodId);
            var json = JsonConvert.SerializeObject(FoodIntake);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "ClientFoodIntake.pdf");
            string subject = "ClientFoodIntake";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int foodId)
        {
            var FoodIntake = await _clientFoodIntakeService.Get(foodId);
            var json = JsonConvert.SerializeObject(FoodIntake);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientFoodIntake.pdf");
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
        public async Task<IActionResult> Edit(int FoodIntakeId)
        {
            var FoodIntake = _clientFoodIntakeService.Get(FoodIntakeId);
            var staffs = await _staffService.GetStaffs();

            var putEntity = new CreateClientFoodIntake
            {
                FoodIntakeId = FoodIntake.Result.FoodIntakeId,
                Reference = FoodIntake.Result.Reference,
                ClientId = FoodIntake.Result.ClientId,
                Date = FoodIntake.Result.Date,
                Time = FoodIntake.Result.Time,
                Goal = FoodIntake.Result.Goal,
                CurrentIntake = FoodIntake.Result.CurrentIntake,
                StatusImage = FoodIntake.Result.StatusImage,
                StatusAttach = FoodIntake.Result.StatusAttach,
                Comment = FoodIntake.Result.Comment,
                StaffName = FoodIntake.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                Physician = FoodIntake.Result.Physician.Select(s => s.StaffPersonalInfoId).ToList(),
                PhysicianResponse = FoodIntake.Result.PhysicianResponse,
                OfficerToAct = FoodIntake.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                Deadline = FoodIntake.Result.Deadline,
                Remarks = FoodIntake.Result.Remarks,
                Status = FoodIntake.Result.Status,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientFoodIntake model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientFoodIntake postlog = new PostClientFoodIntake();

            #region Evidence
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
            #endregion

                postlog.ClientId = model.ClientId;
                postlog.Reference = model.Reference;
                postlog.Date = model.Date;
                postlog.Time = model.Time;
                postlog.Goal = model.Goal;
                postlog.CurrentIntake = model.CurrentIntake;
                postlog.StatusImage = model.StatusImage;
                postlog.StatusAttach = model.StatusAttach;
                postlog.Comment = model.Comment;
                postlog.StaffName = model.StaffName.Select(o => new PostFoodIntakeStaffName { StaffPersonalInfoId = o, FoodIntakeId = model.FoodIntakeId }).ToList();
                postlog.Physician = model.Physician.Select(o => new PostFoodIntakePhysician { StaffPersonalInfoId = o, FoodIntakeId = model.FoodIntakeId }).ToList();
                postlog.PhysicianResponse = model.PhysicianResponse;
                postlog.OfficerToAct = model.OfficerToAct.Select(o => new PostFoodIntakeOfficerToAct { StaffPersonalInfoId = o, FoodIntakeId = model.FoodIntakeId }).ToList();
                postlog.Deadline = model.Deadline;
                postlog.Remarks = model.Remarks;
                postlog.Status = model.Status;

            var result = await _clientFoodIntakeService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Food Intake successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientFoodIntake model)
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
            #endregion

            PutClientFoodIntake put = new PutClientFoodIntake();
            put.FoodIntakeId = model.FoodIntakeId;
            put.ClientId = model.ClientId;
            put.Reference = model.Reference;
            put.Date = model.Date;
            put.Time = model.Time;
            put.Goal = model.Goal;
            put.CurrentIntake = model.CurrentIntake;
            put.StatusImage = model.StatusImage;
            put.StatusAttach = model.StatusAttach;
            put.Comment = model.Comment;
            put.StaffName = model.StaffName.Select(o => new PutFoodIntakeStaffName { StaffPersonalInfoId = o, FoodIntakeId = model.FoodIntakeId }).ToList();
            put.Physician = model.Physician.Select(o => new PutFoodIntakePhysician { StaffPersonalInfoId = o, FoodIntakeId = model.FoodIntakeId }).ToList();
            put.PhysicianResponse = model.PhysicianResponse;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutFoodIntakeOfficerToAct { StaffPersonalInfoId = o, FoodIntakeId = model.FoodIntakeId }).ToList();
            put.Deadline = model.Deadline;
            put.Remarks = model.Remarks;
            put.Status = model.Status;

            var entity = await _clientFoodIntakeService.Put(put);
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
