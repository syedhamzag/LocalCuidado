﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Communication
{
   public class InboxMessage
    {
        public int CommunicationId { get; set; }
        public string From { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        [Display(Name = "Date")]
        public DateTime CommuncationDate { get; set; }
        public bool IsRead { get; set; }
    }
}
