using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffCommunication
    {
        public int StaffCommunicationId { get; set; }
        public DateTime ValueDate { get; set; }
        public string Concern { get; set; }
        /// <summary>
        /// From BaseRecordItem
        /// </summary>
        public int CommunicationClassId { get; set; }
        /// <summary>
        /// From Staff
        /// </summary>
        public int PersonInvolved { get; set; }
        public string ExpectedAction { get; set; }
        public string ActionTaken { get; set; }
        /// <summary>
        /// From BaseRecordItem
        /// </summary>
        public int Status { get; set; }
        public string Telephone { get; set; }
        /// <summary>
        /// From Staff
        /// </summary>
        public int PersonResponsibleForAction { get; set; }
        public string Attachment { get; set; }
    }
}
