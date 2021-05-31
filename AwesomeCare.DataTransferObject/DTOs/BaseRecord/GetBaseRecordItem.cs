using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecord
{
   public class GetBaseRecordItem
    {
        public int BaseRecordItemId { get; set; }
        public int BaseRecordId { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public bool Deleted { get; set; }
        public bool HasGoogleForm { get; set; }
        public string AddLink { get; set; }
        public string ViewLink { get; set; }
    }
}
