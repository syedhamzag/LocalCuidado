using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class WoundCareStaffName
    {
        public int WoundCareStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int WoundCareId { get; set; }

        public virtual ClientWoundCare WoundCare { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}