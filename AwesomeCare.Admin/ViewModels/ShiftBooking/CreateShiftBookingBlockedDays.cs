using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ShiftBooking
{
    public class CreateShiftBookingBlockedDays
    {
        public CreateShiftBookingBlockedDays()
        {
            WeekDays = Enum.GetNames(typeof(DayOfWeek)).ToArray();
            
        }

        public int DaysInMonth { get; set; }
        public string[] WeekDays { get; set; }
        public int ShiftBookingId { get; set; }
        public string SelectedMonth { get; set; }
    }
}
