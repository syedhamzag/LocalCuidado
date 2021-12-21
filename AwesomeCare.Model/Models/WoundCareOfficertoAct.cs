using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class WoundCareOfficerToAct
    {
        public int WoundCareOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int WoundCareId { get; set; }

        public virtual ClientWoundCare WoundCare { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
