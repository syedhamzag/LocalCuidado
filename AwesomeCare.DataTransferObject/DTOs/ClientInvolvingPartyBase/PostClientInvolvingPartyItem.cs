using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase
{
   public class PostClientInvolvingPartyItem
    {
        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }
        [MaxLength(225)]
        public string Description { get; set; }
    }
}
