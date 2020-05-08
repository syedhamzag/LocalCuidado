using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
   public class RotaAdmin
    {
        public RotaAdmin()
        {
            RotaDays = new List<RotaDays>();
        }
        public int ClientRotaId { get; set; }
        public int ClientId { get; set; }
        public string Period { get; set; }
        public string ClientName { get; set; }
        public string ClientKeySafe { get; set; }
        public string ClientPostCode { get; set; }

        public List<RotaDays> RotaDays { get; set; }
    }
}
