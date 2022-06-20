using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Chat
{
    public class PostChat : BaseDTO
    {
        public int ChatId { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public DateTime Dated { get; set; }
        public string Type { get; set; }
    }
}
