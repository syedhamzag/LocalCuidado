using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
   public class GetClientTask
    {
        public int ClientRotaTaskId { get; set; }
        public int RotaTaskId { get; set; }
        public int ClientRotaDaysId { get; set; }
    }
}
