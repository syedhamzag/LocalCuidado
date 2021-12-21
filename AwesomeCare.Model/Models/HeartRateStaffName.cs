using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class HeartRateStaffName
    {
        public int HeartRateStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int HeartRateId { get; set; }

        public virtual ClientHeartRate HeartRate { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
