using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
  public  class GetClientAttachedToRota
    {
        public int ClientRotaId { get; set; }
        public int ClientId { get; set; }
        /// <summary>
        /// AM, TEA, LUNCH e.t.c.
        /// </summary>
        public int ClientRotaTypeId { get; set; }
        public int ClientRotaDaysId { get; set; }
        public int RotaDayofWeekId { get; set; }
        public int RotaId { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
    }
}
