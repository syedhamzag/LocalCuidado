using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientHobbies
    {
        public int CHId { get; set; }
        public int HId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
