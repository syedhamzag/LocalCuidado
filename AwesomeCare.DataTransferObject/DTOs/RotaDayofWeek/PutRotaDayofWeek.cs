using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek
{
   public class PutRotaDayofWeek:BaseDTO
    {
        [Required]
        public int RotaDayofWeekId { get; set; }
        [Required]
        public string DayofWeek { get; set; }
    }
}
