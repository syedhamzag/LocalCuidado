using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class CommunicationTo
    {
        public int CommunicationToId { get; set; }
        public int To { get; set; }

        public virtual Communication Communication { get; set; }
    }
}
