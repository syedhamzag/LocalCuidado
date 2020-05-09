using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffRotaPeriod
    {
        public int StaffRotaPeriodId { get; set; }
        public int StaffRotaId { get; set; }
        public int ClientRotaTypeId { get; set; }
        public string ClockInTime { get; set; }
        public string ClockOutTime { get; set; }

        public virtual StaffRota StaffRota { get; set; }
        public virtual ClientRotaType ClientRotaType { get; set; }
    }
}
