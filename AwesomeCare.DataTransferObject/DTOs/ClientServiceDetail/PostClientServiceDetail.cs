using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs
{
   public class PostClientServiceDetail
    {
        public PostClientServiceDetail()
        {
            ClientServiceDetailReceipts = new List<PostClientServiceDetailReceipt>();
            ClientServiceDetailItems = new List<PostClientServiceDetailItem>();
        }
        
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal AmountReturned { get; set; }

        public  List<PostClientServiceDetailReceipt> ClientServiceDetailReceipts { get; set; }
        public  List<PostClientServiceDetailItem> ClientServiceDetailItems { get; set; }
    }
}
