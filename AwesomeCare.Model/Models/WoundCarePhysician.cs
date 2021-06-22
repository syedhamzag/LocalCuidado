using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class WoundCarePhysician
    {
        public int WoundCarePhysicianId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int WoundCareId { get; set; }

        public virtual ClientWoundCare WoundCare { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
