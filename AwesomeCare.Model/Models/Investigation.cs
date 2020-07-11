using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class Investigation
    {
        public Investigation()
        {
            InvestigationAttachments = new HashSet<InvestigationAttachment>();
        }
        public int InvestigationId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        /// <summary>
        /// From BaseRecord Item
        /// </summary>
        public int IncidentClass { get; set; }
        public string Remark { get; set; }
        public DateTimeOffset IncidentDate { get; set; }
        public DateTimeOffset? ConclusionDate { get; set; }

        public virtual ICollection<InvestigationAttachment> InvestigationAttachments { get; set; }
    }
}
