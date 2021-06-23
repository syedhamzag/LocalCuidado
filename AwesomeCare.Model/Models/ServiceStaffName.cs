using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ServiceStaffName
    {
        public int ServiceStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ServiceId { get; set; }

        public virtual ClientServiceWatch Service { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
