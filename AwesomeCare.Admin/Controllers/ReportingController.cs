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
using OfficeOpenXml;
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
        public async Task<IActionResult> Download(int clientId)
        {
            var rotaTypes = await _clientRotaTypeService.Get();
            var clientRotas = await _clientRotaService.GetForEdit(clientId);
            var rotaTasks = await _rotaTaskService.Get();
            var client = await _clientService.GetClientDetail();
            List<string> list = client.Select(s => s.FullName).ToList();
            var stream = new MemoryStream();
            int row = 2;
            
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Column(4).Width = 30;
                workSheet.Column(5).Width = 12.8;
                workSheet.Column(7).Width = 16;
                workSheet.Column(9).Width = 9.4;

                workSheet.Cells["D1:J1"].Merge = true;
                workSheet.Cells["D1"].Value = "AWESOME HEALTHCARE SOLUTIONS LOG BOOK (Client Name:"+client.Where(s=>s.ClientId==clientId).FirstOrDefault().FullName+") (ID:"+ client.Where(s => s.ClientId == clientId).FirstOrDefault().ClientId + ")";
                workSheet.Cells["D1"].Style.Font.Bold = true;
                workSheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                row++;
                
                foreach (var rotaType in rotaTypes)
                {
                    int startRow = row++;
                    var rotaDywk = clientRotas.FirstOrDefault(c => c.ClientRotaTypeId == rotaType.ClientRotaTypeId)?.ClientRotaDays?.FirstOrDefault(d => d.RotaDayofWeekId == 1);
                    if (rotaDywk != null)
                    {
                        workSheet.Cells["C" + startRow].Value = rotaType.RotaType;
                        workSheet.Cells["C" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + startRow].Value = "Date";
                        workSheet.Cells["D" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells[string.Concat("F", startRow, ":H", startRow)].Merge = true;
                        workSheet.Cells["F" + startRow].Value = "Please select 'Yes' for care delivered:";
                        workSheet.Cells["F" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["I" + startRow].Value = "Yes";
                        workSheet.Cells["I" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["J" + startRow].Value = "No";
                        workSheet.Cells["J" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        int taskRow = row;
                        var tasks = rotaDywk.RotaTasks.Where(s => s.ClientRotaDaysId == rotaDywk.ClientRotaDaysId).ToList();
                        foreach (var tk in rotaTasks)
                        {
                            if (tasks.FirstOrDefault(s => s.RotaTaskId==tk.RotaTaskId) != null)
                            {
                                workSheet.Cells["F" + taskRow].Value = tk.TaskName+" \n";
                                workSheet.Cells["I" + taskRow].Value = "[] \n";
                                workSheet.Cells["J" + taskRow].Value = "[] \n";
                            }
                        }
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Time In:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Time Out:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Duration";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Carer 1: Full Name";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Signature:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Carer 2: Full Name Signature:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Signature:";

                        workSheet.Cells["F" + taskRow + ":H" + (row - 1)].Merge = true;
                        workSheet.Cells["F" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["F" + taskRow + ":H" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["I" + taskRow + ":I" + (row - 1)].Merge = true;
                        workSheet.Cells["I" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["I" + taskRow + ":I" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["J" + taskRow + ":J" + (row - 1)].Merge = true;
                        workSheet.Cells["J" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["J" + taskRow + ":J" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row].Value = "Bowel Movement: \n Oral Care: ";
                        workSheet.Cells["D" + row].Style.WrapText = true;
                        workSheet.Cells["D" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["E" + row].Value = " [] Yes \t [] No \n [] Yes \t [] No";
                        workSheet.Cells["E" + row].Style.WrapText = true;
                        workSheet.Cells["E" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        
                        workSheet.Cells["F" + row].Value = "Food and Fluid Prepared";
                        workSheet.Cells["F" + row].Style.WrapText = true;
                        workSheet.Cells["F" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["G" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["G" + row].Value = "\n";
                        workSheet.Cells["H" + row].Value = " [] 1/4 \n [] 2/4 \n [] 3/4 \n [] Full";
                        workSheet.Cells["H" + row].Style.WrapText = true;
                        workSheet.Cells["H" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["H" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["I" + row].Value = "Handover to next Carers ";
                        workSheet.Cells["I" + row].Style.WrapText = true;
                        workSheet.Cells["I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["J" + row].Value = "\n";
                        workSheet.Cells["J" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        workSheet.Cells["E" +startRow +":E"+row].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells["C" + startRow + ":C" + row].Merge = true;
                        workSheet.Cells["C" + startRow + ":C" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        workSheet.Cells["C" + startRow + ":C" + row].Style.TextRotation = 90;
                        workSheet.Cells["C" + startRow + ":J" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
                        row++;
                    }
                }

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"EmptyLog-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
        public IActionResult DistanceFinder()
        {
            var model = new ReportingViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DistanceFinder(string postcode)
        {
            var model = new ReportingViewModel();
            var client = await _clientService.GetClients();
            var clients = client.Where(s => s.PostCode.Contains(postcode)).ToList();
            model.Client = clients;
            return View(model);
        }
        public async Task<IActionResult> FilledLog()
        {
            var model = new ReportingViewModel();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilledLog(ReportingViewModel model)
        {
            var client = await _clientService.GetClientDetail();
            if (model.Date==null || !ModelState.IsValid)
            {             
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            
            var rotaAdmin = await _rotaTaskService.LiveRota(model.Date);
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            var clientRotas = await _clientRotaService.GetForEdit(model.ClientId);
            var rotaTasks = await _rotaTaskService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();

            if (clientRotas.Count > 0)
            {

                model.ClientId = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().ClientId;
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                model.RotaTypes = rotaTypes;
                model.RotaTasks = rotaTasks.Select(s => new SelectListItem(s.TaskName, s.RotaTaskId.ToString())).ToList();
                model.ClientRotas = clientRotas;
                model.RotaAdmin = rotaAdmin;
            }
            HttpContext.Session.Set<List<GetClientRotaType>>("rotaTypes", rotaTypes);
            return View(model);
        }

        public async Task<IActionResult> FilledDownload(int clientId)
        {
            var rotaTypes = await _clientRotaTypeService.Get();
            var clientRotas = await _clientRotaService.GetForEdit(clientId);
            var rotaTasks = await _rotaTaskService.Get();
            var client = await _clientService.GetClientDetail();
            List<string> list = client.Select(s => s.FullName).ToList();
            var stream = new MemoryStream();
            int row = 2;

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Column(4).Width = 30;
                workSheet.Column(5).Width = 12.8;
                workSheet.Column(7).Width = 16;
                workSheet.Column(9).Width = 9.4;

                workSheet.Cells["D1:J1"].Merge = true;
                workSheet.Cells["D1"].Value = "AWESOME HEALTHCARE SOLUTIONS LOG BOOK (Client Name:" + client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName + ") (ID:" + client.Where(s => s.ClientId == clientId).FirstOrDefault().ClientId + ")";
                workSheet.Cells["D1"].Style.Font.Bold = true;
                workSheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                row++;

                foreach (var rotaType in rotaTypes)
                {
                    int startRow = row++;
                    var rotaDywk = clientRotas.FirstOrDefault(c => c.ClientRotaTypeId == rotaType.ClientRotaTypeId)?.ClientRotaDays?.FirstOrDefault(d => d.RotaDayofWeekId == 1);
                    if (rotaDywk != null)
                    {
                        workSheet.Cells["C" + startRow].Value = rotaType.RotaType;
                        workSheet.Cells["C" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + startRow].Value = "Date";
                        workSheet.Cells["D" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells[string.Concat("F", startRow, ":H", startRow)].Merge = true;
                        workSheet.Cells["F" + startRow].Value = "Please select 'Yes' for care delivered:";
                        workSheet.Cells["F" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["I" + startRow].Value = "Yes";
                        workSheet.Cells["I" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["J" + startRow].Value = "No";
                        workSheet.Cells["J" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        int taskRow = row;
                        var tasks = rotaDywk.RotaTasks.Where(s => s.ClientRotaDaysId == rotaDywk.ClientRotaDaysId).ToList();
                        foreach (var tk in rotaTasks)
                        {
                            if (tasks.FirstOrDefault(s => s.RotaTaskId == tk.RotaTaskId) != null)
                            {
                                workSheet.Cells["F" + taskRow].Value = tk.TaskName + " \n";
                                workSheet.Cells["I" + taskRow].Value = "[] \n";
                                workSheet.Cells["J" + taskRow].Value = "[] \n";
                            }
                        }
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Time In:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Time Out:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Duration";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Carer 1: Full Name";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Signature:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Carer 2: Full Name Signature:";
                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row++].Value = "Signature:";

                        workSheet.Cells["F" + taskRow + ":H" + (row - 1)].Merge = true;
                        workSheet.Cells["F" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["F" + taskRow + ":H" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["I" + taskRow + ":I" + (row - 1)].Merge = true;
                        workSheet.Cells["I" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["I" + taskRow + ":I" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["J" + taskRow + ":J" + (row - 1)].Merge = true;
                        workSheet.Cells["J" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["J" + taskRow + ":J" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["D" + row].Value = "Bowel Movement: \n Oral Care: ";
                        workSheet.Cells["D" + row].Style.WrapText = true;
                        workSheet.Cells["D" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["E" + row].Value = " [] Yes \t [] No \n [] Yes \t [] No";
                        workSheet.Cells["E" + row].Style.WrapText = true;
                        workSheet.Cells["E" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                        workSheet.Cells["F" + row].Value = "Food and Fluid Prepared";
                        workSheet.Cells["F" + row].Style.WrapText = true;
                        workSheet.Cells["F" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["G" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["G" + row].Value = "\n";
                        workSheet.Cells["H" + row].Value = " [] 1/4 \n [] 2/4 \n [] 3/4 \n [] Full";
                        workSheet.Cells["H" + row].Style.WrapText = true;
                        workSheet.Cells["H" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["H" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["I" + row].Value = "Handover to next Carers ";
                        workSheet.Cells["I" + row].Style.WrapText = true;
                        workSheet.Cells["I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        workSheet.Cells["I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells["J" + row].Value = "\n";
                        workSheet.Cells["J" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                        workSheet.Cells["E" + startRow + ":E" + row].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells["C" + startRow + ":C" + row].Merge = true;
                        workSheet.Cells["C" + startRow + ":C" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        workSheet.Cells["C" + startRow + ":C" + row].Style.TextRotation = 90;
                        workSheet.Cells["C" + startRow + ":J" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
                        row++;
                    }
                }

                package.Save();
            }
            stream.Position = 0;
            string excelName = $"FilledLog-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
