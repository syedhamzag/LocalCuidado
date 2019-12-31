using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
    public class GetClientRota 
    {
        public int ClientRotaId { get; set; }
        public int ClientId { get; set; }
        public int ClientRotaTypeId { get; set; }
    }
}
