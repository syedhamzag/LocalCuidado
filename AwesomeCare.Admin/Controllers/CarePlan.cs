using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Capacity;
using AwesomeCare.Admin.Services.Client;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class CarePlan : BaseController
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

        public CarePlan(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, ICapacityService capacityService,
            IBaseRecordService baseRecord, IConsentCareService careService, IConsentDataService dataService, IConsentLandLineService landService,
            IEquipmentService equipmentService, IKeyIndicatorsService keyService, IPersonalService personalService, IPersonCentredService centredService,
            IReviewService reviewService) : base(fileUpload)
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

            model.ClientId = clientId.Value;
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
    }
}
