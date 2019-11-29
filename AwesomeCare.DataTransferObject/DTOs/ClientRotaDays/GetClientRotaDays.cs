using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaDays
{
   public class GetClientRotaDays
    {
        public int ClientRotaDaysId { get; set; }
        public int ClientRotaId { get; set; }
        public int RotaDayofWeekId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
    }
}
