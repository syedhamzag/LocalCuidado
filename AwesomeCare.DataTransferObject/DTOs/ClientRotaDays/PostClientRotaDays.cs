using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaDays
{
  public  class PostClientRotaDays
    {
        
        [Required(ErrorMessage = "please provide  ClientRota")]
        public int ClientRotaId { get; set; }
        [Required(ErrorMessage = "Please provide Day of Week")]
        public int RotaDayofWeekId { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string StopTime { get; set; }
        [Required(ErrorMessage = "Please provide Rota")]
        public int RotaId { get; set; }
    }
}
