using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CuidiBuddy
{
    public class PutCuidiBuddy
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CuidiBuddyId { get; set; }
    }
}
