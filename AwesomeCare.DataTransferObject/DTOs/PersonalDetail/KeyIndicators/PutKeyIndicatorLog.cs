using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators
{
    public class PutKeyIndicatorLog
    {
        public int KeyIndicatorLogId { get; set; }
        public int KeyId { get; set; }
        public int BaseRecordId { get; set; }
    }
}
