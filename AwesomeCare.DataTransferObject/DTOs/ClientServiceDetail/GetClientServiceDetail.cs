using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs
{
   public class GetClientServiceDetail
    {
        public GetClientServiceDetail()
        {
            ClientServiceDetailReceipts = new List<GetClientServiceDetailReceipt>();
            ClientServiceDetailItems = new List<GetClientServiceDetailItem>();
        }
        public int ClientServiceDetailId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Staff { get; set; }
        public int ClientId { get; set; }
        public string Client { get; set; }
        public decimal AmountGiven { get; set; }
        public decimal AmountReturned { get; set; }
        public DateTimeOffset ServiceDate { get; set; }

        public  List<GetClientServiceDetailReceipt> ClientServiceDetailReceipts { get; set; }
        public  List<GetClientServiceDetailItem> ClientServiceDetailItems { get; set; }
    }
}
