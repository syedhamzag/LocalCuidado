using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity
{
    public class PostCapacity
    {
        public int PersonalDetailId { get; set; }
        public int CapacityId { get; set; }
        public int Pointer { get; set; }
        public int Implications { get; set; }
    }
}
