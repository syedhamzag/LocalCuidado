using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.ShiftBooking
{
    public class CreateBookingViewModel
    {
        public CreateBookingViewModel()
        {
            Rotas = new List<SelectListItem>();
            WeekDays = Enum.GetNames(typeof(DayOfWeek)).ToArray();// new string[] {  "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
          //  int currentMonth = DateTime.Now.Month;
            Months = DateTimeFormatInfo.CurrentInfo.MonthNames.Select(s => new SelectListItem(s, s)).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();
        }

        public int DaysInMonth { get; set; }
        public string[] WeekDays { get; set; }
        public string SelectedMonth { get; set; }
        public List<SelectListItem> Months { get; set; }
        public List<SelectListItem> Rotas { get; set; }
        [Required]
        public int Rota { get; set; }
    }
}
