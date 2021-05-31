using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffCommunication
{
  public  class GetStaffCommunication
    {
        public int StaffCommunicationId { get; set; }
        [Display(Name ="Date")]
        public DateTime ValueDate { get; set; }
        public string Concern { get; set; }
        /// <summary>
        /// From BaseRecordItem
        /// </summary>
        [Display(Name = "Class")]
        public string CommunicationClass { get; set; }
        /// <summary>
        /// From Staff
        /// </summary>
        [Display(Name = "Person Involved")]
        public string PersonInvolved { get; set; }
        [Display(Name = "Expected Action")]
        public string ExpectedAction { get; set; }
        [Display(Name = "Action Taken")]
        public string ActionTaken { get; set; }
        /// <summary>
        /// From BaseRecordItem
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; }
        
        public string Telephone { get; set; }
        /// <summary>
        /// From Staff
        /// </summary>
        [Display(Name = "Person Responsible For Action")]
        public string PersonResponsibleForAction { get; set; }
        
        public string Attachment { get; set; }
    }
}
