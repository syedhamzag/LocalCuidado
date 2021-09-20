using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class HospitalEntryPersonToTakeAction
    {
        public int HospitalEntryPersonToTakeActionId { get; set; }
        public int HospitalEntryId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public virtual HospitalEntry HospitalEntry { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
