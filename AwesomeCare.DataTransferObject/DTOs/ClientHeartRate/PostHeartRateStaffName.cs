﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHeartRate
{
    public class PostHeartRateStaffName
    {
        public int HeartRateOfficerToActId { get; set; }
        public int HeartRateId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
