using AwesomeCare.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientInvolvingPartyItem: BaseModel
    {
        public ClientInvolvingPartyItem()
        {
            ClientInvolvingParty = new HashSet<ClientInvolvingParty>();
        }
        public int ClientInvolvingPartyItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ClientInvolvingParty> ClientInvolvingParty { get; set; }
    }
}
