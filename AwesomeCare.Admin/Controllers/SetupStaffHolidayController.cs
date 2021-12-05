using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffHoliday;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.SetupStaffHoliday;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class SetupStaffHolidayController : BaseController
    {
        private ISetupStaffHolidayService _StaffHoliday;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public SetupStaffHolidayController(ISetupStaffHolidayService StaffHoliday, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _StaffHoliday = StaffHoliday;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffHoliday.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateSetupStaffHoliday> reports = new List<CreateSetupStaffHoliday>();
            foreach (GetSetupStaffHoliday item in entities)
            {
                var report = new CreateSetupStaffHoliday();
                report.SetupHolidayId = item.SetupHolidayId;
                report.YearEndPeriodStartDate = item.YearEndPeriodStartDate;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index()
        {
            var model = new CreateSetupStaffHoliday();
            var staff = await _staffService.GetStaffs();
            model.StaffList = staff.Select(s=> new SelectListItem {Text = s.Fullname, Value = s.StaffPersonalInfoId.ToString() }).ToList();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSetupStaffHoliday model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.StaffPersonalInfoId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "setupstaffholiday";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion
            PostSetupStaffHoliday post = new PostSetupStaffHoliday();
            post.SetupHolidayId = model.SetupHolidayId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.Remark = model.Remark;
            post.Attachment = model.Attachment;
            post.IncrementalDailyHolidayByHrs = model.IncrementalDailyHolidayByHrs;
            post.HoursSoFar = model.HoursSoFar;
            post.NumberOfDays = model.NumberOfDays;
            post.TypeOfHoliday = model.TypeOfHoliday;
            post.YearEndPeriodStartDate = model.YearEndPeriodStartDate;
            post.YearOfEmployment = model.YearOfEmployment;
            


            var result = await _StaffHoliday.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "SetupStaffHoliday successfully added to staff" : "An Error Occurred"
            });

            return RedirectToAction("Reports");
        }
    }
}
