using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ClientServiceDetail
    {
        public ClientServiceDetail()
        {
            ClientServiceDetailReceipts = new HashSet<ClientServiceDetailReceipt>();
            ClientServiceDetailItems = new HashSet<ClientServiceDetailItem>();
        }
        public int ClientServiceDetailId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal AmountReturned { get; set; }
        public DateTimeOffset ServiceDate { get; set; }

        public virtual ICollection<ClientServiceDetailReceipt> ClientServiceDetailReceipts { get; set; }
        public virtual ICollection<ClientServiceDetailItem> ClientServiceDetailItems { get; set; }
    }
}
