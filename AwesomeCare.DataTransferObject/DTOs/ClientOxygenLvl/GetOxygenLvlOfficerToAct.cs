using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl
{
    public class GetOxygenLvlOfficerToAct
    {
        public int OxygenLvlOfficerToActId { get; set; }
        public int OxygenLvlId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
