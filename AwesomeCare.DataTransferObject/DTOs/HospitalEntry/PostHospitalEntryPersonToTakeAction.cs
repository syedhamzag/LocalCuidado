using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.HospitalEntry
{
    public class PostHospitalEntryPersonToTakeAction
    {
        public int HospitalEntryPersonToTakeActionId { get; set; }
        public int HospitalEntryId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
