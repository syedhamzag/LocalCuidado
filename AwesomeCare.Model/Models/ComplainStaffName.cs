using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ComplainStaffName
    {
        public int ComplainStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ComplainId { get; set; }

        public virtual ClientComplainRegister ComplainRegister { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
