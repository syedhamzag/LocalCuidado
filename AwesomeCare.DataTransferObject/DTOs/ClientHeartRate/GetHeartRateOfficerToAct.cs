using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHeartRate
{
    public class GetHeartRateOfficerToAct
    {
        public int HeartRateOfficerToActId { get; set; }
        public int HeartRateId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
