using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class TrackingConcernStaff
    {
        public int TrackingConcernStaffId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TrackingConcernNoteId { get; set; }

        public virtual TrackingConcernNote TrackingConcernNote { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
