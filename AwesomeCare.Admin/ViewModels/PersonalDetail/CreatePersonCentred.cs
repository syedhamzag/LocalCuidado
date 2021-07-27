using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreatePersonCentred
    {
        public CreatePersonCentred()
        {
            FocusList = new List<SelectListItem>();
        }

        public ICollection<SelectListItem> FocusList { get; set; }

        [Required]
        public int PersonCentredId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public List<int> Focus { get; set; }
        [Required]
        public int Class { get; set; }
        [Required]
        public string ExpSupport { get; set; }
    }
}
