﻿using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
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
using Microsoft.Extensions.Logging;
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
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;
        private IClientInvolvingParty _involvingparty;
        private IPersonalDetailService _personaldetailService;
        private readonly ILogger<PersonalDetailController> logger;

        public PersonalDetailController (IFileUpload fileUpload, IStaffService staffService, IClientService clientService,IBaseRecordService baseRecord, 
            IClientInvolvingParty involvingparty, IPersonalDetailService personaldetailService,
            ILogger<PersonalDetailController> logger) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _baseRecord = baseRecord;
            _involvingparty = involvingparty;
            _personaldetailService = personaldetailService;
            this.logger = logger;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _personaldetailService.Get();
            var staff = _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            List<CreateCarePlan> reports = new List<CreateCarePlan>();
            foreach (GetPersonalDetail item in entities)
            {
                var report = new CreateCarePlan();
                report.PersonalDetailId = item.PersonalDetailId;
                report.ClientId = item.ClientId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }

        public async ValueTask<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            CreateCarePlan model = new CreateCarePlan();
            model.GetPersonCentred = new List<GetPersonCentred>();
            model.GetEquipment = new List<GetEquipment>();
            model.ClientId = clientId;
            var bases = await _baseRecord.GetBaseRecord();

            var id1 = bases.Where(s => s.KeyName == "Indicator").FirstOrDefault().BaseRecordId;
            var items1 = await _baseRecord.GetBaseRecordWithItems(id1);
            model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

            var id3 = bases.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
            var items3 = await _baseRecord.GetBaseRecordWithItems(id3);
            model.KeyLogList = items3.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


            var id4 = bases.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
            var items4 = await _baseRecord.GetBaseRecordWithItems(id4);
            model.LandLogList = items4.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


            var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);
            model.ClassList = classItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

            foreach (var item in model.ClassList)
            {
                var child = bases.Where(s => s.KeyName == item.Text).FirstOrDefault().BaseRecordId;
                var childItems = await _baseRecord.GetBaseRecordWithItems(child);

                if (item.Text.ToString() == "Individuality")
                {
                    model.Individuality = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text,model.Individuality);
                }
                if (item.Text.ToString() == "RightsAndRespect")
                {
                    model.RightsAndRespect= childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text,model.RightsAndRespect);
                }
                if (item.Text.ToString() == "Choice")
                {
                    model.Choice = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text,model.Choice);
                }
                if (item.Text.ToString() == "DignityAndPrivacy")
                {
                    model.DignityAndPrivacy = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text,model.DignityAndPrivacy);
                }
                if (item.Text.ToString() == "Partnership")
                {
                    model.Partnership = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text,model.Partnership);
                }

            }

            var staffs = await _staffService.GetStaffs();
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            var involve = await _clientService.GetClient(clientId);
            model.InvolingList = involve.InvolvingParties.Select(s => new SelectListItem(s.Name,s.ClientInvolvingPartyId.ToString())).ToList();


            var pdetail = await _personaldetailService.Get(model.ClientId);
            if (pdetail != null)
            {
                model =await Get(model.ClientId);
                model.ClassList = classItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();

                foreach (var item in model.ClassList)
                {
                    var child = bases.Where(s => s.KeyName == item.Text).FirstOrDefault().BaseRecordId;
                    var childItems = await _baseRecord.GetBaseRecordWithItems(child);

                    if (item.Text.ToString() == "Individuality")
                    {
                        model.Individuality = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                        model.FocusList.Add(item.Text, model.Individuality);
                    }
                    if (item.Text.ToString() == "RightsAndRespect")
                    {
                        model.RightsAndRespect = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                        model.FocusList.Add(item.Text, model.RightsAndRespect);
                    }
                    if (item.Text.ToString() == "Choice")
                    {
                        model.Choice = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                        model.FocusList.Add(item.Text, model.Choice);
                    }
                    if (item.Text.ToString() == "DignityAndPrivacy")
                    {
                        model.DignityAndPrivacy = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                        model.FocusList.Add(item.Text, model.DignityAndPrivacy);
                    }
                    if (item.Text.ToString() == "Partnership")
                    {
                        model.Partnership = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                        model.FocusList.Add(item.Text, model.Partnership);
                    }
                }
                int i = 1;
                foreach (var item in model.GetPersonCentred)
                {
                    if (item.Class.ToString() == model.ClassList.Where(s=>s.Text == "Individuality").FirstOrDefault().Value)
                        model.Focus1 = item.Focus.Select(s=>s.BaseRecordId).ToList();
                    if (item.Class.ToString() == model.ClassList.Where(s => s.Text == "RightsAndRespect").FirstOrDefault().Value)
                        model.Focus2 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (item.Class.ToString() == model.ClassList.Where(s => s.Text == "Choice").FirstOrDefault().Value)
                        model.Focus3 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (item.Class.ToString() == model.ClassList.Where(s => s.Text == "DignityAndPrivacy").FirstOrDefault().Value)
                        model.Focus4 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (item.Class.ToString() == model.ClassList.Where(s => s.Text == "Partnership").FirstOrDefault().Value)
                        model.Focus5 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    i++;
                }
                model.PersonCentreCount = pdetail.PersonCentred.Count;
                model.GetEquipment = pdetail.Equipment;
                model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.KeyLogList = items3.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.LandLogList = items4.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                model.InvolingList = involve.InvolvingParties.Select(s => new SelectListItem(s.Name, s.ClientInvolvingPartyId.ToString())).ToList();
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
                var bases = await _baseRecord.GetBaseRecord();
                var id1 = bases.Where(s => s.KeyName == "Indicator").FirstOrDefault().BaseRecordId;
                var items1 = await _baseRecord.GetBaseRecordWithItems(id1);
                model.IndicatorList = items1.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


                //var id2 = bases.Where(s => s.KeyName == "Focus").FirstOrDefault().BaseRecordId;
                //var items2 = await _baseRecord.GetBaseRecordWithItems(id2);
                //model.FocusList = items2.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


                var id3 = bases.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
                var items3 = await _baseRecord.GetBaseRecordWithItems(id3);
                model.KeyLogList = items3.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();


                var id4 = bases.Where(s => s.KeyName == "LogMethod").FirstOrDefault().BaseRecordId;
                var items4 = await _baseRecord.GetBaseRecordWithItems(id4);
                model.LandLogList = items4.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();



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
            pdetail.ConsentLandline.Name = model.LandName;
            pdetail.ConsentLandline.Date = model.LandLineDate;
            pdetail.ConsentLandline.LogMethod = model.LandLineLogMethod.Select(o => new PostConsentLandlineLog { BaseRecordId = o, LandlineId = model.LandLineId }).ToList();
            #endregion

            #region Equipment
            for (int i = 0; i < model.EquipmentCount; i++)
            {
                PostEquipment eq = new PostEquipment();
                string ImageId = "Image";
                var EquipmentId = int.Parse(formcollection["EquipmentId"][i]);
                string Attachment = "";
                if (model.PersonalDetailId > 0) 
                {
                    Attachment = formcollection["Attachment"][i] != null ? formcollection["Attachment"][i] : "";
                }
                
                var Image = formcollection.Files.GetFile(ImageId);
                var Location = int.Parse(formcollection["Location"][i]);
                var Name = int.Parse(formcollection["Name"][i]);
                var Type = int.Parse(formcollection["Type"][i]);
                var ServiceDate = DateTime.Parse(formcollection["ServiceDate"][i].ToString());
                var NextServiceDate = DateTime.Parse(formcollection["NextServiceDate"][i].ToString());
                var Status = int.Parse(formcollection["Status"][i]);
                var PersonToAct = int.Parse(formcollection["PersonToAct"][i]);
                string path = "";
                #region Attachment
                if (!string.IsNullOrWhiteSpace(Attachment))
                {
                    eq.Attachment = Attachment;
                }
                else
                { 
                    if (Image != null)
                    {
                        string folder = "clientcomplain";
                        string filename = string.Concat(ImageId,DateTime.Now.ToString());
                        path = await _fileUpload.UploadFile(folder, true, filename, Image.OpenReadStream(), Image.ContentType);
                    }
                    else
                    {
                        path = "No Image";
                    }
                    eq.Attachment = path;
                }
                #endregion
                eq.EquipmentId = EquipmentId;
                eq.PersonalDetailId = model.PersonalDetailId;
                eq.Location = Location;
                eq.Name = Name;
                eq.Type = Type;
                eq.NextServiceDate = NextServiceDate;
                eq.ServiceDate = ServiceDate;
                eq.Status = Status;
                eq.PersonToAct = PersonToAct;
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
            for (int i = 1; i <= model.PersonCentreCount; i++)
            {
                string ischeckid = $"isChecked{i}";
                var isChecked = formcollection[ischeckid];
                if (isChecked.Count > 0 && isChecked[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                { 
                    PostPersonCentred pc = new PostPersonCentred();
                    var PersonCenteredId = int.Parse(formcollection[$"PersonCentredId{i}"].FirstOrDefault());
                    var Class = int.Parse(formcollection[$"Class{i}"].FirstOrDefault());
                    var Focus = formcollection[$"Focus{i}"];
                    var ExpSupport = formcollection[$"ExpSupport{i}"].FirstOrDefault().ToString();
                    var ex = new List<PostPersonCentredFocus>();
                    for (int j = 0; j < Focus.Count; j++)
                    {
                        var newtask = new PostPersonCentredFocus();
                        newtask.BaseRecordId = int.Parse(Focus[j].ToString());
                        newtask.PersonCentredId = PersonCenteredId;
                        ex.Add(newtask);
                    }
                    pc.PersonCentredId = PersonCenteredId;
                    pc.PersonalDetailId = model.PersonalDetailId;
                    pc.Class = Class;
                    pc.ExpSupport = ExpSupport;
                    pc.Focus = ex;

                    pcentre.Add(pc);
                }
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
            CreateCarePlan model = new CreateCarePlan();
            var bases = await _baseRecord.GetBaseRecord();
            var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);
            

            //var client = await _clientService.GetClientDetail();
            model =await Get(clientId);
            model.ClassList = classItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            foreach (var item in model.ClassList)
            {
                var child = bases.Where(s => s.KeyName == item.Text).FirstOrDefault().BaseRecordId;
                var childItems = await _baseRecord.GetBaseRecordWithItems(child);

                if (item.Text.ToString() == "Individuality")
                {
                    model.Individuality = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.Individuality);
                }
                if (item.Text.ToString() == "RightsAndRespect")
                {
                    model.RightsAndRespect = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.RightsAndRespect);
                }
                if (item.Text.ToString() == "Choice")
                {
                    model.Choice = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.Choice);
                }
                if (item.Text.ToString() == "DignityAndPrivacy")
                {
                    model.DignityAndPrivacy = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.DignityAndPrivacy);
                }
                if (item.Text.ToString() == "Partnership")
                {
                    model.Partnership = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.Partnership);
                }
            }
            int i = 1;
            foreach (var item in model.GetPersonCentred)
            {
                if (i == 1)
                    model.Focus1 = item.Focus.Select(s => s.BaseRecordId).ToList();
                if (i == 2)
                    model.Focus2 = item.Focus.Select(s => s.BaseRecordId).ToList();
                if (i == 3)
                    model.Focus3 = item.Focus.Select(s => s.BaseRecordId).ToList();
                if (i == 4)
                    model.Focus4 = item.Focus.Select(s => s.BaseRecordId).ToList();
                if (i == 5)
                    model.Focus5 = item.Focus.Select(s => s.BaseRecordId).ToList();
                i++;
            }
            return View(model);
        }

        public async ValueTask<CreateCarePlan> Get(int clientId)
        {
            try
            {
                var staff = await _staffService.GetStaffs();
                var involve = await _clientService.GetClient(clientId);
                var pdetail = await _personaldetailService.Get(clientId);
                var client = await _clientService.GetClient(clientId);
                var details = await _clientService.GetClientDetail();
                var party = await _involvingparty.Get(clientId);
                var Relation = "N/A";
                if (party != null)
                {
                    Relation = party.Relationship;
                }

                var PersonalDetail = _personaldetailService.Get(clientId);
                var putEntity = new CreateCarePlan
                {
                    #region Personal Details
                    ClientId = clientId,
                    PersonalDetailId = pdetail.PersonalDetailId,
                    #endregion

                    #region Capacity
                    CapacityId = pdetail.Capacity.FirstOrDefault().CapacityId,
                    Implications = pdetail.Capacity.FirstOrDefault().Implications,
                    Pointer = pdetail.Capacity.FirstOrDefault().Pointer,
                    IndicatorList = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                    Indicator = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => s.BaseRecordId).ToList(),
                    #endregion

                    #region ConsentCare
                    CareId = pdetail.ConsentCare.FirstOrDefault().CareId,
                    CareSignature = pdetail.ConsentCare.FirstOrDefault().Signature,
                    CareDate = pdetail.ConsentCare.FirstOrDefault().Date,
                    CareName = pdetail.ConsentCare.FirstOrDefault().Name,
                    CareRelation = Relation,
                    #endregion

                    #region ConsentData
                    DataId = pdetail.ConsentData.FirstOrDefault().DataId,
                    DataSignature = pdetail.ConsentData.FirstOrDefault().Signature,
                    DataDate = pdetail.ConsentData.FirstOrDefault().Date,
                    DataName = pdetail.ConsentData.FirstOrDefault().Name,
                    DataRelation = Relation,
                    #endregion

                    #region ConsentLandLine
                    LandLineId = pdetail.ConsentLandline.FirstOrDefault().LandlineId,
                    LandLineSignature = pdetail.ConsentLandline.FirstOrDefault().Signature,
                    LandLineDate = pdetail.ConsentLandline.FirstOrDefault().Date,
                    LandLogList = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                    LandLineLogMethod = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList(),
                    LandName = pdetail.ConsentLandline.FirstOrDefault().Name,
                    LandRelation = Relation,
                    #endregion

                    #region Equipment
                    GetEquipment = pdetail.Equipment,
                    StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                    #endregion

                    #region KeyIndicators
                    AboutMe = pdetail.KeyIndicators.FirstOrDefault().AboutMe,
                    KeyId = pdetail.KeyIndicators.FirstOrDefault().KeyId,
                    FamilyRole = pdetail.KeyIndicators.FirstOrDefault().FamilyRole,
                    Debture = pdetail.KeyIndicators.FirstOrDefault().Debture,
                    LivingStatus = pdetail.KeyIndicators.FirstOrDefault().LivingStatus,
                    KeyLogList = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                    LogMethod = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList(),
                    ThingsILike = pdetail.KeyIndicators.FirstOrDefault().ThingsILike,
                    #endregion

                    #region Personal
                    PersonalId = pdetail.Personal.FirstOrDefault().PersonalId,
                    Smoking = pdetail.Personal.FirstOrDefault().Smoking,
                    DNR = pdetail.Personal.FirstOrDefault().DNR,
                    FullName = details.Where(s => s.ClientId == clientId).Select(s => s.FullName).FirstOrDefault(),
                    PreferredLanguage = client.LanguageId,
                    Gender = client.GenderId,
                    DateofBirth = client.DateOfBirth,
                    Address = client.Address,
                    PostCode = client.PostCode,
                    Telephone = client.Telephone,
                    PreferredName = client.Firstname,
                    AccessCode = client.KeySafe,
                    PreferredGender = client.ChoiceOfStaffId,
                    KeyWorker = client.Keyworker,
                    TeamLeader = client.TeamLeader,
                    Religion = pdetail.Personal.FirstOrDefault().Religion,
                    Nationality = pdetail.Personal.FirstOrDefault().Nationality,
                    #endregion

                    #region Person Centred
                    GetPersonCentred = pdetail.PersonCentred,
                    #endregion

                    #region Review
                    ReviewId = pdetail.Review.FirstOrDefault().ReviewId,
                    CP_PreDate = pdetail.Review.FirstOrDefault().CP_PreDate,
                    CP_ReviewDate = pdetail.Review.FirstOrDefault().CP_ReviewDate,
                    RA_PreDate = pdetail.Review.FirstOrDefault().RA_PreDate,
                    RA_ReviewDate = pdetail.Review.FirstOrDefault().RA_ReviewDate,
                    #endregion

                    InvolingList = involve.InvolvingParties.Select(s => new SelectListItem(s.Name, s.ClientInvolvingPartyId.ToString())).ToList(),
                    EquipmentCount = pdetail.Equipment.Count,
                    PersonCentreCount = pdetail.PersonCentred.Count,
                    ActionName = "Update",

                };
                return putEntity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                //return default;

                throw;
            }
        }
    }
}
