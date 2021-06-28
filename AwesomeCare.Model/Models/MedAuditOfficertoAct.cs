using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class MedAuditOfficerToAct
    {
        public int MedAuditOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int MedAuditId { get; set; }

        public virtual ClientMedAudit MedAudit { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
