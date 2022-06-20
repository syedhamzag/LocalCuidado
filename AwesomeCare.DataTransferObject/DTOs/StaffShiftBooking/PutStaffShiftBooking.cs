using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking
{
    public class PutStaffShiftBooking
    {
        public int ShiftBookingId { get; set; }
        public int StaffShiftBookingId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
