using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity
{
    public class PostCapacity
    {
        public PostCapacity()
        {
            Indicator = new List<PostCapacityIndicator>();
        }
        public int PersonalDetailId { get; set; }
        public int CapacityId { get; set; }
        public int Pointer { get; set; }
        public string Implications { get; set; }

        public List<PostCapacityIndicator> Indicator { get; set; }
    }
}
