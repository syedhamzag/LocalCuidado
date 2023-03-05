using AwesomeCare.Admin.Services;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Untowards;
using AwesomeCare.DataTransferObject.DTOs.HospitalExit;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AwesomeCare.Admin.Services.HospitalExit;
using System.IO;

namespace AwesomeCare.Admin.Controllers
{
    public class HospitalExitController : BaseController
    {
        private IHospitalExitServices _hospitalService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;

        public HospitalExitController(IHospitalExitServices hospitalService, IFileUpload fileUpload,
            IClientService clientService, IStaffService staffService, IBaseRecordService baseService) : base(fileUpload)

        {
            _hospitalService = hospitalService;
            _clientService = clientService;
            _baseService = baseService;
            _staffService = staffService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _hospitalService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateHospitalExit> reports = new List<CreateHospitalExit>();
            foreach (GetHospitalExit item in entities)
            {
                var report = new CreateHospitalExit();
                report.HospitalExitId = item.HospitalExitId;
                report.Reference = item.Reference;
                report.Date = item.Date;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }
        public async Task<IActionResult> Index(int? clientId)
        {
            var model = new CreateHospitalExit();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateHospitalExit model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostHospitalExit post = new PostHospitalExit();

            post.ClientId = model.ClientId;
            post.Reference = model.Reference;
            post.Date = model.Date;
            post.OfficerToTakeAction = model.OfficerToTakeAction.Select(o => new PostHospitalExitOfficerToTakeAction { StaffPersonalInfoId = o, HospitalExitId = model.HospitalExitId }).ToList();
            post.Remark = model.Remark;
            post.Status = model.Status;
            post.IsCarePlanUpdated = model.IsCarePlanUpdated;
            post.IsGrosSriesAvaible = model.IsGrosSriesAvaible;
            post.IsHomeCleaned = model.IsHomeCleaned;
            post.isLittleCashAvailableForServiceUser = model.isLittleCashAvailableForServiceUser;
            post.IsMedicationAvaialable = model.IsMedicationAvaialable;
            post.isRotaTeamInformed = model.isRotaTeamInformed;
            post.IsServiceUseronRota = model.IsServiceUseronRota;
            post.ModeOfMeansOfTrasportBackHome = model.ModeOfMeansOfTrasportBackHome;
            post.NumberOfStaffRequiredOnDischarge = model.NumberOfStaffRequiredOnDischarge;
            post.PurposeofAdmission = model.PurposeofAdmission;
            post.ReablementRequired = model.ReablementRequired;
            post.Time = model.Time;
            post.URLLINK = model.URLLINK;
            post.WhichSupportIsNeeded = model.WhichSupportIsNeeded;
            post.AreContinentProductNeedAndAvailable = model.AreContinentProductNeedAndAvailable;
            post.AreEqipmentNeededAvailable = model.AreEqipmentNeededAvailable;
            post.AreLocalSupportOrProgramNeeded = model.AreLocalSupportOrProgramNeeded;
            post.AreStaffTrainnedOnEquipmentNeeded = model.AreStaffTrainnedOnEquipmentNeeded;
            post.ConditionOnDischarge = model.ConditionOnDischarge;
            post.ContactIncaseOfReAdmission = model.ContactIncaseOfReAdmission;
            post.HospitalExitId = model.HospitalExitId;

            var json = JsonConvert.SerializeObject(post);
            var result = await _hospitalService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode != false ? "New Hospital Exit successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }

        public async Task<IActionResult> Download(int Id)
        {
            var entity = await GetDownload(Id);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);

        }
        public async Task<CreateHospitalExit> GetDownload(int Id)
        {
            var i = await _hospitalService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateHospitalExit
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                Remark = i.Remark,
                PurposeofAdmission = i.PurposeofAdmission,
                ContactIncaseOfReAdmission = i.ContactIncaseOfReAdmission,
                Date = i.Date,
                Time = i.Time,
                Reference = i.Reference,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
                WhichSupportIsNeeded = i.WhichSupportIsNeeded,
                URLLINK = i.URLLINK,
                ConditionOnDischargeName = _baseService.GetBaseRecordItemById(i.ConditionOnDischarge).Result.ValueName,
                NumberOfStaffRequiredOnDischargeName = _baseService.GetBaseRecordItemById(i.NumberOfStaffRequiredOnDischarge).Result.ValueName,
                IsGrosSriesAvaibleName = _baseService.GetBaseRecordItemById(i.IsGrosSriesAvaible).Result.ValueName,
                IsHomeCleanedName = _baseService.GetBaseRecordItemById(i.IsHomeCleaned).Result.ValueName,
                IsMedicationAvaialableName = _baseService.GetBaseRecordItemById(i.IsMedicationAvaialable).Result.ValueName,
                IsServiceUseronRotaName = _baseService.GetBaseRecordItemById(i.IsServiceUseronRota).Result.ValueName,
                isRotaTeamInformedName = _baseService.GetBaseRecordItemById(i.isRotaTeamInformed).Result.ValueName,
                isLittleCashAvailableForServiceUserName = _baseService.GetBaseRecordItemById(i.isLittleCashAvailableForServiceUser).Result.ValueName,
                ModeOfMeansOfTrasportBackHomeName = _baseService.GetBaseRecordItemById(i.ModeOfMeansOfTrasportBackHome).Result.ValueName,
                AreEqipmentNeededAvailableName = _baseService.GetBaseRecordItemById(i.AreEqipmentNeededAvailable).Result.ValueName,
                AreStaffTrainnedOnEquipmentNeededName = _baseService.GetBaseRecordItemById(i.AreStaffTrainnedOnEquipmentNeeded).Result.ValueName,
                AreContinentProductNeedAndAvailableName = _baseService.GetBaseRecordItemById(i.AreContinentProductNeedAndAvailable).Result.ValueName,
                AreLocalSupportOrProgramNeededName = _baseService.GetBaseRecordItemById(i.AreLocalSupportOrProgramNeeded).Result.ValueName,
                IsCarePlanUpdatedName = _baseService.GetBaseRecordItemById(i.IsCarePlanUpdated).Result.ValueName,
                ReablementRequiredName = _baseService.GetBaseRecordItemById(i.ReablementRequired).Result.ValueName,
            };
            foreach (var item in i.OfficerToTakeAction.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.OfficerToTakeActionName))
                    putEntity.OfficerToTakeActionName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.OfficerToTakeActionName = putEntity.OfficerToTakeActionName + ", " + staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
            return putEntity;
        }
    }
}
