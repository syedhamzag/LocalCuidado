using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AwesomeCare.Admin.Extensions;
using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.Admin.Services.ShiftBooking;
using AwesomeCare.Admin.Models;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.Services.StaffWorkTeam;
using AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    // [Route("[Controller]")]
    public class ShiftBookingController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<ShiftBookingController> _logger;
        private IShiftBookingService _shiftBookingService;
        private IStaffWorkTeamService _staffWorkTeamService;
        private IClientRotaNameService _clientRotaNameService;

        public ShiftBookingController(IStaffService staffService, IClientRotaNameService clientRotaNameService, IStaffWorkTeamService staffWorkTeamService, IFileUpload fileUpload, IShiftBookingService shiftBookingService, ILogger<ShiftBookingController> logger) : base(fileUpload)
        {
            _staffService = staffService;
            _logger = logger;
            _shiftBookingService = shiftBookingService;
            _staffWorkTeamService = staffWorkTeamService;
            _clientRotaNameService = clientRotaNameService;
        }
        public async Task<IActionResult> Index()
        {
            var entities = await _shiftBookingService.Get();

            return View(entities);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateShiftBooking();
            var staffs = await _staffService.GetStaffs();
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            var teams = await _staffWorkTeamService.Get();
            model.WorkTeams = teams.Select(s => new SelectListItem(s.WorkTeam, s.StaffWorkTeamId.ToString())).ToList();

            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();

            HttpContext.Session.Set<List<GetStaffs>>("staffs", staffs);
            HttpContext.Session.Set<List<GetStaffWorkTeam>>("workteams", teams);
            HttpContext.Session.Set<List<GetClientRotaName>>("rotas", rotas);


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShiftBooking model)
        {
            //ensure to check to if the selected rota does not exit for that month

            model.DriverRequired = string.Equals(model.RequiresDriver, "yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            if (!ModelState.IsValid)
            {
                model.Staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs").Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.WorkTeams = HttpContext.Session.Get<List<GetStaffWorkTeam>>("workteams").Select(s => new SelectListItem(s.WorkTeam, s.StaffWorkTeamId.ToString())).ToList();
                model.Rotas = HttpContext.Session.Get<List<GetClientRotaName>>("rotas").Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();

                return View(ModelState);
            }

            var postShiftBooking = Mapper.Map<PostShiftBooking>(model);
            var result = await _shiftBookingService.Post(postShiftBooking);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Shift Booking successfully created" : "An Error Occurred" });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var entity = await _shiftBookingService.Get(id);
            return View(entity);
        }


        [Route("/ShiftBooking/View-Shift", Name = "ViewShift")]
        public async Task<IActionResult> ViewShift()
        {
            var model = new ViewShiftViewModel();
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            model.DaysInMonth = daysInMonth;
            string selectedMonth = model.Months[DateTime.Now.Month - 1].Text;
            model.SelectedMonth = selectedMonth;

            var months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var monthId = Array.IndexOf(months, selectedMonth) + 1;


            var staffShiftBookings = await _shiftBookingService.GetStaffShiftBookingsByMonth(monthId);
            model.Staffs = staffShiftBookings.Staffs;
            return View(model);
        }


        [Route("/ShiftBooking/View-Shift", Name = "ViewShift")]
        [HttpPost]
        public async Task<IActionResult> ViewShift(ViewShiftViewModel model)
        {
            var months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var monthId = Array.IndexOf(months, model.SelectedMonth) + 1;
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, monthId);
            model.DaysInMonth = daysInMonth;

            var staffShiftBookings = await _shiftBookingService.GetStaffShiftBookingsByMonth(monthId);
            if (staffShiftBookings != null)
                model.Staffs = staffShiftBookings.Staffs;
            return View(model);
        }

        public async Task<IActionResult> CreateStaffShift()
        {
            var model = new CreateStaffShiftViewModel();
            List<GetStaffs> staffs;

            var currentMonthName = DateTime.Now.ToString("MMMM");
            string selectedMonth = model.Months.FirstOrDefault(m=> m.Text == currentMonthName)?.Text;
            model.SelectedMonth = selectedMonth;

            var selectedMonthId = (Array.IndexOf<string>(DateTimeFormatInfo.CurrentInfo.MonthNames, selectedMonth) + 1).ToString("D2");
            var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonthId, DateTime.Now.Year.ToString());
            if (shiftBooked == null)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "No shift scheduled for the selected month" });
                staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");

                model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                return View(model);
            }
            staffs = await _staffService.GetStaffs();

            model.ShiftBookingId = shiftBooked.ShiftBookingId;

            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ShiftBooked = shiftBooked;

            HttpContext.Session.Set("shiftBooked", shiftBooked);
            HttpContext.Session.Set("staffs", staffs);

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(selectedMonthId));
            model.DaysInMonth = daysInMonth;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchShiftBooking(CreateStaffShiftViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<GetStaffs> staffs;
            var selectedMonthId = (Array.IndexOf<string>(DateTimeFormatInfo.CurrentInfo.MonthNames, model.SelectedMonth) + 1).ToString("D2");
            var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonthId, DateTime.Now.Year.ToString());
            if (shiftBooked == null)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "No shift scheduled for the selected month" });
                staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");

                model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                return View("CreateStaffShift", model);
            }
            staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");

            model.ShiftBooked = shiftBooked;
            model.ShiftBookingId = shiftBooked.ShiftBookingId;
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            model.Staff = staffs.FirstOrDefault(s => s.StaffPersonalInfoId.ToString() == model.SelectedStaff);

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(selectedMonthId));
            model.DaysInMonth = daysInMonth;

            return View("CreateStaffShift", model);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShift(CreateStaffShiftViewModel model, IFormCollection formCollection)
        {
            var staffPersonalInfoId = model.SelectedStaff;

            var staffShiftBooking = new PostStaffShiftBooking()
            {
                ShiftBookingId = model.ShiftBookingId,
                StaffPersonalInfoId = int.Parse(staffPersonalInfoId)
            };
            //PostStaffShiftBookingDay
            for (int i = 1; i <= model.DaysInMonth; i++)
            {
                var dt = i.ToString("D2");
                var selectedDate = formCollection[dt];
                string dayweek = $"{dt}_day";
                var selectedDay = formCollection[dayweek];
                if (selectedDate.Count > 0 && selectedDay.Count > 0)
                {
                    staffShiftBooking.Days.Add(new PostStaffShiftBookingDay
                    {
                        Day = dt,
                        WeekDay = selectedDay.FirstOrDefault()
                    }); ;
                }
            }

            if (staffShiftBooking.Days.Count == 0)
                return RedirectToAction("CreateStaffShift");

            var kk = JsonConvert.SerializeObject(staffShiftBooking);
            var result = await _shiftBookingService.CreateBooking(staffShiftBooking);
            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = "Your booking was successful" });
                return RedirectToAction("ViewShift");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = content });
            }
            else
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An error occurred" });
            }
            var shiftBooked = HttpContext.Session.Get<GetShiftBookedByMonthYear>("shiftBooked");
            model.ShiftBooked = shiftBooked;

            var staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Staff = staffs.FirstOrDefault(s => s.StaffPersonalInfoId.ToString() == model.SelectedStaff);

            return View("CreateStaffShift", model);
        }


        //public async Task<IActionResult> CreatShift()
        //{
        //    var model = new CreateStaffShiftViewModel();
        //    string selectedMonth = model.Months[DateTime.Now.Month - 1].Text;
        //    model.SelectedMonth = selectedMonth;

        //    var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonth, DateTime.Now.Year.ToString());
        //    var staffs = await _staffService.GetStaffs();

        //    model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
        //    model.ShiftBooked = shiftBooked;

        //    HttpContext.Session.Set("shiftBooked", shiftBooked);
        //    HttpContext.Session.Set("staffs", staffs);

        //    var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        //    model.DaysInMonth = daysInMonth;
        //    return View(model);
        //}

    }
}