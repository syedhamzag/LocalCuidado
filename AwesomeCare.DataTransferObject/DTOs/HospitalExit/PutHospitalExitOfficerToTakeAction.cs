using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.HospitalExit
{
    public class PutHospitalExitOfficerToTakeAction
    {
        public int HospitalExitOfficerToTakeActionId { get; set; }
        public int HospitalExitId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
