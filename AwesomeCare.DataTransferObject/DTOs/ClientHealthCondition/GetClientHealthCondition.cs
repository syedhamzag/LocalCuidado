using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition
{
    public class GetClientHealthCondition
    {
        public int CHCId { get; set; }
        public int HCId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
