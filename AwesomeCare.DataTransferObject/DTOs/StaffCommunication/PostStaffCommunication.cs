using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffCommunication
{
   public class PostStaffCommunication
    {
        [Required]
        [Display(Name ="Date")]
        public string ValueDate { get; set; }
        [Required]
        public string Concern { get; set; }
        /// <summary>
        /// From BaseRecordItem
        /// </summary>
        [Required(ErrorMessage ="please select class")]
        [Display(Name = "Class")]
        public int CommunicationClassId { get; set; }
        /// <summary>
        /// From Staff
        /// </summary>
        [Required(ErrorMessage ="please select person involved")]
        [Display(Name = "Person Involved")]
        public int PersonInvolved { get; set; }
        [Required(ErrorMessage ="please input expected action")]
        [Display(Name = "Expected Action")]
        public string ExpectedAction { get; set; }
        [Required(ErrorMessage ="please input action taken")]
        [Display(Name = "Action Taken")]
        public string ActionTaken { get; set; }
        /// <summary>
        /// From BaseRecordItem
        /// </summary>
        [Required(ErrorMessage = "please select status")]
        public int Status { get; set; }
        public string Telephone { get; set; }
        /// <summary>
        /// From Staff
        /// </summary>
        [Required(ErrorMessage ="please select person responsibe for action")]
        [Display(Name = "Person Responsible for Action")]
        public int PersonResponsibleForAction { get; set; }
        public IFormFile FileAttachment { get; set; }
        public string Attachment { get; set; }
    }
}
