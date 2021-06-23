using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientVisit
{
    public class GetVisitOfficerToAct
    {
        public int VisitOfficerToActId { get; set; }
        public int VisitId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
