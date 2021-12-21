using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class BloodCoagPhysician
    {
        public int BloodCoagPhysicianId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BloodRecordId { get; set; }

        public virtual ClientBloodCoagulationRecord BloodCoagulation { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
