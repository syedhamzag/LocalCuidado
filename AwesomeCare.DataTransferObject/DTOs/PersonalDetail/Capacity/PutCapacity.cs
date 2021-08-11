using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity
{
    public class PutCapacity
    {
        public PutCapacity()
        {
            Indicator = new List<PutCapacityIndicator>();
        }
        public int PersonalDetailId { get; set; }
        public int CapacityId { get; set; }
        public int Pointer { get; set; }
        public int Implications { get; set; }

        public List<PutCapacityIndicator> Indicator { get; set; }
    }
}
