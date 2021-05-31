using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Communication
{
   public class Message
    {
        public int CommunicationId { get; set; }
        public string To { get; set; }       
        public string CommunicationMessage { get; set; }       
        public string Subject { get; set; }
        public DateTime CommuncationDate { get; set; }
        public bool IsRead { get; set; }
    }
}
