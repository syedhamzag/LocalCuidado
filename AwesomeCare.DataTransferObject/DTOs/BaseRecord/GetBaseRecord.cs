using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecord
{
    public class GetBaseRecord
    {
        public int BaseRecordId { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }
    }
}
