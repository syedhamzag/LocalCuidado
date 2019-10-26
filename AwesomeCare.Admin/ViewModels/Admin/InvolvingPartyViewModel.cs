using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Admin
{
    public class InvolvingPartyViewModel
    {
        public InvolvingPartyViewModel()
        {
            InvolvingPartyItems = new List<GetClientInvolvingPartyItem>();
        }
        [Required]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string ItemName { get; set; }
        [MaxLength(225)]
        public string Description { get; set; }

        public int Id { get; set; }
        public List<GetClientInvolvingPartyItem> InvolvingPartyItems { get; set; }
    }
}
