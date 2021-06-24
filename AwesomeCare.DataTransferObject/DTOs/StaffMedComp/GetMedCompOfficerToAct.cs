using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffMedComp
{
    public class GetMedCompOfficerToAct
    {
        public int MedCompOfficerToActId { get; set; }
        public int MedCompId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
