﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffInfectionControl
    {
        public int InfectionId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public int Type { get; set; }
        public string Guideline { get; set; }
        public DateTime TestDate { get; set; }
        public int VaccStatus { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
