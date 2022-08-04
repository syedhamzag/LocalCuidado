using AutoMapper;
using AwesomeCare.Admin.Services.OfficeAttendance;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.OfficeAttendance;
using AwesomeCare.DataTransferObject.DTOs.OfficeAttendance;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Admin.Controllers
{
    public class OfficeAttendanceController : BaseController
    {
        private readonly ILogger<OfficeAttendanceController> _logger;
        private readonly IOfficeAttendanceService _officeAttendanceService;
        private readonly IStaffService _staffService;

        public OfficeAttendanceController(ILogger<OfficeAttendanceController> logger,
            IFileUpload fileUpload, IStaffService staffService,
            IOfficeAttendanceService officeAttendanceService) : base(fileUpload)
        {
            _logger = logger;
            _officeAttendanceService = officeAttendanceService;
            _staffService = staffService;
        }

        public IActionResult Index()
        {
            List<GetAttendance> attendance = new List<GetAttendance>();
            return View(attendance);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string startDate, string stopDate)
        {
            var attendance = await _officeAttendanceService.GetByDate(startDate,stopDate);
            return View(attendance);
        }
        public async Task<IActionResult> Edit(int attendanceId)
        {
            var staffs = await _staffService.GetStaffs();
            var attendance = await _officeAttendanceService.Get(attendanceId);
            var entity = Create(attendance);
            entity.StaffList = staffs.Select(s=> new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateAttendance model)
        {
            if (!ModelState.IsValid)
            {
                var staffs = await _staffService.GetStaffs();
                model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            var entity = Put(model);
            var result = await _officeAttendanceService.Put(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public CreateAttendance Create(GetAttendance model)
        {
            var entity = new CreateAttendance() 
            {
                AttendanceId = model.AttendanceId,
                ClockDiff = model.ClockDiff,    
                ClockIn = model.ClockIn,
                ClockInAddress = model.ClockInAddress,
                ClockOut = model.ClockOut,  
                ClockOutAddress = model.ClockOutAddress,
                ClockInDistance = model.ClockInDistance,
                ClockOutDistance = model.ClockOutDistance,
                ClockInMethod = model.ClockInMethod,
                ClockOutMethod = model.ClockOutMethod,
                Date = model.Date,
                JobTitle = model.JobTitle,
                Latitude = model.Latitude,
                Location = model.Location,
                Longitude = model.Longitude,
                Remark = model.Remark,
                Staff   = model.Staff,
                StartTime = model.StartTime,
                StopTime = model.StopTime,
            };

            return entity;
        }
        public PutAttendance Put(CreateAttendance model)
        {
            var entity = new PutAttendance()
            {
                AttendanceId = model.AttendanceId,
                ClockDiff = model.ClockDiff,
                ClockIn = model.ClockIn,
                ClockInAddress = model.ClockInAddress,
                ClockOut = model.ClockOut,
                ClockOutAddress = model.ClockOutAddress,
                ClockInDistance = model.ClockInDistance,
                ClockOutDistance = model.ClockOutDistance,
                ClockInMethod = model.ClockInMethod,
                ClockOutMethod = model.ClockOutMethod,
                Date = model.Date,
                JobTitle = model.JobTitle,
                Latitude = model.Latitude,
                Location = model.Location,
                Longitude = model.Longitude,
                Remark = model.Remark,
                Staff = model.Staff,
                StartTime = model.StartTime,
                StopTime = model.StopTime,
            };

            return entity;
        }
    }
}
