using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.DutyOnCall
{
    public class GetDutyOnCallPersonResponsible
    {
        public int PersonResponsibleId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int DutyOnCallId { get; set; }

        public string StaffName { get; set; }
    }
}
