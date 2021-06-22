using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class SeizurePhysician
    {
        public int SeizurePhysicianId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int SeizureId { get; set; }

        public virtual ClientSeizure Seizure { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
