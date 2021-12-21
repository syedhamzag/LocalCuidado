using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class LiveRota
    {
        public LiveRota()
        {
            ClockDifferences = new List<LiveRotaClockDifference>();
            GroupLiveRotas = new List<GroupLiveRota>();
        }

        public List<LiveRotaClockDifference> ClockDifferences { get; set; }
        public List<GroupLiveRota> GroupLiveRotas { get; set; }
    }
}
