using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
  public  class GetClientRotaDay
    {
        public GetClientRotaDay()
        {
            RotaTasks = new List<GetClientTask>();
        }
        public int ClientRotaDaysId { get; set; }
        public int ClientRotaId { get; set; }
        public int RotaDayofWeekId { get; set; }
        public int RotaId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public List<GetClientTask> RotaTasks { get; set; }
    }
}
