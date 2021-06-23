using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class MedCompOfficerToAct
    {
        public int MedCompOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int MedCompId { get; set; }

        public virtual StaffMedComp MedComp { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
