using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek
{
   public class PostRotaDayofWeek
    {
        [Required]
        public string DayofWeek { get; set; }
    }
}
