﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class PulseRateStaffName
    {
        public int PulseRateStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int PulseRateId { get; set; }

        public virtual ClientPulseRate PulseRate { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}