using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ComplainOfficerToAct
    {
        public int ComplainOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ComplainId { get; set; }

        public virtual ClientComplainRegister ComplainRegister { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
