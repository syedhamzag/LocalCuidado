using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffRegulatoryContact
    {
        public int StaffRegulatoryContactId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int BaseRecordItemId { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Link to attachement
        /// </summary>
        public string Evidence { get; set; }


        public virtual BaseRecordItemModel BaseRecordItem { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
