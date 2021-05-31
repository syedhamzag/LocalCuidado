using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class UntowardsStaffInvolved
    {
        public int UntowardsStaffInvolvedId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int UntowardsId { get; set; }

        public virtual Untowards Untowards { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
