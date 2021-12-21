using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class SupervisionOfficerToAct
    {
        public int SupervisionOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int StaffSupervisionAppraisalId { get; set; }

        public virtual StaffSupervisionAppraisal Supervision { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
