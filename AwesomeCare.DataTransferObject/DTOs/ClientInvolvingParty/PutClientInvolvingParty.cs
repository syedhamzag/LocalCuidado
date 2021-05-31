using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty
{
  public  class PutClientInvolvingParty
    {
        [Required]
        public int ClientInvolvingPartyId { get; set; }
        [Required]
        public int ClientInvolvingPartyItemId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(225)]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(125)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; }
    }
}
