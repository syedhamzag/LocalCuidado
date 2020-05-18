using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking
{
   public class PostStaffShiftBooking
    {
        public PostStaffShiftBooking()
        {
            Days = new List<PostStaffShiftBookingDay>();
        }
        public int ShiftBookingId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public List<PostStaffShiftBookingDay> Days { get; set; }
    }
}
