using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HospitalExitOfficerToTakeAction
 
    {
        public int HospitalExitOfficerToTakeActionId { get; set; }
        public int HospitalExitId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public virtual HospitalExit HospitalExit { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
