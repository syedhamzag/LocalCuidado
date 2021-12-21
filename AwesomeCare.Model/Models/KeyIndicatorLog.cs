using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class KeyIndicatorLog
    {
        public int KeyIndicatorLogId { get; set; }
        public int KeyId { get; set; }
        public int BaseRecordId { get; set; }

        public virtual KeyIndicators KeyIndicators { get; set; }
    }
}
