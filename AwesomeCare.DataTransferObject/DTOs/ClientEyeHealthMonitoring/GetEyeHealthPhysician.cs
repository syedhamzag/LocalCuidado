using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring
{
    public class GetEyeHealthPhysician
    {
        public int EyeHealthPhysicianId { get; set; }
        public int EyeHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
