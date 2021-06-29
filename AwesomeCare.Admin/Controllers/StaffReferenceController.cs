using AutoMapper;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.StaffReference;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffReference;
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
using AwesomeCare.DataTransferObject.DTOs.Client;
using Newtonsoft.Json;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffReferenceController : BaseController
    {
        private IStaffReferenceService _StaffReferenceService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public StaffReferenceController(IStaffReferenceService StaffReferenceService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _StaffReferenceService = StaffReferenceService;
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
            var entities = await _StaffReferenceService.Get();
            List<CreateStaffReference> reports = new List<CreateStaffReference>();
            foreach (GetStaffReference item in entities)
            {
                var report = new CreateStaffReference();
                report.StaffReferenceId = item.StaffReferenceId;
                report.Reference = item.Reference;
                report.Date = item.Date;
                report.RefreeName = item.RefreeName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }
        public async Task<IActionResult> Index(int? staffId)
        {
            var model = new CreateStaffReference();
            var staffs = await _staffService.GetStaffs();
            model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int refId)
        {
            var staffRef = await _StaffReferenceService.Get(refId);
            var json = JsonConvert.SerializeObject(staffRef);
            return View(staffRef);
        }
        public async Task<IActionResult> Email(int refId, string sender, string password, string recipient, string Smtp)
        {

            var staffRef = await _StaffReferenceService.Get(refId);
            var json = JsonConvert.SerializeObject(staffRef);
            byte[] byte1 = GeneratePdf(json);
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), "StaffReference.pdf");
            string subject = "StaffReference";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int refId)
        {
            var staffRef = await _StaffReferenceService.Get(refId);
            var json = JsonConvert.SerializeObject(staffRef);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "StaffReference.pdf");
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
        public async Task<IActionResult> Edit(int refId)
        {
            var StaffReference = _StaffReferenceService.Get(refId);
            var staffs = await _staffService.GetStaffs();

            var client = await _clientService.GetClientDetail();
            var putEntity = new CreateStaffReference
            {
                StaffReferenceId = StaffReference.Result.StaffReferenceId,
                Reference = StaffReference.Result.Reference,
                Address = StaffReference.Result.Address,
                ApplicantRole = StaffReference.Result.ApplicantRole,
                Caring = StaffReference.Result.Caring,
                ConfirmedBy = StaffReference.Result.ConfirmedBy,
                Contact = StaffReference.Result.Contact,
                DateofEmployement = StaffReference.Result.DateofEmployement,
                DateofExit = StaffReference.Result.DateofExit,
                RehireStaff = StaffReference.Result.RehireStaff,
                RefreeName = StaffReference.Result.RefreeName,
                Relationship = StaffReference.Result.Relationship,
                StaffId = StaffReference.Result.StaffId,
                WorkUnderPressure = StaffReference.Result.WorkUnderPressure,
                TeamWork = StaffReference.Result.TeamWork,
                Email = StaffReference.Result.Email,
                PreviousExperience = StaffReference.Result.PreviousExperience,
                Knowledgeable = StaffReference.Result.Knowledgeable,
                Integrity = StaffReference.Result.Integrity,
                Date = StaffReference.Result.Date,
                Status = StaffReference.Result.Status,
                Attachment = StaffReference.Result.Attachment,
                OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffReference model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                var client = await _clientService.GetClientDetail();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

            var postVoice = Mapper.Map<PostStaffReference>(model);

            var result = await _StaffReferenceService.Create(postVoice);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Reference successfully registered" : "An Error Occurred" });
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffId, ActiveTab = model.ActiveTab });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffReference model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.OfficerToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                var client = await _clientService.GetClientDetail();
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string folderA = "clientcomplain";
                string filenameA = string.Concat(folderA, "_Attachment_", model.StaffId);
                string pathA = await _fileUpload.UploadFile(folderA, true, filenameA, model.Attach.OpenReadStream());
                model.Attachment = pathA;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion
            var put = Mapper.Map<PutStaffReference>(model);
            var entity = await _StaffReferenceService.Put(put);
            
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
