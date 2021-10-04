using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.DutyOnCall
{
    public class PostDutyOnCallPersonResponsible
    {
        public int PersonResponsibleId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int DutyOnCallId { get; set; }
    }
}
