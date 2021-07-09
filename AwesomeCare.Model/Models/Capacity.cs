using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Capacity 
    {
        public Capacity()
        {
            Indicator = new HashSet<CapacityIndicator>();
        }

        public int CapacityId { get; set; }
        public int Pointer { get; set; }
        public int Implications { get; set; }

        public virtual ICollection<CapacityIndicator> Indicator { get; set; }
    }
}
