using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ShiftBooking
{
    public class ViewShiftViewModel
    {
        public ViewShiftViewModel()
        {
            WeekDays =  Enum.GetNames(typeof(DayOfWeek)).ToArray();// new string[] {  "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            Months = DateTimeFormatInfo.CurrentInfo.MonthNames.Select(s => new SelectListItem(s, s)).Where(s=>!string.IsNullOrWhiteSpace(s.Text)).ToList() ;
        }

        public int DaysInMonth { get; set; }
        public string[] WeekDays { get; set; }
        public string SelectedMonth { get; set; }
        public List<SelectListItem> Months { get; set; }
    }
}
