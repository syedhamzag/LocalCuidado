using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class VisitStaffName
    {
        public int VisitStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int VisitId { get; set; }

        public virtual ClientMgtVisit Visit { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
