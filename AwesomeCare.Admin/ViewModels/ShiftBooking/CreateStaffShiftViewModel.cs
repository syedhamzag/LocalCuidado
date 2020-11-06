using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ShiftBooking
{
    public class CreateStaffShiftViewModel
    {
        public CreateStaffShiftViewModel()
        {
            WeekDays = Enum.GetNames(typeof(DayOfWeek)).ToArray();
            Staffs = new List<SelectListItem>();
            var monthArray = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var currentMonthId = DateTime.Now.Month;
            var currentMonthName = DateTime.Now.ToString("MMMM");
            var currentMonthIndex = Array.IndexOf(monthArray, currentMonthName);
            Months = DateTimeFormatInfo.CurrentInfo.MonthNames.Where(i=> Array.IndexOf(monthArray,i) >= currentMonthIndex).Select(s => new SelectListItem(s, s)).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();
            Rotas = new List<SelectListItem>();
        }
        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> Rotas { get; set; }

        [Required(ErrorMessage = "please select staff")]
        [Display(Name = "Staff")]
        public string SelectedStaff { get; set; }
        public List<SelectListItem> Months { get; set; }
        [Required(ErrorMessage = "please select month")]
        public string SelectedMonth { get; set; }
        public int DaysInMonth { get; set; }
        public string[] WeekDays { get; set; }
        public int ShiftBookingId { get; set; }
        public GetShiftBookedByMonthYear ShiftBooked { get; set; }
        public bool CanUserDrive { get; set; }
        public GetStaffs Staff { get; set; }
        [Required]
        public int Rota { get; set; }
    }
}
