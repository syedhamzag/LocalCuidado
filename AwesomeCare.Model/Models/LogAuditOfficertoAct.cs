using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class LogAuditOfficerToAct
    {
        public int LogAuditOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int LogAuditId { get; set; }

        public virtual ClientLogAudit LogAudit { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
