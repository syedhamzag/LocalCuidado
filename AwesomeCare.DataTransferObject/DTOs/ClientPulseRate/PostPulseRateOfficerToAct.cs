﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPulseRate
{
    public class PostPulseRateOfficerToAct
    {
        public int PulseRateOfficerToActId { get; set; }
        public int PulseRateId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
