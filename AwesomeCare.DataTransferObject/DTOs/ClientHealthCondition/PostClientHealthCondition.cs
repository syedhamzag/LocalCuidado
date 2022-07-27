using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition
{
    public class PostClientHealthCondition
    {
        public int CHCId { get; set; }
        public int HCId { get; set; }
        public int ClientId { get; set; }
    }
}
