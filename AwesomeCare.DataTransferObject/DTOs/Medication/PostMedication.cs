using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Medication
{
   public class PostMedication:BaseDTO
    {
       [Required]
       [MaxLength(225)]
       [Display(Name ="Name")]
        public string MedicationName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Strength { get; set; }
    }
}
