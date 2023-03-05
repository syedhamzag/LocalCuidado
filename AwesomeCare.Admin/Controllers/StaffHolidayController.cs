using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffHoliday;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffHoliday;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private ISetupStaffHolidayService _setupStaff;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffHolidayController(IStaffHolidayService StaffHoliday, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord, ISetupStaffHolidayService setupStaff) : base(fileUpload)
        {
            _StaffHoliday = StaffHoliday;
            _staffService = staffService;
            _baseRecord = baseRecord;
            _setupStaff = setupStaff;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _StaffHoliday.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffHoliday> reports = new List<CreateStaffHoliday>();
            foreach (GetStaffHoliday item in entities)
            {
                var Adays = Math.Round(item.AllocatedDays,0);
                int Allocated = int.Parse(Adays.ToString());
                var setup = await _setupStaff.Get();
                var days = setup.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId && s.TypeOfHoliday == item.Class).FirstOrDefault().NumberOfDays;
                var report = new CreateStaffHoliday();
                report.StaffHolidayId = item.StaffHolidayId;
                report.StartDate = item.StartDate;
                report.EndDate = item.EndDate;
                report.AllocatedDays = item.AllocatedDays;
                report.Balance = ( Allocated - days);
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                report.ClassName = _baseRecord.GetBaseRecordItemById(item.Class).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateStaffHoliday();
            var setup = await _setupStaff.Get();
            model.SetupStaffHoliday = setup.Where(s => s.StaffPersonalInfoId == staffId).Select(s=> new SelectListItem(_baseRecord.GetBaseRecordItemById(s.TypeOfHoliday).Result.ValueName, s.SetupHolidayId.ToString())).ToList();
            model.StaffPersonalInfoId = staffId;
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            var holidays = await _StaffHoliday.Get();
            var holiday = holidays.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault();
            if (holiday != null)
            {
                model.AllocatedDays = holiday.AllocatedDays;
                model.Attachment = holiday.Attachment;
                model.Class = holiday.Class;
                model.CopyOfHandover = holiday.CopyOfHandover;
                model.Days = holiday.Days;
                model.EndDate = holiday.EndDate;
                model.PersonOnResponsibility = holiday.PersonOnResponsibility;
                model.Purpose = holiday.Purpose;
                model.Remark = holiday.Remark;
                model.StartDate = holiday.StartDate;
                model.YearOfService = holiday.YearOfService;
                model.StaffPersonalInfoId = holiday.StaffPersonalInfoId;
                model.StaffHolidayId = holiday.StaffHolidayId;
                model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            }
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
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
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
        public async Task<IActionResult> Edit(int staffHolidayId)
        {
            var model = await _StaffHoliday.Get(staffHolidayId);
            var setup = await _setupStaff.Get();

            var putEntity = new CreateStaffHoliday
            {
            StaffHolidayId = model.StaffHolidayId,
            StaffPersonalInfoId = model.StaffPersonalInfoId,
            AllocatedDays = model.AllocatedDays,
            Class = model.Class,
            CopyOfHandover = model.CopyOfHandover,
            Days = model.Days,
            EndDate = model.EndDate,
            PersonOnResponsibility = model.PersonOnResponsibility,
            Purpose = model.Purpose,
            Remark = model.Remark,
            StartDate = model.StartDate,
            YearOfService = model.YearOfService,
            Attachment = model.Attachment,
            SetupStaffHoliday = setup.Where(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId).Select(s => new SelectListItem(_baseRecord.GetBaseRecordItemById(s.TypeOfHoliday).Result.ValueName, s.SetupHolidayId.ToString())).ToList()
        };
            return View(putEntity);
        }
        public async Task<IActionResult> View(int staffHolidayId)
        {
            var model = await _StaffHoliday.Get(staffHolidayId);
            var setup = await _setupStaff.Get();

            var putEntity = new CreateStaffHoliday
            {
                StaffHolidayId = model.StaffHolidayId,
                StaffPersonalInfoId = model.StaffPersonalInfoId,
                AllocatedDays = model.AllocatedDays,
                Class = model.Class,
                CopyOfHandover = model.CopyOfHandover,
                Days = model.Days,
                EndDate = model.EndDate,
                PersonOnResponsibility = model.PersonOnResponsibility,
                Purpose = model.Purpose,
                Remark = model.Remark,
                StartDate = model.StartDate,
                YearOfService = model.YearOfService,
                Attachment = model.Attachment,
                SetupStaffHoliday = setup.Where(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId).Select(s => new SelectListItem(_baseRecord.GetBaseRecordItemById(s.TypeOfHoliday).Result.ValueName, s.SetupHolidayId.ToString())).ToList()
            };
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStaffHoliday model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #region Evidence
            if (model.Attach != null)
            {
                string extention = model.StaffPersonalInfoId + System.IO.Path.GetExtension(model.Attach.FileName);
                string folder = "staffholiday";
                string filename = string.Concat(folder, "_Attachment_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
                model.Attachment = path;

            }
            else
            {
                model.Attachment = model.Attachment;
            }
            #endregion

            var put = new PutStaffHoliday();
            put.StaffHolidayId = model.StaffHolidayId;
            put.StaffPersonalInfoId = model.StaffPersonalInfoId;
            put.AllocatedDays = model.AllocatedDays;
            put.Class = model.Class;
            put.CopyOfHandover = model.CopyOfHandover;
            put.Days = model.Days;
            put.EndDate = model.EndDate;
            put.PersonOnResponsibility = model.PersonOnResponsibility;
            put.Purpose = model.Purpose;
            put.Remark = model.Remark;
            put.StartDate = model.StartDate;
            put.YearOfService = model.YearOfService;
            put.Attachment = model.Attachment;

            var entity = await _StaffHoliday.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }

        [HttpGet]
        public JsonResult GetDays(int setupId, int holidayId)
        {
            var list = new List<int>();
            var setup = _setupStaff.Get(setupId);
            if(setup.Result != null)
                list.Add(setup.Result.NumberOfDays);
            var holidays = _StaffHoliday.Get();

            if (holidays.Result != null)
            {
                var holiday = holidays.Result.Where(s=>s.StaffPersonalInfoId==setup.Result.StaffPersonalInfoId && s.Class==setup.Result.TypeOfHoliday).ToList();
                if (holiday.Count > 0)
                { 
                    foreach (var item in holiday)
                    {
                        var days = Math.Round(item.Days, 0);
                        int day = int.Parse(days.ToString());
                        list.Add(day);
                    }
                }
                else
                    list.Add(0);
            }
            
            return Json(list);

        }
        [HttpGet]
        public JsonResult GetHoliday(DateTime sdate, DateTime edate)
        {
            int days = 0;
            int difference = (edate.Day - sdate.Day);
            for (int i = 0; i <= difference; i++)
            {
                string day = sdate.AddDays(i).DayOfWeek.ToString();
                if(day != "Sunday" && day != "Saturday")
                    days++;
            }
            return Json(days);

        }
    }
}
