using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class PostStaffRegulatoryContact
    {
        public int StaffPersonalInfoId { get; set; }
        public int BaseRecordItemId { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Link to attachement
        /// </summary>
        public string Evidence { get; set; }
    }
}
