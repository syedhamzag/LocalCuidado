using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase
{
   public class PutClientInvolvingPartyItem:BaseDTO
    {
        [Required]
        public int ClientInvolvingPartyItemId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }
        [MaxLength(225)]
        public string Description { get; set; }
    }
}
