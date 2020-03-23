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

namespace AwesomeCare.Web.Controllers
{
    public class ShiftBookingController : BaseController
    {
        private IShiftBookingService _shiftBookingService;
        private ILogger<ShiftBookingController> _logger;
        private IClientRotaNameService _clientRotaNameService;

        public ShiftBookingController(IShiftBookingService shiftBookingService, IClientRotaNameService clientRotaNameService, ILogger<ShiftBookingController> logger)
        {
            _shiftBookingService = shiftBookingService;
            _logger = logger;
            _clientRotaNameService = clientRotaNameService;
        }
        public IActionResult Create(string month, int shiftId)
        {
            var model = new CreateBookingViewModel();

            //var savedRota = HttpContext.Session.Get<List<GetClientRotaName>>("rotas");
            //if (savedRota == null)
            //{
            //    var rotas = await _clientRotaNameService.Get();
            //    model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            //    HttpContext.Session.Set<List<GetClientRotaName>>("rotas", rotas);
            //}
            //else
            //{
            //    model.Rotas = savedRota.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            //}


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
            var staffShiftBooking = new PostStaffShiftBooking()
            {
                ShiftBookingId = model.ShiftBookingId,
                StaffPersonalInfoId = 1
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

            var result =await _shiftBookingService.CreateBooking(staffShiftBooking);
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
            return View(model);
        }

        public async Task<IActionResult> Shifts()
        {
            var shifts = await _shiftBookingService.Get();
            return View(shifts);
        }
    }
}