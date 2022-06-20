using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ShiftBooking
{
    public class ViewShiftViewModel: GetShiftBookedByMonthYear
    {
        public ViewShiftViewModel()
        {
            WeekDays =  Enum.GetNames(typeof(DayOfWeek)).ToArray();// new string[] {  "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            Months = DateTimeFormatInfo.CurrentInfo.MonthNames.Select(s => new SelectListItem(s, s)).Where(s=>!string.IsNullOrWhiteSpace(s.Text)).ToList() ;
            Rotas = new List<SelectListItem>();
        }

        public int DaysInMonth { get; set; }
        public string[] WeekDays { get; set; }
        public string SelectedMonth { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
        public List<SelectListItem> Months { get; set; }
        public List<SelectListItem> Rotas { get; set; }
        public List<GetStaffs> StaffList { get; set; } = new List<GetStaffs>();
        public int Rota { get; set; }
        public List<SelectListItem> YesNo { get; set; } = new List<SelectListItem>{
                new SelectListItem("No","No"),
                new SelectListItem("Yes","Yes")
            };
        public List<SelectListItem> Team { get; set; }
        public string RequiresDriver { get; set; }
        public Array Ids { get; set; }
    }
}
