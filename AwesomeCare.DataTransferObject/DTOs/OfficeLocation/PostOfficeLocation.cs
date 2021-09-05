using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.OfficeLocation
{
   public class PostOfficeLocation
    {
       [Required]    
       [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Latitude { get; set; }

        [MaxLength(255)]
        public string Longitude { get; set; }

        [MaxLength(255)]
        [Display(Name = "Contact Person")]
        public string ContactPersonFullName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Contact Person Email")]
        public string ContactPersonEmail { get; set; }

        [MaxLength(255)]
        [Display(Name = "Contact Person Telephone")]
        public string ContactPersonPhoneNumber { get; set; }
    }
}
