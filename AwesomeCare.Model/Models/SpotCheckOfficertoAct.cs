using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class SpotCheckOfficerToAct
    {
        public int SpotCheckOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int SpotCheckId { get; set; }

        public virtual StaffSpotCheck SpotCheck { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
