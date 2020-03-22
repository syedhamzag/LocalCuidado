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
        public int RotaId { get; set; }
        public int MonthIndex { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int StaffPersonalInfoId { get; set; }

        
        public virtual IList<GetStaffShiftBookingDay> Days { get; set; }
    }
}
