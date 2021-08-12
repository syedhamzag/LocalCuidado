using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class CapacityIndicator
    {
        public int CapacityIndicatorId { get; set; }
        public int CapacityId { get; set; }  
        public int BaseRecordId { get; set; }

        public virtual Capacity Capacity { get; set; }
    }
}
