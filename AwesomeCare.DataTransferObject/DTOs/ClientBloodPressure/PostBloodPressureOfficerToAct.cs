using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure
{
    public class PostBloodPressureOfficerToAct
    {
        public int BloodPressureOfficerToActId { get; set; }
        public int BloodPressureId { get; set; }
        public int StaffPersonalInfoId { get; set; }

    }
}
