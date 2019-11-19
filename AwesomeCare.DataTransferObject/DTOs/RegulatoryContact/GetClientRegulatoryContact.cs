using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RegulatoryContact
{
  public  class GetClientRegulatoryContact
    {
        public int ClientRegulatoryContactId { get; set; }
        public int ClientId { get; set; }
        public int BaseRecordItemId { get; set; }
        public DateTime? DatePerformed { get; set; }
        public DateTime? DueDate { get; set; }      
        public string Evidence { get; set; }
    }
}
