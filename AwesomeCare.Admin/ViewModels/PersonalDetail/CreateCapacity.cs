using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateCapacity
    {
        public CreateCapacity()
        {
            IndicatorList = new List<SelectListItem>();
        }
        public ICollection<SelectListItem> IndicatorList { get; set; }

        [Required]
        public int PersonalDetailId { get; set; } 
        [Required]
        public int CapacityId { get; set; }
        [Required]
        public int Pointer { get; set; }
        [Required]
        public int Implications { get; set; }

    }
}
