using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Chat
{
    public class GetChat : BaseDTO
    {
        public int ChatId { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public DateTime Dated { get; set; }
        public string Type { get; set; }

        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
    }
}
