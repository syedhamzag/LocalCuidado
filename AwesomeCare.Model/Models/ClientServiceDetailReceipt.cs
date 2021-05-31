using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ClientServiceDetailReceipt
    {
        public int ClientServiceDetailReceiptId { get; set; }
        public int ClientServiceDetailId { get; set; }
        public string Attachment { get; set; }

        public virtual ClientServiceDetail ClientServiceDetail { get; set; }
    }
}
