using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBooking
{
   public class DeleteStaffShiftBookingDay
    {
        public DeleteStaffShiftBookingDay()
        {
            StaffShiftBookingDayId = new List<int>();
        }

        public List<int> StaffShiftBookingDayId { get; set; }
    }
}
