using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.DutyOnCall;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
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
    public class DutyOnCallController : BaseController
    {
        private IDutyOnCallService _dutyoncallService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseService;

        public DutyOnCallController(IDutyOnCallService dutyoncallService, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseService, IStaffService staffService) : base(fileUpload)
        {
            _dutyoncallService = dutyoncallService;
            _clientService = clientService;
            _baseService = baseService;
            _staffService = staffService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _dutyoncallService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateDutyOnCall> reports = new List<CreateDutyOnCall>();
            foreach (GetDutyOnCall item in entities)
            {
                var report = new CreateDutyOnCall();
                report.DutyOnCallId = item.DutyOnCallId;
                report.Attachment = item.Attachment;
                report.RefNo = item.RefNo;
                report.Subject = item.Subject;
                report.ClientId = item.ClientId;
                report.DateOfCall = item.DateOfCall;
                report.DateOfIncident = item.DateOfIncident;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.PriorityName = _baseService.GetBaseRecordItemById(item.Priority).Result.ValueName;
                report.NotificationStatusName = _baseService.GetBaseRecordItemById(item.NotificationStatus).Result.ValueName;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateDutyOnCall();
            model.ClientId = clientId;
            
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var staff = await _staffService.GetStaffs();
            model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);
        }

        public async Task<IActionResult> View(int DutyOnCallId)
        {
            var putEntity = await GetDutyOnCall(DutyOnCallId);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int DutyOnCallId)
        {
            var putEntity = await GetDutyOnCall(DutyOnCallId);
            return View(putEntity);
        }
        public async Task<CreateDutyOnCall> GetDutyOnCall(int DutyOnCallId)
        {
            var i = await _dutyoncallService.Get(DutyOnCallId);
            var staff = await _staffService.GetStaffs();
            
            var putEntity = new CreateDutyOnCall
            {
                ClientId = i.ClientId,
                Remarks = i.Remarks,
                NotificationStatus = i.NotificationStatus,
                Status = i.Status,
                ActionTaken = i.ActionTaken,
                Attachment = i.Attachment,
                ClientInitial = i.ClientInitial,
                DateOfCall = i.DateOfCall,
                DateOfIncident = i.DateOfIncident,
                DetailsOfIncident = i.DetailsOfIncident,
                DetailsRequired = i.DetailsRequired,
                DutyOnCallId = i.DutyOnCallId,
                NotifyPerson = i.NotifyPerson,
                NotifyStaffInvolved = i.NotifyStaffInvolved,
                PersonResponsible = i.PersonResponsible.Select(s => s.StaffPersonalInfoId).ToList(),
                PersonToAct = i.PersonToAct.Select(s=>s.StaffPersonalInfoId).ToList(),
                PositionOfReporting = i.PositionOfReporting,
                Priority = i.Priority,
                RefNo = i.RefNo,
                ReportedBy = i.ReportedBy,
                StaffBlacklisted = i.StaffBlacklisted,
                Subject = i.Subject,
                TelephoneToCall = i.TelephoneToCall,
                TimeOfCall = i.TimeOfCall,
                TypeOfDutyCall = i.TypeOfDutyCall,
                TypeOfIncident = i.TypeOfIncident,
                Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return putEntity;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateDutyOnCall model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                var staff = await _staffService.GetStaffs();
                model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            PostDutyOnCall duty = new PostDutyOnCall();
            if (model.Attach != null)
            {
                string extention = model.RefNo + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "dutyoncall";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            duty.ClientId = model.ClientId;
            duty.Remarks = model.Remarks;
            duty.Status = model.Status;
            duty.NotificationStatus = model.NotificationStatus;
            duty.ActionTaken = model.ActionTaken;
            duty.Attachment = model.Attachment;
            duty.ClientInitial = model.ClientInitial;
            duty.DateOfCall = model.DateOfCall;
            duty.DateOfIncident = model.DateOfIncident;
            duty.DetailsOfIncident = model.DetailsOfIncident;
            duty.DetailsRequired = model.DetailsRequired;
            duty.DutyOnCallId = model.DutyOnCallId;
            duty.NotifyPerson = model.NotifyPerson;
            duty.NotifyStaffInvolved = model.NotifyStaffInvolved;
            duty.PersonResponsible = model.PersonResponsible.Select(s => new PostDutyOnCallPersonResponsible { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId}).ToList();
            duty.PersonToAct = model.PersonToAct.Select(s => new PostDutyOnCallPersonToAct { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId}).ToList();
            duty.PositionOfReporting = model.PositionOfReporting;
            duty.Priority = model.Priority;
            duty.RefNo = model.RefNo;
            duty.ReportedBy = model.ReportedBy;
            duty.StaffBlacklisted = model.StaffBlacklisted;
            duty.Subject = model.Subject;
            duty.TelephoneToCall = model.TelephoneToCall;
            duty.TimeOfCall = model.TimeOfCall;
            duty.TypeOfDutyCall = model.TypeOfDutyCall;
            duty.TypeOfIncident = model.TypeOfIncident;

            var json = JsonConvert.SerializeObject(duty);
            var result = await _dutyoncallService.Create(duty);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateDutyOnCall model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                var staff = await _staffService.GetStaffs();
                model.Staffs = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }

            PutDutyOnCall duty = new PutDutyOnCall();
            if (model.Attach != null)
            {
                string extention = model.RefNo + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "dutyoncall";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = model.Attachment;
            }
            duty.ClientId = model.ClientId;
            duty.Remarks = model.Remarks;
            duty.Status = model.Status;
            duty.NotificationStatus = model.NotificationStatus;
            duty.ActionTaken = model.ActionTaken;
            duty.Attachment = model.Attachment;
            duty.ClientInitial = model.ClientInitial;
            duty.DateOfCall = model.DateOfCall;
            duty.DateOfIncident = model.DateOfIncident;
            duty.DetailsOfIncident = model.DetailsOfIncident;
            duty.DetailsRequired = model.DetailsRequired;
            duty.DutyOnCallId = model.DutyOnCallId;
            duty.NotifyPerson = model.NotifyPerson;
            duty.NotifyStaffInvolved = model.NotifyStaffInvolved;
            duty.PersonResponsible = model.PersonResponsible.Select(s => new PutDutyOnCallPersonResponsible { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId }).ToList();
            duty.PersonToAct = model.PersonToAct.Select(s => new PutDutyOnCallPersonToAct { StaffPersonalInfoId = s, DutyOnCallId = model.DutyOnCallId }).ToList();
            duty.PositionOfReporting = model.PositionOfReporting;
            duty.Priority = model.Priority;
            duty.RefNo = model.RefNo;
            duty.ReportedBy = model.ReportedBy;
            duty.StaffBlacklisted = model.StaffBlacklisted;
            duty.Subject = model.Subject;
            duty.TelephoneToCall = model.TelephoneToCall;
            duty.TimeOfCall = model.TimeOfCall;
            duty.TypeOfDutyCall = model.TypeOfDutyCall;
            duty.TypeOfIncident = model.TypeOfIncident;

            var entity = await _dutyoncallService.Put(duty);
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

        public async Task<IActionResult> Download(int DutyOnCallId)
        {
            var entity = await GetDutyOnCall(DutyOnCallId);
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
                Table table = new Table();
                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 12 }
                    )
                );
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
                run.AppendChild(new Text("Sheffield City Council - Communities"));
                

                Run run1 = para1.AppendChild(new Run());
                run1.AppendChild(new Text("Untoward Incident"));

                Run run2 = para2.AppendChild(new Run());
                run2.AppendChild(new Text("Please find below untoward incident as detail below:"));

                #region Table
                // Append the TableProperties object to the empty table.
                table.AppendChild<TableProperties>(tblProp);
                List<TableRow> trow = new List<TableRow>();
                List<TableCell> cells = new List<TableCell>();
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Service User initials"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.ClientInitial))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge{ Val = MergedCellValues.Continue}));
                trow[row].AppendChild(cells[cel]);
                
                cel++;                        
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Care First No."))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.RefNo))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Team Responsible"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Sheffield Social Services"))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Date Of Incident"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.DateOfIncident.ToString("dd.MM.yyyy")))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("NB: Initials only to be used for Service Users, their family members or any other party mentioned in this Untoward."))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Provider:"))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Reported By:"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.ReportedBy))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Position:"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(_baseService.GetBaseRecordItemById(entity.PositionOfReporting).Result.ValueName))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Tel:"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("20210150365206"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Date:"))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.DateOfIncident.ToString("dd.MM.yyyy")))));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.DetailsOfIncident))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Details of action taken:"))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.ActionTaken))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text("Details of any action required by service:"))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                row++;
                trow.Add(new TableRow());
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text(entity.DetailsRequired))));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Restart }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                cells.Add(new TableCell());
                cells[cel].AppendChild(new Paragraph(new Run(new Text())));
                cells[cel].AppendChild(new TableCellProperties(new HorizontalMerge { Val = MergedCellValues.Continue }));
                trow[row].AppendChild(cells[cel]);
                cel++;
                #endregion
                foreach (TableRow item in trow)
                {
                    table.AppendChild(item);
                }
                document.AppendChild(para);
                document.AppendChild(para1);
                document.AppendChild(para2);
                document.AppendChild(table);
                document.Save(mdp);
                
                mdp.Document.Save();
                
            }
            #endregion
            stream.Position = 0;
            string fileName = $"Untowatds-{client.FirstOrDefault(s=>s.ClientId == entity.ClientId).FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}
