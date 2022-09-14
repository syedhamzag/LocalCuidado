using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientCareObj;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Client.CareObj;
using AwesomeCare.Services.Services;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientCareObjController : BaseController
    {
        private IClientCareObjService _clientCareObjService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientCareObjController> _logger;
        private readonly IMemoryCache _cache;

        public ClientCareObjController(IClientCareObjService clientCareObjService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env,
            ILogger<ClientCareObjController> logger, IMemoryCache cache, IEmailService emailService, IBaseRecordService baseService) : base(fileUpload)
        {
            _clientCareObjService = clientCareObjService;
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
            var entities = await _clientCareObjService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientCareObj> reports = new List<CreateClientCareObj>();
            foreach (GetClientCareObj item in entities)
            {
                var report = new CreateClientCareObj();
                report.CareObjId = item.CareObjId;
                report.Date = item.Date;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateClientCareObj();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.PersonToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        public async Task<IActionResult> View(int CareObjId)
        {
            var CareObj = await _clientCareObjService.Get(CareObjId);
            var client = await _clientService.GetClientDetail();
            var putEntity = new CreateClientCareObj
            {
                CareObjId = CareObj.CareObjId,
                ClientId = CareObj.ClientId,
                ClientName = client.Where(s => s.ClientId == CareObj.ClientId).FirstOrDefault().FullName,
                Date = CareObj.Date,
                Remark = CareObj.Remark,
                Status = CareObj.Status,
                Note = CareObj.Note,
                PersonToAct = CareObj.PersonToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                PersonToActList = CareObj.PersonToAct.Select(s => new SelectListItem(s.StaffName, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        public async Task<IActionResult> Email(int CareObjId, string sender, string password, string recipient, string Smtp)
        {
            List<string> rcpt = new List<string>();
            var CareObj = await _clientCareObjService.Get(CareObjId);
            var json = JsonConvert.SerializeObject(CareObj);
            byte[] byte1 = GeneratePdf(json);
            string filename = "ClientCareObj.pdf";
            System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(new MemoryStream(byte1), filename);
            string subject = "ClientCareObj";
            string body = "";
            await _emailService.SendEmail(att, subject, body, sender, password, recipient, Smtp);
            await _emailService.SendAsync(rcpt, subject, body, byte1, filename, "pdf", true);
            return RedirectToAction("Reports");
        }
        public async Task<IActionResult> Download(int CareObjId)
        {
            var CareObj = await _clientCareObjService.Get(CareObjId);
            var json = JsonConvert.SerializeObject(CareObj);
            byte[] byte1 = GeneratePdf(json);

            return File(byte1, "application/pdf", "ClientCareObj.pdf");
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
        public async Task<IActionResult> Edit(int CareObjId)
        {
            var CareObj = await _clientCareObjService.Get(CareObjId);
            var staffs = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();
            var putEntity = new CreateClientCareObj
            {
                CareObjId = CareObj.CareObjId,
                ClientId = CareObj.ClientId,
                ClientName = client.Where(s => s.ClientId == CareObj.ClientId).FirstOrDefault().FullName,
                Date = CareObj.Date,
                Remark = CareObj.Remark,
                Status = CareObj.Status,
                Note = CareObj.Note,
                PersonToAct = CareObj.PersonToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                PersonToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientCareObj model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.PersonToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostClientCareObj postlog = new PostClientCareObj();

            postlog.ClientId = model.ClientId;
            postlog.Date = model.Date;
            postlog.PersonToAct = model.PersonToAct.Select(o => new PostCareObjPersonToAct { StaffPersonalInfoId = o, CareObjId = model.CareObjId }).ToList();
            postlog.Remark = model.Remark;
            postlog.Status = model.Status;
            postlog.Note = model.Note;

            var result = await _clientCareObjService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Care Obj successfully registered" : "An Error Occurred" });
            return RedirectToAction("Calender", new { clientId = model.ClientId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientCareObj model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staffs = await _staffService.GetStaffs();
                model.PersonToActList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }


            PutClientCareObj put = new PutClientCareObj();
            put.CareObjId = model.CareObjId;
            put.ClientId = model.ClientId;
            put.Date = model.Date;
            put.PersonToAct = model.PersonToAct.Select(o => new PutCareObjPersonToAct { StaffPersonalInfoId = o, CareObjId = model.CareObjId }).ToList();
            put.Remark = model.Remark;
            put.Status = model.Status;
            put.Note = model.Note;

            var entity = await _clientCareObjService.Put(put);
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

        public async Task<IActionResult> Calender(int? clientId)
        {
            var model = new CreateClientCareObjCalender();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            var obj = await _clientCareObjService.GetByClient(clientId.Value);
            model.GetClientCareObj = obj.ToList();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            return View(model);

        }
    }
}
