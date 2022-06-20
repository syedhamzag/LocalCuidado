using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffShiftBookingDay
    {
        public int StaffShiftBookingDayId { get; set; }
        public int StaffShiftBookingId { get; set; }
        public string Day { get; set; }
        public string WeekDay { get; set; }
        public DateTime Date { get; set; }
        public virtual StaffShiftBooking ShiftBooking { get; set; }
    }
}
