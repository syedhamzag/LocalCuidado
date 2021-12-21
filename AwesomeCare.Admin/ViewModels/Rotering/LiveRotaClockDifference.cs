using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class LiveRotaClockDifference
    {
        public string Period { get; set; }
        public TimeSpan TotalClockDifference { get; set; }
    }
}
