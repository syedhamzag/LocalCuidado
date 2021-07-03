using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.ClientRota;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.ViewModels.Reporting;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.Admin.Extensions;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class ReportingController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IMemoryCache _cache;
        private readonly IClientRotaService _clientRotaService;
        private readonly IRotaTaskService _rotaTaskService;
        private readonly IClientRotaTypeService _clientRotaTypeService;

        public ReportingController(IFileUpload fileUpload, IMemoryCache cache, IClientService clientService,
            IClientRotaService clientRotaService, IRotaTaskService rotaTaskService, IClientRotaTypeService clientRotaTypeService) : base(fileUpload)
        {
            _clientService = clientService;
            _cache = cache;
            _clientRotaService = clientRotaService;
            _rotaTaskService = rotaTaskService;
            _clientRotaTypeService = clientRotaTypeService;
        }
        public async Task<IActionResult> EmptyLog()
        {
            var model = new ReportingViewModel();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyLog(int clientId, string Date)
        {
            var model = new ReportingViewModel();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            var clientRotas = await _clientRotaService.GetForEdit(clientId);
            var rotaTasks = await _rotaTaskService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();

            if (clientRotas.Count > 0)
            {

                model.ClientId = client.Where(s => s.ClientId == clientId).FirstOrDefault().ClientId;
                model.ClientName = client.Where(s=>s.ClientId==clientId).FirstOrDefault().FullName;
                model.RotaTypes = rotaTypes;
                model.RotaTasks = rotaTasks.Select(s=> new SelectListItem(s.TaskName, s.RotaTaskId.ToString())).ToList();
                model.ClientRotas = clientRotas;
            }
            HttpContext.Session.Set<List<GetClientRotaType>>("rotaTypes", rotaTypes);
            return View(model);
        }
        //public async Task<IActionResult> Download()
        //{
        //    ////var LogAudit = await _clientlogAuditService.Get(logId);
        //    ////var json = JsonConvert.SerializeObject(LogAudit);
        //    ////byte[] byte1 = GeneratePdf(json);

        //    //return File(byte1, "application/pdf", "ClientLogAudit.pdf");
        //}
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
                        var para = new Paragraph(paragraphs);
                        document.Add(para);
                        buffer = memStream.ToArray();
                        document.Close();
                    }
                }
                buffer = memStream.ToArray();
            }
            return buffer;
        }
        public IActionResult FilledLog()
        {
            return View();
        }
        public IActionResult EmptyMarChart()
        {
            return View();
        }
        public IActionResult FilledMarChart()
        {
            return View();
        }
    }
}
