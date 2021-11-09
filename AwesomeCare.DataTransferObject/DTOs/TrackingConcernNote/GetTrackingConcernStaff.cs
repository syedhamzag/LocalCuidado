using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote
{
    public class GetTrackingConcernStaff
    {
        public int TrackingConcernStaffId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TrackingConcernNoteId { get; set; }
        public string StaffName { get; set; }

    }
}
