﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity
{
    public class GetCapacityIndicator
    {
        public int CapacityId { get; set; }
        public int CapacityIndicatorId { get; set; }
        public int BaseRecordId { get; set; }
        public string ValueName { get; set; }
    }
}
