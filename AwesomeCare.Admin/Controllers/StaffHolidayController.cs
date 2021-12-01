using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffHoliday;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffHoliday;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffHolidayController : BaseController
    {
        private IStaffHolidayService _StaffHoliday;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffHolidayController(IStaffHolidayService StaffHoliday, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _StaffHoliday = StaffHoliday;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffHoliday.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffHoliday> reports = new List<CreateStaffHoliday>();
            foreach (GetStaffHoliday item in entities)
            {
                var report = new CreateStaffHoliday();
                report.StaffHolidayId = item.StaffHolidayId;
                report.StartDate = item.StartDate;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateStaffHoliday();
            model.StaffPersonalInfoId = staffId;
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStaffHoliday model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            #region Attachment
            if (model.Attach != null)
            {
                string extention = model.StaffPersonalInfoId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffholiday";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream());
                model.Attachment = path;
            }
            else
            {
                model.Attachment = "No Image";
            }
            #endregion
            PostStaffHoliday post = new PostStaffHoliday();
            post.StaffHolidayId = model.StaffHolidayId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;
            post.AllocatedDays = model.AllocatedDays;
            post.Class = model.Class;
            post.CopyOfHandover = model.CopyOfHandover;
            post.Days = model.Days;
            post.EndDate = model.EndDate;
            post.PersonOnResponsibility = model.PersonOnResponsibility;
            post.Purpose = model.Purpose;
            post.Remark = model.Remark;
            post.StartDate = model.StartDate;
            post.YearOfService = model.YearOfService;
            post.Attachment = model.Attachment;


            var result = await _StaffHoliday.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus 
            {   IsSuccessful = result.IsSuccessStatusCode, 
                Message = result.IsSuccessStatusCode ? "StaffHoliday successfully added to staff" : "An Error Occurred"
            });
            
            return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
