using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHobbies
{
    public class GetClientHobbies
    {
        public int CHId { get; set; }
        public int HId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
    }
}
