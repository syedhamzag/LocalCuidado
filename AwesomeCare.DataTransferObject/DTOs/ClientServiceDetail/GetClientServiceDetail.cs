using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name ="Amount Given")]
        public decimal AmountGiven { get; set; }
        [Display(Name = "Amount Returned")]
        public decimal AmountReturned { get; set; }
        [Display(Name = "Service Date")]
        public DateTimeOffset ServiceDate { get; set; }

        public  List<GetClientServiceDetailReceipt> ClientServiceDetailReceipts { get; set; }
        public  List<GetClientServiceDetailItem> ClientServiceDetailItems { get; set; }
    }
}
