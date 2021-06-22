using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp
{
    public class GetBodyTempOfficerToAct
    {
        public int BodyTempOfficerToActId { get; set; }
        public int BodyTempId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
