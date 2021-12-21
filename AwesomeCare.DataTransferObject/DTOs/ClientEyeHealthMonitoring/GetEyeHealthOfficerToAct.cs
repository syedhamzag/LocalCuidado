using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring
{
    public class GetEyeHealthOfficerToAct
    {
        public int EyeHealthOfficerToActId { get; set; }
        public int EyeHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
