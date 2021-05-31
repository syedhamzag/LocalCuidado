using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking
{
   public class GetStaffShiftBookingDay
    {
        public int StaffShiftBookingDayId { get; set; }
        public int StaffShiftBookingId { get; set; }
        public string Day { get; set; }
        public string WeekDay { get; set; }
    }
}
