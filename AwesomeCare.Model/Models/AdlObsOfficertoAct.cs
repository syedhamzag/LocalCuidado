using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class AdlObsOfficerToAct
    {
        public int AdlObsOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ObservationId { get; set; }

        public virtual StaffAdlObs AdlObs { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
