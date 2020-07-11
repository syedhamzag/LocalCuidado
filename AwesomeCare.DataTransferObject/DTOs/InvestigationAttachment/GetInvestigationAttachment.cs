using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment
{
  public  class GetInvestigationAttachment
    {
        public int InvestigationAttachmentId { get; set; }
        public int InvestigationId { get; set; }
        public string Attachment { get; set; }
    }
}
