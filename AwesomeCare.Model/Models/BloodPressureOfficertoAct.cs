using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class BloodPressureOfficerToAct
    {
        public int BloodPressureOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BloodPressureId { get; set; }

        public virtual ClientBloodPressure BloodPressure { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
