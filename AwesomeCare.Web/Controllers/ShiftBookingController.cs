﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Web.Services.ClientRotaName;
using AwesomeCare.Web.Services.ShiftBooking;
using AwesomeCare.Web.ViewModels.ShiftBooking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using AwesomeCare.Web.Extensions;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using Microsoft.AspNetCore.Authorization;
using AwesomeCare.Services.Services;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using System.Globalization;
using System.Security.Claims;
using AwesomeCare.Web.Services.Staff;
using Newtonsoft.Json;

namespace AwesomeCare.Web.Controllers
{

    public class ShiftBookingController : BaseController
    {
        private IShiftBookingService _shiftBookingService;
        private ILogger<ShiftBookingController> _logger;
        private IClientRotaNameService _clientRotaNameService;
        private readonly IStaffService staffService;

        public ShiftBookingController(IShiftBookingService shiftBookingService, IClientRotaNameService clientRotaNameService,
            ILogger<ShiftBookingController> logger, IFileUpload fileUpload,
            IStaffService staffService) : base(fileUpload)
        {
            _shiftBookingService = shiftBookingService;
            _logger = logger;
            _clientRotaNameService = clientRotaNameService;
            this.staffService = staffService;
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> Create(string month, int shiftId)
        {

            var selectedMonthId = (Array.IndexOf<string>(DateTimeFormatInfo.CurrentInfo.MonthNames, month) + 1).ToString("D2");
            //if (int.TryParse(selectedMonth, out int mth) && mth < DateTime.Now.Month)
            //   return RedirectToActionPermanent("Shifts");

            var model = new CreateBookingViewModel();
            model.CanUserDrive = User.CanDrive();


            var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(shiftId, selectedMonthId, DateTime.Now.Year.ToString());
            model.ShiftBooked = shiftBooked;

            HttpContext.Session.Set("shiftBooked", shiftBooked);

            model.ShiftBookingId = shiftId;

            var monthId = int.TryParse(selectedMonthId, out int id) ? id : 0;
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, monthId);
            model.DaysInMonth = daysInMonth;
            if (!string.IsNullOrEmpty(month))
                model.SelectedMonth = month;
            else
                model.SelectedMonth = model.Months[monthId].Text;

            model.SelectedMonthId = monthId;// int.Parse(selectedMonthId);

            return View(model);
        }
        public IActionResult ChangeMonth(CreateBookingViewModel model)
        {
            return RedirectToAction("Create", new { month = model.SelectedMonth });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel model, IFormCollection formCollection)
        {
            var staffPersonalInfoId = User.StaffPersonalInfoId();

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
                return RedirectToAction("Shifts");


            var result = await _shiftBookingService.CreateBooking(staffShiftBooking);
            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = "Your booking was successful" });
                return RedirectToAction("Shifts");
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
            return View(model);
        }

        public async Task<IActionResult> Shifts()
        {
            var user = User.Identities;
            var isStaffRole = User.IsInRole("Staff");
            var shifts = await _shiftBookingService.Get();
            _logger.LogInformation($"shifts {JsonConvert.SerializeObject(shifts)}");

            var myProfile = await staffService.MyProfile();
            _logger.LogInformation($"myProfile {JsonConvert.SerializeObject(myProfile)}");

            var shiftsInTeam = shifts.Where(s => s.PublishTo == myProfile.StaffWorkTeamId).ToList();
            _logger.LogInformation($"shiftsInTeam {JsonConvert.SerializeObject(shiftsInTeam)}");
            return View(shiftsInTeam);
        }
    }
}