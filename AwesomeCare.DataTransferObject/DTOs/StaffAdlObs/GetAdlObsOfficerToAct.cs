using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientAdlObs
{
    public class GetAdlObsOfficerToAct
    {
        public int AdlObsOfficerToActId { get; set; }
        public int AdlObsId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
