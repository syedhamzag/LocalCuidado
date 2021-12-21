using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.OfficeLocation
{
   public class PutOfficeLocation
    {
        [Required]
        public int OfficeLocationId { get; set; }

        [Required]
        [MaxLength(15)]
        public string UniqueId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Latitude { get; set; }

        [MaxLength(255)]
        public string Longitude { get; set; }

        [MaxLength(255)]
        public string ContactPersonFullName { get; set; }

        [MaxLength(255)]
        public string ContactPersonEmail { get; set; }

        [MaxLength(255)]
        public string ContactPersonPhoneNumber { get; set; }
    }
}
