using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMedAudit
{
    public class GetMedAuditStaffName
    {
        public int MedAuditStaffNameId { get; set; }
        public int MedAuditId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
