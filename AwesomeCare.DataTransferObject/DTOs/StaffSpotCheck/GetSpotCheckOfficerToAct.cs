using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientSpotCheck
{
    public class GetSpotCheckOfficerToAct
    {
        public int SpotCheckOfficerToActId { get; set; }
        public int SpotCheckId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
