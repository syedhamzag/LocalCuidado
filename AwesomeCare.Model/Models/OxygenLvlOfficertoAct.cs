using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class OxygenLvlOfficerToAct
    {
        public int OxygenLvlOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int OxygenLvlId { get; set; }

        public virtual ClientOxygenLvl OxygenLvl { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
