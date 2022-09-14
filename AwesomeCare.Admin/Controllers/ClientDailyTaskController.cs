using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientDailyTask;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ClientDailyTask;
using AwesomeCare.DataTransferObject.DTOs.ClientDailyTask;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
namespace AwesomeCare.Admin.Controllers
{
    public class ClientDailyTaskController : BaseController
    {
        private IClientDailyTaskService _ClientDailyTaskService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;

        public ClientDailyTaskController(IClientDailyTaskService ClientDailyTaskService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService) : base(fileUpload)
        {
            _ClientDailyTaskService = ClientDailyTaskService;
            _clientService = clientService;
            _baseService = baseService;

        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _ClientDailyTaskService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateClientDailyTask> reports = new List<CreateClientDailyTask>();
            foreach (GetClientDailyTask item in entities)
            {
                var report = new CreateClientDailyTask();
                report.DailyTaskId = item.DailyTaskId;
                report.DailyTaskName = item.DailyTaskName;
                report.Attachment = item.Attachment;
                report.Date = item.Date;
                report.AmendmentDate = item.AmendmentDate;
                report.ClientId = item.ClientId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateClientDailyTask();
            model.ClientId = clientId;

            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);
        }

        public async Task<IActionResult> View(int ClientDailyTaskId)
        {
            var putEntity = await GetClientDailyTask(ClientDailyTaskId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int ClientDailyTaskId)
        {
            var putEntity = await GetClientDailyTask(ClientDailyTaskId);
            return View(putEntity);
        }
        public async Task<CreateClientDailyTask> GetClientDailyTask(int ClientDailyTaskId)
        {
            var i = await _ClientDailyTaskService.Get(ClientDailyTaskId);

            var putEntity = new CreateClientDailyTask
            {
                ClientId = i.ClientId,
                DailyTaskName = i.DailyTaskName,
                Date = i.Date,
                AmendmentDate = i.AmendmentDate,
                Attachment = i.Attachment
            };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientDailyTask model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                return View(model);
            }

            PostClientDailyTask post = new PostClientDailyTask();
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientdailytask";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            post.ClientId = model.ClientId;
            post.DailyTaskName = model.DailyTaskName;
            post.AmendmentDate = model.AmendmentDate;
            post.Attachment = model.Attachment;
            post.Date = model.Date;

            var json = JsonConvert.SerializeObject(post);
            var result = await _ClientDailyTaskService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateClientDailyTask model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutClientDailyTask put = new PutClientDailyTask();
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "clientdailytask";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = model.Attachment;
            }
            put.ClientId = model.ClientId;
            put.DailyTaskName = model.DailyTaskName;
            put.AmendmentDate = model.AmendmentDate;
            put.Attachment = model.Attachment;
            put.Date = model.Date;

            var entity = await _ClientDailyTaskService.Put(put);
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

        public async Task<IActionResult> Download(int ClientDailyTaskId)
        {
            var entity = await GetClientDailyTask(ClientDailyTaskId);
            var client = await _clientService.GetClientDetail();
            var stream = new MemoryStream();
            int row = 0;
            int cel = 0;
            #region Word Document
            using (WordprocessingDocument doc =
                WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mdp = doc.MainDocumentPart;
                mdp = doc.AddMainDocumentPart();
                Document document = new Document(new Body());
                //Table table = new Table();
                //TableProperties tblProp = new TableProperties(
                //    new TableBorders(
                //        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                //        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                //        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                //        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                //        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                //        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 }
                //    )
                //);
                Paragraph para = new Paragraph();
                Paragraph para1 = new Paragraph();
                Paragraph para2 = new Paragraph();
                StyleRunProperties stylRunProps = new StyleRunProperties();
                Run run = para.AppendChild(new Run());
                RunProperties runProperties = run.AppendChild(new RunProperties());
                Bold bold = new Bold();
                bold.Val = OnOffValue.FromBoolean(true);
                FontSize fn = new FontSize();
                fn.Val = "60";
                runProperties.AppendChild(fn);
                runProperties.AppendChild(bold);
                runProperties.AppendChild(new Justification() { Val = JustificationValues.Center });
                run.AppendChild(new Text("Client Daily Task"));


                Run run1 = para1.AppendChild(new Run());
                run1.AppendChild(new Text(""));

                Run run2 = para2.AppendChild(new Run());
                run2.AppendChild(new Text(""));

                #region Table
                // Append the TableProperties object to the empty table.
                //table.AppendChild<TableProperties>(tblProp);
                //List<TableRow> trow = new List<TableRow>();
                //List<TableCell> cells = new List<TableCell>();
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Service User initials"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                //trow[row].AppendChild(cells[cel]);

                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Care First No."))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Team Responsible"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Sheffield Social Services"))));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Date Of Incident"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("NB: Initials only to be used for Service Users, their family members or any other party mentioned in this Untoward."))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Provider:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Reported By:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Position:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Tel:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text(""))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Date:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Details of action taken:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text("Details of any action required by service:"))));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                //row++;
                //trow.Add(new TableRow());
                //cells.Add(new TableCell());
                //cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                //trow[row].AppendChild(cells[cel]);
                //cel++;
                #endregion
                //foreach (TableRow item in trow)
                //{
                //    table.AppendChild(item);
                //}
                document.AppendChild(para);
                document.AppendChild(para1);
                document.AppendChild(para2);
                //document.AppendChild(table);
                document.Save(mdp);

                mdp.Document.Save();

            }
            #endregion
            stream.Position = 0;
            string fileName = $"DailyTask-{client.FirstOrDefault(s => s.ClientId == entity.ClientId).FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}
