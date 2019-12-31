using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaTask
{
   public class GetClientRotaTask
    {
        public int ClientRotaTaskId { get; set; }
        public int RotaTaskId { get; set; }
        public int ClientRotaDaysId { get; set; }
    }
}
