using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase
{
   public class GetClientInvolvingPartyItem:BaseDTO
    {
        public int ClientInvolvingPartyItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
    }
}
