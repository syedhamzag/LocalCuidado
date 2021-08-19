﻿using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.Admin.Services.ConsentCare;
using AwesomeCare.Admin.Services.PersonalDetail;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.CarePlan;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail;
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
    public class PersonalDetailController : BaseController
    {
        private IConsentCareService _careService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;
        private IClientInvolvingParty _involvingparty;
        private IPersonalDetailService _personaldetailService;

        public PersonalDetailController (IFileUpload fileUpload, IStaffService staffService, IClientService clientService,IBaseRecordService baseRecord, 
            IConsentCareService careService, IClientInvolvingParty involvingparty, IPersonalDetailService personaldetailService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _careService = careService;
            _baseRecord = baseRecord;
            _involvingparty = involvingparty;
            _personaldetailService = personaldetailService;
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            CreateCarePlan model = new CreateCarePlan();
            model.ClientId = clientId;
            var bases = await _baseRecord.GetBaseRecord();

            var id1 = bases.Where(s => s.KeyName == "Indicator").FirstOrDefault().BaseRecordId;
            var items1 = await _baseRecord.GetBaseRecordWithItems(id1);
            model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


            var id2 = bases.Where(s => s.KeyName == "Focus").FirstOrDefault().BaseRecordId;
            var items2 = await _baseRecord.GetBaseRecordWithItems(id2);
            model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


            var id3 = bases.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
            var items3 = await _baseRecord.GetBaseRecordWithItems(id3);
            model.KeyLogList = items3.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


            var id4 = bases.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
            var items4 = await _baseRecord.GetBaseRecordWithItems(id4);
            model.LandLogList = items4.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


            var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);
            model.LandLogList = items4.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

            var staffs = await _staffService.GetStaffs();
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            var involve = await _clientService.GetClient(clientId);
            model.InvolingList = involve.InvolvingParties.Select(s => new SelectListItem(s.Name,s.ClientInvolvingPartyId.ToString())).ToList();


            var pdetail = await _personaldetailService.Get(model.ClientId);
            if (pdetail != null)
            {
                model = Get(model.ClientId);
                model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCarePlan model, IFormCollection formcollection)
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

                var bases3 = await _baseRecord.GetBaseRecord();
                var id3 = bases3.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
                var items3 = await _baseRecord.GetBaseRecordWithItems(id3);
                model.KeyLogList = items3.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

                var bases4 = await _baseRecord.GetBaseRecord();
                var id4 = bases4.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
                var items4 = await _baseRecord.GetBaseRecordWithItems(id4);
                model.LandLogList = items4.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

                var bases5 = await _baseRecord.GetBaseRecord();
                var id5 = bases5.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
                var items5 = await _baseRecord.GetBaseRecordWithItems(id5);
                model.TestList = items5.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                return View(model);
            }

            PostPersonalDetail pdetail = new PostPersonalDetail();
            pdetail.Capacity = new PostCapacity();
            pdetail.ConsentCare = new PostConsentCare();
            pdetail.ConsentData = new PostConsentData();
            pdetail.ConsentLandline = new PostConsentLandLine();
            pdetail.KeyIndicators = new PostKeyIndicators();
            pdetail.Personal = new PostPersonal();
            pdetail.Review = new PostReview();

            List<PostEquipment> equipment = new List<PostEquipment>();
            List<PostPersonCentred> pcentre = new List<PostPersonCentred>();

            #region Personal Detail
            pdetail.PersonalDetailId = model.PersonalDetailId;
            pdetail.ClientId = model.ClientId;
            #endregion

            #region Capacity
            pdetail.Capacity.CapacityId = model.CapacityId;
            pdetail.Capacity.PersonalDetailId = model.PersonalDetailId;
            pdetail.Capacity.Pointer = model.Pointer;
            pdetail.Capacity.Implications = model.Implications;
            pdetail.Capacity.Indicator = model.Indicator.Select(o => new PostCapacityIndicator { BaseRecordId = o, CapacityId = model.CapacityId }).ToList();
            #endregion

            #region ConsentCare
            pdetail.ConsentCare.PersonalDetailId = model.PersonalDetailId;
            pdetail.ConsentCare.CareId = model.CareId;
            pdetail.ConsentCare.Signature = model.CareSignature;
            pdetail.ConsentCare.Date = model.CareDate;
            pdetail.ConsentCare.Name = model.CareName;
            #endregion

            #region ConsentData
            pdetail.ConsentData.PersonalDetailId = model.PersonalDetailId;
            pdetail.ConsentData.DataId = model.DataId;
            pdetail.ConsentData.Signature = model.DataSignature;
            pdetail.ConsentData.Date = model.DataDate;
            pdetail.ConsentData.Name = model.DataName;
            #endregion

            #region ConsentLandLine
            pdetail.ConsentLandline.PersonalDetailId = model.PersonalDetailId;
            pdetail.ConsentLandline.LandlineId = model.LandLineId;
            pdetail.ConsentLandline.Signature = model.LandLineSignature;
            pdetail.ConsentLandline.Date = model.LandLineDate;
            pdetail.ConsentLandline.LogMethod = model.LandLineLogMethod.Select(o => new PostConsentLandlineLog { BaseRecordId = o, LandlineId = model.LandLineId }).ToList();
            #endregion

            #region Equipment
            for (int i=0; i < model.EquipmentCount; i++)
            {
                PostEquipment eq = new PostEquipment();
                string ImageId = "Image";
                var Image = formcollection.Files.GetFile(ImageId);
                var PersonalDetailId = int.Parse(formcollection["PersonalDetailId"][i]);
                var Location = int.Parse(formcollection["Location"][i]);
                var Name = int.Parse(formcollection["Name"][i]);
                var Type = int.Parse(formcollection["Type"][i]);
                var ServiceDate = DateTime.Parse(formcollection["ServiceDate"][i].ToString());
                var NextServiceDate = DateTime.Parse(formcollection["NextServiceDate"][i].ToString());
                var Status = int.Parse(formcollection["Status"][i]);
                var PersonToAct = int.Parse(formcollection["PersonToAct"][i]);
                string path = "";
                if (Image != null)
                {
                    string folder = "clientcomplain";
                    string filename = string.Concat(ImageId,DateTime.Now.ToString());
                    path = await _fileUpload.UploadFile(folder, true, filename, Image.OpenReadStream());
                }
                else
                {
                    path = "No Image";
                }
                
                eq.PersonalDetailId = PersonalDetailId;
                eq.Location = Location;
                eq.Name = Name;
                eq.Type = Type;
                eq.NextServiceDate = NextServiceDate;
                eq.ServiceDate = ServiceDate;
                eq.Status = Status;
                eq.PersonToAct = PersonToAct;
                eq.Attachment = path;
                equipment.Add(eq);
            }
            
            #endregion

            #region KeyIndicators
            pdetail.KeyIndicators.PersonalDetailId = model.PersonalDetailId;
            pdetail.KeyIndicators.AboutMe = model.AboutMe;
            pdetail.KeyIndicators.KeyId = model.KeyId;
            pdetail.KeyIndicators.FamilyRole = model.FamilyRole;
            pdetail.KeyIndicators.Debture = model.Debture;
            pdetail.KeyIndicators.LivingStatus = model.LivingStatus;
            pdetail.KeyIndicators.LogMethod = model.LogMethod.Select(o => new PostKeyIndicatorLog { BaseRecordId = o, KeyId = model.KeyId }).ToList();
            pdetail.KeyIndicators.ThingsILike = model.ThingsILike;
            #endregion

            #region Personal
            pdetail.Personal.PersonalDetailId = model.PersonalDetailId;
            pdetail.Personal.PersonalId = model.PersonalId;
            pdetail.Personal.Smoking = model.Smoking;
            pdetail.Personal.DNR = model.DNR;
            pdetail.Personal.Nationality = model.Nationality;
            pdetail.Personal.Religion = model.Religion;
            #endregion

            #region Person Centred
            for (int i = 0; i < model.PersonCentreCount; i++)
            {
                PostPersonCentred pc = new PostPersonCentred();
                var Class = int.Parse(formcollection["Class"][i]);
                var Focus = formcollection["Focus"];
                var ExpSupport = formcollection["ExpSupport"][i].ToString();
                var ex = new List<PostPersonCentredFocus>();
                for (int j = 0; j < Focus.Count; j++)
                {
                    var newtask = new PostPersonCentredFocus();
                    newtask.BaseRecordId = int.Parse(Focus[j].ToString());
                    newtask.PersonCentredId = 0;
                    ex.Add(newtask);
                }
                pc.Class = Class;
                pc.ExpSupport = ExpSupport;
                pc.Focus = ex;

                pcentre.Add(pc);

            }
            #endregion

            #region Review
            pdetail.Review.PersonalDetailId = model.PersonalDetailId;
            pdetail.Review.ReviewId = model.ReviewId;
            pdetail.Review.CP_PreDate = model.CP_PreDate;
            pdetail.Review.CP_ReviewDate = model.CP_ReviewDate;
            pdetail.Review.RA_PreDate = model.RA_PreDate;
            pdetail.Review.RA_ReviewDate = model.RA_ReviewDate;
            #endregion


            pdetail.Equipment = equipment;
            pdetail.PersonCentred = pcentre;

            if (pdetail.PersonalDetailId > 0)
            {
                var json = JsonConvert.SerializeObject(pdetail);
                var result = await _personaldetailService.Put(pdetail);
                var content = result.Content.ReadAsStringAsync();
            }
            else
            {
                var json = JsonConvert.SerializeObject(pdetail);
                var result = await _personaldetailService.Create(pdetail);
                var content = result.Content.ReadAsStringAsync();
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

            var pdetail = _personaldetailService.Get(clientId);
            var client = _clientService.GetClient(clientId);
            var details = _clientService.GetClientDetail();
            var party = _involvingparty.Get(clientId);
            var Relation = "N/A";
            if (party.Result != null)
            {
                Relation = party.Result.Relationship;
            }
            var putEntity = new CreateCarePlan
            {

                #region Personal Details
                ClientId = clientId,
                PersonalDetailId = pdetail.Result.PersonalDetailId,
                #endregion

                #region Capacity
                CapacityId = pdetail.Result.Capacity.FirstOrDefault().CapacityId,
                Implications = pdetail.Result.Capacity.FirstOrDefault().Implications,
                Pointer = pdetail.Result.Capacity.FirstOrDefault().Pointer,
                IndicatorList = pdetail.Result.Capacity.FirstOrDefault().Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                Indicator = pdetail.Result.Capacity.FirstOrDefault().Indicator.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region ConsentCare
                CareId = pdetail.Result.ConsentCare.FirstOrDefault().CareId,
                CareSignature = pdetail.Result.ConsentCare.FirstOrDefault().Signature,
                CareDate = pdetail.Result.ConsentCare.FirstOrDefault().Date,
                CareName = pdetail.Result.ConsentCare.FirstOrDefault().Name,
                CareRelation = Relation,
                #endregion

                #region ConsentData
                DataId = pdetail.Result.ConsentData.FirstOrDefault().DataId,
                DataSignature = pdetail.Result.ConsentData.FirstOrDefault().Signature,
                DataDate = pdetail.Result.ConsentData.FirstOrDefault().Date,
                DataName = pdetail.Result.ConsentData.FirstOrDefault().Name,
                DataRelation = Relation,
                #endregion

                #region ConsentLandLine
                LandLineId = pdetail.Result.ConsentLandline.FirstOrDefault().LandlineId,
                LandLineSignature = pdetail.Result.ConsentLandline.FirstOrDefault().Signature,
                LandLineDate = pdetail.Result.ConsentLandline.FirstOrDefault().Date,
                LandLogList = pdetail.Result.ConsentLandline.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                LandLineLogMethod = pdetail.Result.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList(),
                LandName = pdetail.Result.ConsentLandline.FirstOrDefault().Name,
                LandRelation = Relation,
                #endregion

                #region Equipment
                GetEquipment = pdetail.Result.Equipment,
                //EquipmentId = pdetail.Result.Equipment.FirstOrDefault().EquipmentId,
                //Location = pdetail.Result.Equipment.FirstOrDefault().Location,
                //Name = pdetail.Result.Equipment.FirstOrDefault().Name,
                //Type = pdetail.Result.Equipment.FirstOrDefault().Type,
                //NextServiceDate = pdetail.Result.Equipment.FirstOrDefault().NextServiceDate,
                //ServiceDate = pdetail.Result.Equipment.FirstOrDefault().ServiceDate,
                //Status = pdetail.Result.Equipment.FirstOrDefault().Status,
                //PersonToAct = pdetail.Result.Equipment.FirstOrDefault().PersonToAct,
                //Attachment = pdetail.Result.Equipment.FirstOrDefault().Attachment,
                StaffList = staff.Result.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                #endregion

                #region KeyIndicators
                AboutMe = pdetail.Result.KeyIndicators.FirstOrDefault().AboutMe,
                KeyId = pdetail.Result.KeyIndicators.FirstOrDefault().KeyId,
                FamilyRole = pdetail.Result.KeyIndicators.FirstOrDefault().FamilyRole,
                Debture = pdetail.Result.KeyIndicators.FirstOrDefault().Debture,
                LivingStatus = pdetail.Result.KeyIndicators.FirstOrDefault().LivingStatus,
                KeyLogList = pdetail.Result.KeyIndicators.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                LogMethod = pdetail.Result.KeyIndicators.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList(),
                ThingsILike = pdetail.Result.KeyIndicators.FirstOrDefault().ThingsILike,
                #endregion

                #region Personal
                PersonalId = pdetail.Result.Personal.FirstOrDefault().PersonalId,
                Smoking = pdetail.Result.Personal.FirstOrDefault().Smoking,
                DNR = pdetail.Result.Personal.FirstOrDefault().DNR,
                FullName = details.Result.Where(s => s.ClientId == clientId).Select(s => s.FullName).FirstOrDefault(),
                PreferredLanguage = client.Result.LanguageId,
                Gender = client.Result.GenderId,
                DateofBirth = client.Result.DateOfBirth,
                Address = client.Result.Address,
                PostCode = client.Result.PostCode,
                Telephone = client.Result.Telephone,
                PreferredName = client.Result.Firstname,
                AccessCode = client.Result.KeySafe,
                PreferredGender = client.Result.ChoiceOfStaffId,
                KeyWorker = client.Result.Keyworker,
                TeamLeader = client.Result.TeamLeader,
                #endregion

                #region Person Centred
                //PersonCentredId = pdetail.Result.PersonCentred.FirstOrDefault().PersonCentredId,
                //Class = pdetail.Result.PersonCentred.FirstOrDefault().Class,
                //ExpSupport = pdetail.Result.PersonCentred.FirstOrDefault().ExpSupport,
                //FocusList = pdetail.Result.PersonCentred.FirstOrDefault().Focus.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                //Focus = pdetail.Result.PersonCentred.FirstOrDefault().Focus.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region Review
                ReviewId = pdetail.Result.Review.FirstOrDefault().ReviewId,
                CP_PreDate = pdetail.Result.Review.FirstOrDefault().CP_PreDate,
                CP_ReviewDate = pdetail.Result.Review.FirstOrDefault().CP_ReviewDate,
                RA_PreDate = pdetail.Result.Review.FirstOrDefault().RA_PreDate,
                RA_ReviewDate = pdetail.Result.Review.FirstOrDefault().RA_ReviewDate,
                #endregion
                EquipmentCount = pdetail.Result.Equipment.Count,
                PersonCentreCount = pdetail.Result.PersonCentred.Count,
                ActionName = "Update",

            };
            return putEntity;

        }


    }
}
