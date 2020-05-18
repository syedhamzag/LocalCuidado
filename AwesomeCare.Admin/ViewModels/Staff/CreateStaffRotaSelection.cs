using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffRotaSelection
    {
        public CreateStaffRotaSelection()
        {
            RotaSelections = new List<GetStaffRotaDynamicAddition>();
        }

        public string SubTitle { get; set; } = "Add Item";
        [Required]
        public int StaffRotaDynamicAdditionId { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        public bool Deleted { get; set; } = false;
        public List<GetStaffRotaDynamicAddition> RotaSelections { get; set; }
    }
}
