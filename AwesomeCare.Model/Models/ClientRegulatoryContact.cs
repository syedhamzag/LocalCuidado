using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientRegulatoryContact
    {
        public int ClientRegulatoryContactId { get; set; }
        public int ClientId { get; set; }
        public int BaseRecordItemId { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Link to attachement
        /// </summary>
        public string Evidence { get; set; }


        public virtual BaseRecordItemModel BaseRecordItem { get; set; }
        public virtual Client Client { get; set; }
    }
}
