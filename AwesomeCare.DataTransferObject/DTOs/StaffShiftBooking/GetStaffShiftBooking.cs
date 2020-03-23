using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking
{
   public class GetStaffShiftBooking
    {
        public GetStaffShiftBooking()
        {
            Days = new List<GetStaffShiftBookingDay>();
        }
        public int StaffShiftBookingId { get; set; }
        public int ShiftBookingId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        
        public virtual IList<GetStaffShiftBookingDay> Days { get; set; }
    }
}
