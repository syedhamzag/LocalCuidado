using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffShiftBooking
    {
        public StaffShiftBooking()
        {
            Days = new HashSet<StaffShiftBookingDay>();
        }
        public int StaffShiftBookingId { get; set; }
        public int RotaId { get; set; }
        public int MonthIndex { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public virtual Rota Rota { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
        public virtual ICollection<StaffShiftBookingDay> Days { get; set; }
    }
}
