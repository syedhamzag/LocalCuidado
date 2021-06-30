using AwesomeCare.DataTransferObject.DTOs.Rotering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class GroupLiveRota
    {
        public GroupLiveRota()
        {
            Trackers = new List<LiveTracker>();
           
        }
        public string StaffName { get; set; }
        public List<LiveTracker> Trackers { get; set; }
       
    }

   
}
