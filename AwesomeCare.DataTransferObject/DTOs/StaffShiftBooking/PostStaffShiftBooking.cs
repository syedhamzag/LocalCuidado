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
        public int RotaId { get; set; }
        public int MonthIndex { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public List<PostStaffShiftBookingDay> Days { get; set; }
    }
}
