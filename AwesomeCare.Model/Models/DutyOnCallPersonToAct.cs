using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class DutyOnCallPersonToAct
    {
        public int PersonToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int DutyOnCallId { get; set; }

        public virtual DutyOnCall DutyOnCall { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
