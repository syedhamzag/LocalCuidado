using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaType
{
   public class GetClientRotaType:BaseDTO
    {
        public int ClientRotaTypeId { get; set; }
        public string RotaType { get; set; }
    }
}
