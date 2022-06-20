﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Medication
{
    public class PutStaffMedTracker
    {
        public int StaffMedTrackerId { get; set; }
        public DateTime MedTrackDate { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int RotaId { get; set; }

        public int ClientMedId { get; set; }
        public string DoseGiven { get; set; }
        public int Status { get; set; }
    }
}
