using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientLogAudit
{
    public class PutLogAuditOfficerToAct
    {
        public int LogAuditId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
