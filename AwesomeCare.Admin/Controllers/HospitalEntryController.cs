using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.HospitalEntry;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Untowards;
using AwesomeCare.DataTransferObject.DTOs.HospitalEntry;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class HospitalEntryController : BaseController
    {
        private IHospitalEntryService _hospitalService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private IClientService _clientService;
        public HospitalEntryController(IFileUpload fileUpload, IHospitalEntryService hospitalService, IBaseRecordService baseService,
            IStaffService staffService, IClientService clientService) :base(fileUpload)
        {
            _hospitalService = hospitalService;
            _baseService = baseService;
            _staffService = staffService;
            _clientService = clientService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _hospitalService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateHospitalEntry> reports = new List<CreateHospitalEntry>();
            foreach (GetHospitalEntry item in entities)
            {
                var report = new CreateHospitalEntry();
                report.HospitalEntryId = item.HospitalEntryId;
                report.LastDateofAdmission = item.LastDateofAdmission;
                report.Date = item.Date;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                report.StatusName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
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
        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateHospitalEntry();
            var staffs = await _staffService.GetStaffs();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateHospitalEntry model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            PostHospitalEntry post = new PostHospitalEntry();

            #region Evidence
            if (model.Attach != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "hospitalentry";
                string filename = string.Concat(folder, "_Attach_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion

            post.ClientId = model.ClientId;
            post.Reference = model.Reference;
            post.Date = model.Date;
            post.StaffInvolved = model.StaffInvolved.Select(o => new PostHospitalEntryStaffInvolved { StaffPersonalInfoId = o, HospitalEntryId = model.HospitalEntryId }).ToList();
            post.PersonToTakeAction = model.PersonToTakeAction.Select(o => new PostHospitalEntryPersonToTakeAction { StaffPersonalInfoId = o, HospitalEntryId = model.HospitalEntryId }).ToList();
            post.Remark = model.Remark;
            post.Status = model.Status;
            post.Attachment = model.Attachment;
            post.CauseofAdmission = model.CauseofAdmission;
            post.ClientId = model.ClientId;
            post.ConditionOfAdmission = model.ConditionOfAdmission;
            post.Date = model.Date;
            post.HospitalEntryId = model.HospitalEntryId;
            post.IsFamilyInformed = model.IsFamilyInformed;
            post.IsHomeCleaned = model.IsHomeCleaned;
            post.LastDateofAdmission = model.LastDateofAdmission;
            post.MeansOfTransport = model.MeansOfTransport;
            post.NameParamedicStaff = model.NameParamedicStaff;
            post.ParamicStaffTeamNo = model.ParamicStaffTeamNo;
            post.PossibleDateReturn = model.PossibleDateReturn;
            post.PurposeofAdmission = model.PurposeofAdmission;
            post.Time = model.Time;
            post.URLLINK = model.URLLINK;
            
            

            var result = await _hospitalService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Log Audit successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }

        public async Task<CreateHospitalEntry> GetDownload(int Id)
        {
            var i = await _hospitalService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateHospitalEntry
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                Remark = i.Remark,
                PurposeofAdmission = i.PurposeofAdmission,
                Attachment = i.Attachment,
                CauseofAdmission = i.CauseofAdmission,
                Date = i.Date,
                Time = i.Time,
                LastDateofAdmission = i.LastDateofAdmission,
                PossibleDateReturn = i.PossibleDateReturn,
                URLLINK = i.URLLINK,
                ConditionOfAdmissionName = _baseService.GetBaseRecordItemById(i.ConditionOfAdmission).Result.ValueName,
                StatusName = _baseService.GetBaseRecordItemById(i.Status).Result.ValueName,
                IsFamilyInformedName = _baseService.GetBaseRecordItemById(i.IsFamilyInformed).Result.ValueName,
                IsHomeCleanedName = _baseService.GetBaseRecordItemById(i.IsHomeCleaned).Result.ValueName,
                NameParamedicStaffName = _baseService.GetBaseRecordItemById(i.NameParamedicStaff).Result.ValueName,
                ParamicStaffTeamNoName = _baseService.GetBaseRecordItemById(i.ParamicStaffTeamNo).Result.ValueName,
            };
            foreach (var item in i.PersonToTakeAction.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.PersonToTakeActionName))
                    putEntity.PersonToTakeActionName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.PersonToTakeActionName = putEntity.PersonToTakeActionName + ", " + staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
            foreach (var item in i.StaffInvolved.Select(s => s.StaffPersonalInfoId).ToList())
            {
                if (string.IsNullOrWhiteSpace(putEntity.StaffInvolvedName))
                    putEntity.StaffInvolvedName = staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
                else
                    putEntity.StaffInvolvedName = putEntity.StaffInvolvedName + ", " + staff.Where(s => s.StaffPersonalInfoId == item).SingleOrDefault().Fullname;
            }
            return putEntity;
        }
    }
}
