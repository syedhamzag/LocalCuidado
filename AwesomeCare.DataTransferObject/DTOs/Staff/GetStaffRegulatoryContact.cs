using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
  public  class GetStaffRegulatoryContact
    {
        public int StaffRegulatoryContactId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BaseRecordItemId { get; set; }
        public string RegulatoryContact { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Link to attachement
        /// </summary>
        public string Evidence { get; set; }
        public bool HasGoogleForm { get; set; }
        public string AddLink { get; set; }
        public string ViewLink { get; set; }
    }
}
