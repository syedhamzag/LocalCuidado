using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Communication
    {
        public int CommunicationId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Message { get; set; }
    }
}
