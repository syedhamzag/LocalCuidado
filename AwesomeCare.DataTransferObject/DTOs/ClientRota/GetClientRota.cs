using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRota
{
    public class GetClientRota 
    {
        public GetClientRota()
        {
            ClientRotaDays = new List<GetClientRotaDay>();
        }
        public int ClientRotaId { get; set; }       
        public int ClientId { get; set; }
        public int ClientRotaTypeId { get; set; }

        public List<GetClientRotaDay> ClientRotaDays { get; set; }
    }
}
