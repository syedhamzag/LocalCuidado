using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs
{
   public class GetClientServiceDetailReceipt
    {
        public int ClientServiceDetailReceiptId { get; set; }
        public int ClientServiceDetailId { get; set; }
        public string Attachment { get; set; }

    }
}
