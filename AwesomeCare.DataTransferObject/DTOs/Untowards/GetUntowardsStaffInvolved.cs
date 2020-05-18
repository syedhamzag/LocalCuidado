using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Untowards
{
  public  class GetUntowardsStaffInvolved
    {
        public int UntowardsStaffInvolvedId { get; set; }
        public int UntowardsId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Staff { get; set; }
    }
}
