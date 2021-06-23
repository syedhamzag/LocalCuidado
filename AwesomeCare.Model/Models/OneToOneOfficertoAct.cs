using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class OneToOneOfficerToAct
    {
        public int OneToOneOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int OneToOneId { get; set; }

        public virtual StaffOneToOne OneToOne { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
