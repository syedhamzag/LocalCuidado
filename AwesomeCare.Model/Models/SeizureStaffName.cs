using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class SeizureStaffName
    {
        public int SeizureStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int SeizureId { get; set; }

        public virtual ClientSeizure Seizure { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}