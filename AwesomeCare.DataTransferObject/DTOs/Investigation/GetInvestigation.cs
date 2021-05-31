using AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Investigation
{
  public  class GetInvestigation
    {
        public GetInvestigation()
        {
            InvestigationAttachments = new List<GetInvestigationAttachment>();
        }

        public int InvestigationId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Staff { get; set; }
        public int ClientId { get; set; }
        public string Client { get; set; }
        /// <summary>
        /// From BaseRecord Item
        /// </summary>       
        public int IncidentClassId { get; set; }
        [Display(Name = "Incident Class")]
        public string IncidentClass { get; set; }
        public string Remark { get; set; }
        [Display(Name = "Incident Date")]
        public DateTimeOffset IncidentDate { get; set; }
        [Display(Name = "Conclusion Date")]
        public DateTimeOffset? ConclusionDate { get; set; }
        [Display(Name = "Attachments")]
        public List<GetInvestigationAttachment> InvestigationAttachments { get; set; }
    }
}
