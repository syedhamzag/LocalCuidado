using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientServiceDetailItem
    {
        public int ClientServiceDetailItemId { get; set; }
        public int ClientServiceDetailId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        public virtual ClientServiceDetail ClientServiceDetail { get; set; }
    }
}
