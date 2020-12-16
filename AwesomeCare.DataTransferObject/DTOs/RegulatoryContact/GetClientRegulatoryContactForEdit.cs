using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RegulatoryContact
{
   public class GetClientRegulatoryContactForEdit
    {
        public int ClientRegulatoryContactId { get; set; }
        public int ClientId { get; set; }
        public int BaseRecordItemId { get; set; }
        public string RegulatoryContact { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Link to attachement
        /// </summary>
        public string Evidence { get; set; }
    }
}
