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
namespace AwesomeCare.Web.Controllers
{
    public class ShiftBookingController : Controller
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
        public async Task<IActionResult> Create(string month)
        {
            var model = new CreateBookingViewModel();

            var savedRota = HttpContext.Session.Get<List<GetClientRotaName>>("rotas");
            if (savedRota == null)
            {
                var rotas = await _clientRotaNameService.Get();
                model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
                HttpContext.Session.Set<List<GetClientRotaName>>("rotas", rotas);
            }
            else
            {
                model.Rotas = savedRota.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            }
           



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
        public IActionResult Create(CreateBookingViewModel model, IFormCollection formCollection)
        {
            for (int i = 1; i <= model.DaysInMonth; i++)
            {
                var dt = i.ToString("D2");
                var selectedDate = formCollection[dt];
                if (selectedDate.Count > 0)
                {

                }
            }
            return RedirectToAction("Create");
        }
   
    public async Task<IActionResult> Shifts()
        {
            var shifts = await _shiftBookingService.Get();
            return View(shifts);
        }
    }
}