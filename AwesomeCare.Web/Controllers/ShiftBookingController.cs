using System;
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

namespace AwesomeCare.Web.Controllers
{
   
    public class ShiftBookingController : BaseController
    {
        private IShiftBookingService _shiftBookingService;
        private ILogger<ShiftBookingController> _logger;
        private IClientRotaNameService _clientRotaNameService;

        public ShiftBookingController(IShiftBookingService shiftBookingService, IClientRotaNameService clientRotaNameService,
            ILogger<ShiftBookingController> logger, IFileUpload fileUpload) : base(fileUpload)
        {
            _shiftBookingService = shiftBookingService;
            _logger = logger;
            _clientRotaNameService = clientRotaNameService;
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> Create(string month, int shiftId)
        {
          
            var selectedMonth = (Array.IndexOf<string>(DateTimeFormatInfo.CurrentInfo.MonthNames, month) + 1).ToString("D2");
            if (int.TryParse(selectedMonth, out int mth) && mth < DateTime.Now.Month)
               return RedirectToActionPermanent("Shifts");

            var model = new CreateBookingViewModel();
            model.CanUserDrive = User.CanDrive();

           
            var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonth, DateTime.Now.Year.ToString());
            model.ShiftBooked = shiftBooked;

            HttpContext.Session.Set("shiftBooked", shiftBooked);

            model.ShiftBookingId = shiftId;

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            model.DaysInMonth = daysInMonth;
            if (!string.IsNullOrEmpty(month))
                model.SelectedMonth = month;
            else
                model.SelectedMonth = model.Months[DateTime.Now.Month - 1].Text;

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

            if(staffShiftBooking.Days.Count == 0)
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
            return View(shifts);
        }
    }
}