﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote
{
    public class PutTrackingConcernStaff
    {
        public int TrackingConcernStaffId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TrackingConcernNoteId { get; set; }
    }
}
