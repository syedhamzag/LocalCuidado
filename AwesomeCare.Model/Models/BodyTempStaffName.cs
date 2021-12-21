using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class BodyTempStaffName
    {
        public int BodyTempStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BodyTempId { get; set; }

        public virtual ClientBodyTemp BodyTemp { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
