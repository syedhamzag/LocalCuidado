using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientHealthCondition
    {
        public int CHCId { get; set; }
        public int HCId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
