using AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Investigation
{
    public class PostInvestigation
    {
        public PostInvestigation()
        {
            InvestigationAttachments = new List<PostInvestigationAttachment>();
        }
        [Required(ErrorMessage = "please select a staff")]
        public int StaffPersonalInfoId { get; set; }
        [Required(ErrorMessage = "please select a client")]
        public int ClientId { get; set; }
        /// <summary>
        /// From BaseRecord Item
        /// </summary>
        [Required(ErrorMessage = "please select incident class")]
        public int IncidentClass { get; set; }
        [Required(ErrorMessage = "please provide remarks")]
        public string Remark { get; set; }
        [Required(ErrorMessage = "please select Incident Date")]
        public DateTimeOffset IncidentDate { get; set; }
        public DateTimeOffset? ConclusionDate { get; set; }

        public List<PostInvestigationAttachment> InvestigationAttachments { get; set; }
    }
}
