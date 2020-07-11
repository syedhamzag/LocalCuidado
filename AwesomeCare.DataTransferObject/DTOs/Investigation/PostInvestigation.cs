using AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Investigation
{
  public  class PostInvestigation
    {
        public PostInvestigation()
        {
            InvestigationAttachments = new List<PostInvestigationAttachment>();
        }
               
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        /// <summary>
        /// From BaseRecord Item
        /// </summary>
        public int IncidentClass { get; set; }
        public string Remark { get; set; }
        public DateTimeOffset IncidentDate { get; set; }
        public DateTimeOffset? ConclusionDate { get; set; }

        public List<PostInvestigationAttachment> InvestigationAttachments { get; set; }
    }
}
