﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord
{
    public class GetBloodCoagPhysician
    {
        public int BloodCoagOfficerToActId { get; set; }
        public int BloodRecordId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
