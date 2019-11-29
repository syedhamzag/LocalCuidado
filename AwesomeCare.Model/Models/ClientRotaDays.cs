using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientRotaDays
    {
        public int ClientRotaDaysId { get; set; }
        public int ClientRotaId { get; set; }
        public int RotaDayofWeekId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }

        public virtual ClientRota ClientRota { get; set; }
        public virtual RotaDayofWeek RotaDayofWeek { get; set; }
    }
}
