using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    [System.Diagnostics.DebuggerDisplay("{ItemName}")]
    public class ClientInvolvingParty
    {
        public int ClientId { get; set; }
        public int ClientInvolvingPartyItemId { get; set; }        
        public string ItemName { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Relationship { get; set; }
    }
}
