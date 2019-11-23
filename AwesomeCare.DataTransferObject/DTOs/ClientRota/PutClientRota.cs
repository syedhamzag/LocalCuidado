using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
   public class PutClientRota:BaseDTO
    {
        [Required]
        public int RotaId { get; set; }
        [Display(Name ="Number of Staff")]
        [Required]
        public int NumberOfStaff { get; set; }
        [Required]
        [Display(Name = "Rota Name")]
        public string RotaName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Area { get; set; }
    }
}
