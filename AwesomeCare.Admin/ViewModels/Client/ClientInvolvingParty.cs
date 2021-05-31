using AwesomeCare.DataTransferObject.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [RequiredDependant("true",nameof(IsSelected),typeof(bool))]
        public string Name { get; set; }
        [RequiredDependant("true", nameof(IsSelected), typeof(bool))]
        public string Address { get; set; }
        [EmailAddress]
        [RequiredDependant("true", nameof(IsSelected), typeof(bool))]
        public string Email { get; set; }
        [RequiredDependant("true", nameof(IsSelected), typeof(bool))]
        public string Telephone { get; set; }
        [RequiredDependant("true", nameof(IsSelected), typeof(bool))]
        public string Relationship { get; set; }
    }
}
