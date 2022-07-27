using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class CuidiBuddy
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CuidiBuddyId { get; set; }
        public virtual Client Client { get; set; }
    }
}
