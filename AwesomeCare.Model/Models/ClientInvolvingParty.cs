using System;
using System.Collections.Generic;
using System.Text; 

namespace AwesomeCare.Model.Models
{
   public class ClientInvolvingParty
    {
        public int ClientInvolvingPartyId { get; set; }
        public int ClientInvolvingPartyItemId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Relationship { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientInvolvingPartyItem ClientInvolvingPartyItem { get; set; }
    }
}
