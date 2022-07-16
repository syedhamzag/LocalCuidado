using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking
{
    public class PutStaffShiftBooking
    {
        public PutStaffShiftBooking()
        {
            Days = new List<PutStaffShiftBookingDay>();
        }
        public int StaffShiftBookingId { get; set; }
        public int ShiftBookingId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public List<PutStaffShiftBookingDay> Days { get; set; }
    }
}
