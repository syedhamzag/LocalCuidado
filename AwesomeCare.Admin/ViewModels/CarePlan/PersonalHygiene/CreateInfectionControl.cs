﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.PersonalHygiene
{
    public class CreateInfectionControl
    {
        public int InfectionId { get; set; }
        public int ClientId { get; set; }
        public int Type { get; set; }
        public string Guideline { get; set; }
        public DateTime TestDate { get; set; }
        public int VaccStatus { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }

        public string ClientName { get; set; }
    }
}
