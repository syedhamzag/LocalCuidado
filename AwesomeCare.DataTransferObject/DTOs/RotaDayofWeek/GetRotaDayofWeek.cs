using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek
{
   public class GetRotaDayofWeek:BaseDTO
    {
        public int RotaDayofWeekId { get; set; }
        public string DayofWeek { get; set; }
    }
}
