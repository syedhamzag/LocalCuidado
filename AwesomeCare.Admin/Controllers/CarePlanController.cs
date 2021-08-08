using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Capacity;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.Admin.Services.ConsentCare;
using AwesomeCare.Admin.Services.ConsentData;
using AwesomeCare.Admin.Services.ConsentLandLine;
using AwesomeCare.Admin.Services.Equipment;
using AwesomeCare.Admin.Services.KeyIndicators;
using AwesomeCare.Admin.Services.Personal;
using AwesomeCare.Admin.Services.PersonCentred;
using AwesomeCare.Admin.Services.Review;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.CarePlan;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class CarePlanController : BaseController
    {
        private ICapacityService _capacityService;
        private IConsentCareService _careService;
        private IConsentDataService _dataService;
        private IConsentLandLineService _landService;
        private IEquipmentService _equipmentService;
        private IKeyIndicatorsService _keyService;
        private IPersonalService _personalService;
        private IPersonCentredService _centredService;
        private IReviewService _reviewService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;
        private IClientInvolvingParty _involvingparty;

        public CarePlanController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, ICapacityService capacityService,
            IBaseRecordService baseRecord, IConsentCareService careService, IConsentDataService dataService, IConsentLandLineService landService,
            IEquipmentService equipmentService, IKeyIndicatorsService keyService, IPersonalService personalService, IPersonCentredService centredService,
            IReviewService reviewService, IClientInvolvingParty involvingparty) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _capacityService = capacityService;
            _careService = careService;
            _dataService = dataService;
            _landService = landService;
            _equipmentService = equipmentService;
            _keyService = keyService;
            _personalService = personalService;
            _centredService = centredService;
            _reviewService = reviewService;
            _baseRecord = baseRecord;
            _involvingparty = involvingparty;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _careService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateCarePlan> reports = new List<CreateCarePlan>();
            foreach (GetConsentCare item in entities)
            {
                var report = new CreateCarePlan();
                report.ClientId = item.ClientId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            CreateCarePlan model = new CreateCarePlan();
            model.ClientId = clientId;
            var bases1 = await _baseRecord.GetBaseRecord();
            var id1 = bases1.Where(s => s.KeyName == "Indicator").FirstOrDefault().BaseRecordId;
            var items1 = await _baseRecord.GetBaseRecordWithItems(id1);
            model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            
            var bases2 = await _baseRecord.GetBaseRecord();
            var id2 = bases2.Where(s => s.KeyName == "Focus").FirstOrDefault().BaseRecordId;
            var items2 = await _baseRecord.GetBaseRecordWithItems(id2);
            model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

            var staffs = await _staffService.GetStaffs();
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            var care = await _careService.Get(model.ClientId);
            if (care != null)
            {
                model = Get(model.ClientId);
                model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCarePlan model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                var bases1 = await _baseRecord.GetBaseRecord();
                var id1 = bases1.Where(s => s.KeyName == "Indicator").FirstOrDefault().BaseRecordId;
                var items1 = await _baseRecord.GetBaseRecordWithItems(id1);
                model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

                var bases2 = await _baseRecord.GetBaseRecord();
                var id2 = bases2.Where(s => s.KeyName == "Focus").FirstOrDefault().BaseRecordId;
                var items2 = await _baseRecord.GetBaseRecordWithItems(id2);
                model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                return View(model);
            }

            if (model.Attach != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "Attachment", model.ClientId);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;

            }
            else
            {
                if (model.Attachment != null)
                    model.Attachment = model.Attachment;
                model.Attachment = "No Image";
            }

            PostCapacity capacity = new PostCapacity();
            PostConsentCare care = new PostConsentCare();
            PostConsentData data = new PostConsentData();
            PostConsentLandLine land = new PostConsentLandLine();
            PostEquipment equip = new PostEquipment();
            PostKeyIndicators key = new PostKeyIndicators();
            PostPersonal personal = new PostPersonal();
            PostPersonCentred centre = new PostPersonCentred();
            PostReview review = new PostReview();

            #region Capacity
            capacity.ClientId = model.ClientId;
            capacity.CapacityId = model.CapacityId;
            capacity.Implications = model.Implications;
            capacity.Pointer = model.Pointer;
            capacity.Indicator = model.Indicator.Select(o => new PostCapacityIndicator { BaseRecordId = o, CapacityId = model.CapacityId }).ToList();
            #endregion

            #region ConsentCare
            care.ClientId = model.ClientId;
            care.CareId = model.CareId;
            care.Signature = model.CareSignature;
            care.Date = model.CareDate;
            #endregion

            #region ConsentData
            data.ClientId = model.ClientId;
            data.DataId = model.DataId;
            data.Signature = model.DataSignature;
            data.Date = model.DataDate;
            #endregion

            #region ConsentLandLine
            land.ClientId = model.ClientId;
            land.LandlineId = model.LandLineId;
            land.Signature = model.LandLineSignature;
            land.Date = model.LandLineDate;
            land.LogMethod = model.LandLineLogMethod;
            #endregion

            #region Equipment
            equip.EquipmentId = model.EquipmentId;
            equip.ClientId = model.ClientId;
            equip.Location = model.Location;
            equip.Name = model.Name;
            equip.Type = model.Type;
            equip.NextServiceDate = model.NextServiceDate;
            equip.ServiceDate = model.ServiceDate;
            equip.Status = model.Status;
            equip.PersonToAct = model.PersonToAct;
            equip.Attachment = model.Attachment;
            #endregion

            #region KeyIndicators
            key.ClientId = model.ClientId;
            key.AboutMe = model.AboutMe;
            key.KeyId = model.KeyId;
            key.FamilyRole = model.FamilyRole;
            key.Debture = model.Debture;
            key.LivingStatus = model.LivingStatus;
            key.LogMethod = model.LogMethod;
            key.ThingsILike = model.ThingsILike;
            #endregion

            #region Personal
            personal.ClientId = model.ClientId;
            personal.PersonalId = model.PersonalId;
            personal.Smoking = model.Smoking;
            personal.DNR = model.DNR;
            #endregion

            #region Person Centred
            centre.ClientId = model.ClientId;
            centre.PersonCentredId = model.PersonCentredId;
            centre.Class = model.Class;
            centre.ExpSupport = model.ExpSupport;
            centre.Focus = model.Focus.Select(o => new PostPersonCentredFocus { BaseRecordId = o, PersonCentredId = model.PersonCentredId }).ToList();
            #endregion

            #region Review
            review.ClientId = model.ClientId;
            review.ReviewId = model.ReviewId;
            review.CP_PreDate = model.CP_PreDate;
            review.CP_ReviewDate = model.CP_ReviewDate;
            review.RA_PreDate = model.RA_PreDate;
            review.RA_ReviewDate = model.RA_ReviewDate;
            #endregion
            if (care.CareId > 0)
            {
                var result = await _capacityService.Put(capacity);
                var result1 = await _careService.Put(care);
                var result2 = await _dataService.Put(data);
                var result3 = await _landService.Put(land);
                var result4 = await _equipmentService.Put(equip);
                var result5 = await _keyService.Put(key);
                var result6 = await _personalService.Put(personal);
                var result7 = await _centredService.Put(centre);
                var result8 = await _reviewService.Put(review);

                var content = result.Content.ReadAsStringAsync();
                var content1 = result1.Content.ReadAsStringAsync();
                var content2 = result2.Content.ReadAsStringAsync();
                var content3 = result3.Content.ReadAsStringAsync();
                var content4 = result4.Content.ReadAsStringAsync();
                var content5 = result5.Content.ReadAsStringAsync();
                var content6 = result6.Content.ReadAsStringAsync();
                var content7 = result7.Content.ReadAsStringAsync();
                var content8 = result8.Content.ReadAsStringAsync();
            }
            else
            { 
                var result = await _capacityService.Create(capacity);
                var result1 = await _careService.Create(care);
                var result2 = await _dataService.Create(data);
                var result3 = await _landService.Create(land);
                var result4 = await _equipmentService.Create(equip);
                var result5 = await _keyService.Create(key);
                var result6 = await _personalService.Create(personal);
                var result7 = await _centredService.Create(centre);
                var result8 = await _reviewService.Create(review);

                var content = result.Content.ReadAsStringAsync();
                var content1 = result1.Content.ReadAsStringAsync();
                var content2 = result2.Content.ReadAsStringAsync();
                var content3 = result3.Content.ReadAsStringAsync();
                var content4 = result4.Content.ReadAsStringAsync();
                var content5 = result5.Content.ReadAsStringAsync();
                var content6 = result6.Content.ReadAsStringAsync();
                var content7 = result7.Content.ReadAsStringAsync();
                var content8 = result8.Content.ReadAsStringAsync();
            }
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        public async Task<IActionResult> CareView(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            var putEntity = Get(clientId);
            return View(putEntity);
        }
        public CreateCarePlan Get(int clientId)
        {
            var staff = _staffService.GetStaffs();

            var capacity = _capacityService.Get(clientId);
            var care = _careService.Get(clientId);
            var land = _landService.Get(clientId);
            var data = _dataService.Get(clientId);
            var equip = _equipmentService.Get(clientId);
            var key = _keyService.Get(clientId);
            var personal = _personalService.Get(clientId);
            var centre = _centredService.Get(clientId);
            var review = _reviewService.Get(clientId);
            var client = _clientService.GetClient(clientId);
            var details = _clientService.GetClientDetail();
            var party = _involvingparty.Get(clientId);
            var Name = "N/A";
            var Relation = "N/A";
            if (party.Result != null)
            {
                Name = party.Result.Name;
                Relation = party.Result.Relationship;
            }

            var putEntity = new CreateCarePlan
            {

                ClientId = clientId,

                #region Capacity
                CapacityId = capacity.Result.CapacityId,
                Implications = capacity.Result.Implications,
                Pointer = capacity.Result.Pointer,
                IndicatorList = capacity.Result.Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                Indicator = capacity.Result.Indicator.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region ConsentCare
                CareId = care.Result.CareId,
                CareSignature = care.Result.Signature,
                CareDate = care.Result.Date,
                CareName = Name,
                CareRelation = Relation,
                #endregion

                #region ConsentData
                DataId = data.Result.DataId,
                DataSignature = data.Result.Signature,
                DataDate = data.Result.Date,
                DataName = Name,
                DataRelation = Relation,
                #endregion

                #region ConsentLandLine
                LandLineId = land.Result.LandlineId,
                LandLineSignature = land.Result.Signature,
                LandLineDate = land.Result.Date,
                LandLineLogMethod = land.Result.LogMethod,
                LandName = Name,
                LandRelation = Relation,
                #endregion

                #region Equipment
                EquipmentId = equip.Result.EquipmentId,
                Location = equip.Result.Location,
                Name = equip.Result.Name,
                Type = equip.Result.Type,
                NextServiceDate = equip.Result.NextServiceDate,
                ServiceDate = equip.Result.ServiceDate,
                Status = equip.Result.Status,
                PersonToAct = equip.Result.PersonToAct,
                Attachment = equip.Result.Attachment,
                StaffList = staff.Result.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                #endregion

                #region KeyIndicators
                AboutMe = key.Result.AboutMe,
                KeyId = key.Result.KeyId,
                FamilyRole = key.Result.FamilyRole,
                Debture = key.Result.Debture,
                LivingStatus = key.Result.LivingStatus,
                LogMethod = key.Result.LogMethod,
                ThingsILike = key.Result.ThingsILike,
                #endregion

                #region Personal
                PersonalId = personal.Result.PersonalId,
                Smoking = personal.Result.Smoking,
                DNR = personal.Result.DNR,
                FullName = details.Result.Where(s => s.ClientId == clientId).Select(s => s.FullName).FirstOrDefault(),
                PreferredLanguage = client.Result.LanguageId,
                Gender = client.Result.GenderId,
                DateofBirth = client.Result.DateOfBirth,
                Address = client.Result.Address,
                PostCode = client.Result.PostCode,
                Telephone = client.Result.Telephone,
                PreferredName = client.Result.Firstname,
                #endregion

                #region Person Centred
                PersonCentredId = centre.Result.PersonCentredId,
                Class = centre.Result.Class,
                ExpSupport = centre.Result.ExpSupport,
                FocusList = centre.Result.Focus.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                Focus = centre.Result.Focus.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region Review
                ReviewId = review.Result.ReviewId,
                CP_PreDate = review.Result.CP_PreDate,
                CP_ReviewDate = review.Result.CP_ReviewDate,
                RA_PreDate = review.Result.RA_PreDate,
                RA_ReviewDate = review.Result.RA_ReviewDate,
                #endregion

                ActionName = "Update",
           
            };
            return putEntity;

        }
        //public async Task<IActionResult> Download(int clientId)
        //{
        //    var rotaTypes = await _clientRotaTypeService.Get();
        //    var clientRotas = await _clientRotaService.GetForEdit(clientId);
        //    var rotaTasks = await _rotaTaskService.Get();
        //    var client = await _clientService.GetClientDetail();
        //    List<string> list = client.Select(s => s.FullName).ToList();
        //    var stream = new MemoryStream();
        //    int row = 2;

        //    using (var package = new ExcelPackage(stream))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //        workSheet.Column(4).Width = 30;
        //        workSheet.Column(5).Width = 12.8;
        //        workSheet.Column(7).Width = 16;
        //        workSheet.Column(9).Width = 9.4;

        //        workSheet.Cells["D1:J1"].Merge = true;
        //        workSheet.Cells["D1"].Value = "AWESOME HEALTHCARE SOLUTIONS LOG BOOK (Client Name:" + client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName + ") (ID:" + client.Where(s => s.ClientId == clientId).FirstOrDefault().ClientId + ")";
        //        workSheet.Cells["D1"].Style.Font.Bold = true;
        //        workSheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //        row++;

        //        foreach (var rotaType in rotaTypes)
        //        {
        //            int startRow = row++;
        //            var rotaDywk = clientRotas.FirstOrDefault(c => c.ClientRotaTypeId == rotaType.ClientRotaTypeId)?.ClientRotaDays?.FirstOrDefault(d => d.RotaDayofWeekId == 1);
        //            if (rotaDywk != null)
        //            {
        //                workSheet.Cells["C" + startRow].Value = rotaType.RotaType;
        //                workSheet.Cells["C" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + startRow].Value = "Date";
        //                workSheet.Cells["D" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells[string.Concat("F", startRow, ":H", startRow)].Merge = true;
        //                workSheet.Cells["F" + startRow].Value = "Please select 'Yes' for care delivered:";
        //                workSheet.Cells["F" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["I" + startRow].Value = "Yes";
        //                workSheet.Cells["I" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["J" + startRow].Value = "No";
        //                workSheet.Cells["J" + startRow].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                int taskRow = row;
        //                var tasks = rotaDywk.RotaTasks.Where(s => s.ClientRotaDaysId == rotaDywk.ClientRotaDaysId).ToList();
        //                foreach (var tk in tasks)
        //                {
        //                    var Id = rotaTasks.Where(s => s.RotaTaskId == tk.RotaTaskId).FirstOrDefault();
        //                    if (Id.RotaTaskId > 0)
        //                    {
        //                        workSheet.Cells["F" + taskRow].Value += Id.TaskName + " \n";
        //                        workSheet.Cells["I" + taskRow].Value += "[] \n";
        //                        workSheet.Cells["J" + taskRow].Value += "[] \n";
        //                    }
        //                }
        //                workSheet.Cells["F" + taskRow].Style.WrapText = true;
        //                workSheet.Cells["I" + taskRow].Style.WrapText = true;
        //                workSheet.Cells["J" + taskRow].Style.WrapText = true;
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Time In:";
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Time Out:";
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Duration";
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Carer 1: Full Name";
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Signature:";
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Carer 2: Full Name Signature:";
        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row++].Value = "Signature:";

        //                workSheet.Cells["F" + taskRow + ":H" + (row - 1)].Merge = true;
        //                workSheet.Cells["F" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["F" + taskRow + ":H" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["I" + taskRow + ":I" + (row - 1)].Merge = true;
        //                workSheet.Cells["I" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["I" + taskRow + ":I" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["J" + taskRow + ":J" + (row - 1)].Merge = true;
        //                workSheet.Cells["J" + taskRow].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["J" + taskRow + ":J" + (row - 1)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

        //                workSheet.Cells["D" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["D" + row].Value = "Bowel Movement: \n Oral Care: ";
        //                workSheet.Cells["D" + row].Style.WrapText = true;
        //                workSheet.Cells["D" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["E" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["E" + row].Value = " [] Yes \t [] No \n [] Yes \t [] No";
        //                workSheet.Cells["E" + row].Style.WrapText = true;
        //                workSheet.Cells["E" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

        //                workSheet.Cells["F" + row].Value = "Food and Fluid Prepared";
        //                workSheet.Cells["F" + row].Style.WrapText = true;
        //                workSheet.Cells["F" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["G" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["G" + row].Value = "\n";
        //                workSheet.Cells["H" + row].Value = " [] 1/4 \n [] 2/4 \n [] 3/4 \n [] Full";
        //                workSheet.Cells["H" + row].Style.WrapText = true;
        //                workSheet.Cells["H" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["H" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["I" + row].Value = "Handover to next Carers ";
        //                workSheet.Cells["I" + row].Style.WrapText = true;
        //                workSheet.Cells["I" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
        //                workSheet.Cells["I" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        //                workSheet.Cells["J" + row].Value = "\n";
        //                workSheet.Cells["J" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

        //                workSheet.Cells["E" + startRow + ":E" + row].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        //                workSheet.Cells["C" + startRow + ":C" + row].Merge = true;
        //                workSheet.Cells["C" + startRow + ":C" + row].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                workSheet.Cells["C" + startRow + ":C" + row].Style.TextRotation = 90;
        //                workSheet.Cells["C" + startRow + ":J" + row].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        //                row++;
        //            }
        //        }

        //        package.Save();
        //    }
        //    stream.Position = 0;
        //    string excelName = $"EmptyLog-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        //}
    }
}
