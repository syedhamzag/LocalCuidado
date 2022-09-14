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
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
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

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Log Audit successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }
    }
}
