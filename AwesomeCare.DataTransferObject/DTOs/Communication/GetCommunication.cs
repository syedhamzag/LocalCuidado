using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Communication
{
  public  class GetCommunication
    {
        public GetCommunication()
        {
            InboxMessages = new List<InboxMessage>();
            SentMessages = new List<SentMessage>();
        }
        public List<InboxMessage> InboxMessages { get; set; }
        public List<SentMessage> SentMessages { get; set; }
        public int TotalSent { get; set; }
        public int TotalReceived { get; set; }

        //public int CommunicationId { get; set; }
        //public string From { get; set; }
        //public string Sender { get; set; }
        //public string To { get; set; }
        //public string Receiver { get; set; }
        //public string Message { get; set; }
        //public string Subject { get; set; }
        //public DateTime CommuncationDate { get; set; }
        //public bool IsRead { get; set; }
    }
}
