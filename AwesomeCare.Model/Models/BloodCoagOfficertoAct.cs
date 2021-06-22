using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class BloodCoagOfficerToAct
    {
        public int BloodCoagOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BloodRecordId { get; set; }

        public virtual ClientBloodCoagulationRecord BloodCoagulation { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
