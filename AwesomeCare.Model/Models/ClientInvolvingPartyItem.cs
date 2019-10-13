using AwesomeCare.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientInvolvingPartyItem: BaseModel
    {
        public int ClientInvolvingPartyItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
       
    }
}
