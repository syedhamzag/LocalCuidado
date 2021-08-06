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
            //    report.BloodRecordId = item.BloodRecordId;
            //    report.Reference = item.Reference;
                report.ClientId = item.ClientId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
            //    report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }

            return View(reports);
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateCarePlan();

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

            var care = await _careService.Get(clientId.Value);
            if (care.CareId > 0)
            {
                model = await Get(clientId.Value);
                model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.ActionName = "Update";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCarePlan model)
        {
            if (model == null || !ModelState.IsValid)
            {
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

            var json = JsonConvert.SerializeObject(capacity);
            var json1 = JsonConvert.SerializeObject(care);
            var json2 = JsonConvert.SerializeObject(data);
            var json3 = JsonConvert.SerializeObject(land);
            var json4 = JsonConvert.SerializeObject(equip);
            var json5 = JsonConvert.SerializeObject(key);
            var json6 = JsonConvert.SerializeObject(personal);
            var json7 = JsonConvert.SerializeObject(centre);
            var json8 = JsonConvert.SerializeObject(review);
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

            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        public async Task<IActionResult> CareView(int clientId)
        {
            var putEntity = await Get(clientId);
            return View(putEntity);
        }
        public async Task<CreateCarePlan> Get(int clientId)
        {
            var staff = await _staffService.GetStaffs();

            var capacity = await _capacityService.Get(clientId);
            var care = await _careService.Get(clientId);
            var land = await _landService.Get(clientId);
            var data = await _dataService.Get(clientId);
            var equip = await _equipmentService.Get(clientId);
            var key = await _keyService.Get(clientId);
            var personal = await _personalService.Get(clientId);
            var centre = await _centredService.Get(clientId);
            var review = await _reviewService.Get(clientId);
            var client = await _clientService.GetClient(clientId);
            var details = await _clientService.GetClientDetail();
            var party = await _involvingparty.Get(clientId);
            var Name = "N/A";
            var Relation = "N/A";
            if (party != null)
            {
                Name = party.Name;
                Relation = party.Relationship;
            }

            var putEntity = new CreateCarePlan
            {

                ClientId = clientId,

                #region Capacity
                CapacityId = capacity.CapacityId,
                Implications = capacity.Implications,
                Pointer = capacity.Pointer,
                IndicatorList = capacity.Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                Indicator = capacity.Indicator.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region ConsentCare
                CareId = care.CareId,
                CareSignature = care.Signature,
                CareDate = care.Date,
                CareName = Name,
                CareRelation = Relation,
                #endregion

                #region ConsentData
                DataId = data.DataId,
                DataSignature = data.Signature,
                DataDate = data.Date,
                DataName = Name,
                DataRelation = Relation,
                #endregion

                #region ConsentLandLine
                LandLineId = land.LandlineId,
                LandLineSignature = land.Signature,
                LandLineDate = land.Date,
                LandLineLogMethod = land.LogMethod,
                LandName = Name,
                LandRelation = Relation,
                #endregion

                #region Equipment
                EquipmentId = equip.EquipmentId,
                Location = equip.Location,
                Name = equip.Name,
                Type = equip.Type,
                NextServiceDate = equip.NextServiceDate,
                ServiceDate = equip.ServiceDate,
                Status = equip.Status,
                PersonToAct = equip.PersonToAct,
                Attachment = equip.Attachment,
                StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                #endregion

                #region KeyIndicators
                AboutMe = key.AboutMe,
                KeyId = key.KeyId,
                FamilyRole = key.FamilyRole,
                Debture = key.Debture,
                LivingStatus = key.LivingStatus,
                LogMethod = key.LogMethod,
                ThingsILike = key.ThingsILike,
                #endregion

                #region Personal
                PersonalId = personal.PersonalId,
                Smoking = personal.Smoking,
                DNR = personal.DNR,
                FullName = details.Where(s => s.ClientId == clientId).Select(s => s.FullName).FirstOrDefault(),
                PreferredLanguage = client.LanguageId,
                Gender = client.GenderId,
                DateofBirth = client.DateOfBirth,
                Address = client.Address,
                PostCode = client.PostCode,
                Telephone = client.Telephone,
                PreferredName = client.Firstname,
                #endregion

                #region Person Centred
                PersonCentredId = centre.PersonCentredId,
                Class = centre.Class,
                ExpSupport = centre.ExpSupport,
                FocusList = centre.Focus.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                Focus = centre.Focus.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region Review
                ReviewId = review.ReviewId,
                CP_PreDate = review.CP_PreDate,
                CP_ReviewDate = review.CP_ReviewDate,
                RA_PreDate = review.RA_PreDate,
                RA_ReviewDate = review.RA_ReviewDate,
                #endregion

           
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
