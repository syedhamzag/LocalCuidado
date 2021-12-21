using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators
{
    public class GetKeyIndicatorLog
    {
        public int KeyIndicatorLogId { get; set; }
        public int KeyId { get; set; }
        public int BaseRecordId { get; set; }
        public string ValueName { get; set; }
    }
}
