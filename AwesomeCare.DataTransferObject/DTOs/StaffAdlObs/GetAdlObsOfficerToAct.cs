﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffAdlObs
{
    public class GetAdlObsOfficerToAct
    {
        public int AdlObsOfficerToActId { get; set; }
        public int ObservationId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
