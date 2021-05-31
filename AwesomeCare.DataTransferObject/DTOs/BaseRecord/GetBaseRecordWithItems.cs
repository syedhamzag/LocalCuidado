using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecord
{
   public class GetBaseRecordWithItems
    {
        public GetBaseRecordWithItems()
        {
            BaseRecordItems = new List<GetBaseRecordItem>();
        }
        public int BaseRecordId { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }
        public List<GetBaseRecordItem> BaseRecordItems { get; set; }
    }
}
