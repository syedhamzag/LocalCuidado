using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientWoundCare
{
    public class GetWoundCareOfficerToAct
    {
        public int WoundCareOfficerToActId { get; set; }
        public int WoundCareId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
