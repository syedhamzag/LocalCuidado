﻿using AwesomeCare.Admin.Services.Client;
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
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.Admin.Extensions;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using OfficeOpenXml;
using Newtonsoft.Json;
using System.Data;

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
            var _client = await _clientService.GetClients();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            var clientRotas = await _clientRotaService.GetForEdit(clientId);
            var rotaTasks = await _rotaTaskService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();

            if (clientRotas.Count > 0)
            {
                model.IdNumber = _client.Where(s => s.ClientId == clientId).FirstOrDefault().IdNumber;
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
                        foreach (var tk in tasks)
                        {
                            var Id = rotaTasks.Where(s => s.RotaTaskId == tk.RotaTaskId).FirstOrDefault();
                            if (Id.RotaTaskId > 0)
                            {
                                workSheet.Cells["F" + taskRow].Value += Id.TaskName + " \n";
                                workSheet.Cells["I" + taskRow].Value += "[] \n";
                                workSheet.Cells["J" + taskRow].Value += "[] \n";
                            }
                        }
                        workSheet.Cells["F" + taskRow].Style.WrapText = true;
                        workSheet.Cells["I" + taskRow].Style.WrapText = true;
                        workSheet.Cells["J" + taskRow].Style.WrapText = true;
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
            var list = new List<GetClientDistance>();
            var clientdetail = await _clientService.GetClientDetail();
            var getClient = await _clientService.GetClients();
            var client = getClient.Where(s => s.PostCode == postcode).FirstOrDefault();
            if (client != null)
            { 
                var location1 = new Location
                {
                    Latitude = client.Latitude,
                    Longitude = client.Longitude,
                };
                var clients = getClient.ToList();
                foreach (var item in clients)
                {
                
                    var location2 = new Location
                    {
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };
                    if (item.Latitude != null && item.Longitude != null)
                    { 
                        double distance = CalculateDistance(location1, location2);
                        var result = new GetClientDistance
                        {
                            Fullname = clientdetail.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName,
                            Postcode = clients.Where(s => s.ClientId == item.ClientId).FirstOrDefault().PostCode,
                            Distance = distance
                        };
                        list.Add(result);
                    }
                }
                list.OrderBy(s=>s.Distance);
            }
            model.Client = list;
            return View(model);
        }
        public double CalculateDistance(Location point1, Location point2)
        {
            var p1Lat = double.Parse(point1.Latitude);
            var p1Lon = double.Parse(point1.Longitude);
            var p2Lat = double.Parse(point2.Latitude);
            var p2Lon = double.Parse(point2.Longitude);
            var d1 = p1Lat * (Math.PI / 180.0);
            var num1 = p1Lon * (Math.PI / 180.0);
            var d2 = p2Lat * (Math.PI / 180.0);
            var num2 = p2Lon * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
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
            var _client = await _clientService.GetClients();
            var client = await _clientService.GetClientDetail();
            if (model.Date==null || !ModelState.IsValid)
            {             
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);
            }
            
            var rotaLive = await _rotaTaskService.LiveRota(model.Date, model.eDate);
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
            var clientRotas = await _clientRotaService.GetForEdit(model.ClientId);
            var rotaTasks = await _rotaTaskService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();

            if (clientRotas.Count > 0)
            {
                model.IdNumber = _client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().IdNumber;
                model.ClientId = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().ClientId;
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                model.RotaTypes = rotaTypes;
                model.RotaTasks = rotaTasks.Select(s => new SelectListItem(s.TaskName, s.RotaTaskId.ToString())).ToList();
                model.ClientRotas = clientRotas;
                model.RotaLive = rotaLive;
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
                        var dt = new DataTable();
                        dt.Columns.Add("new");
                        int taskRow = row;
                        var tasks = rotaDywk.RotaTasks.Where(s => s.ClientRotaDaysId == rotaDywk.ClientRotaDaysId).ToList();
                        foreach (var tk in rotaTasks)
                        {
                            DataRow dr = dt.NewRow();
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
