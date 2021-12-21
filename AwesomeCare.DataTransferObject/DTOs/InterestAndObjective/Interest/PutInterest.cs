using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest
{
    public class PutInterest
    {
        public int InterestId { get; set; }
        public int GoalId { get; set; }
        public int LeisureActivity { get; set; }
        public int InformalActivity { get; set; }
        public int MaintainContact { get; set; }
        public int CommunityActivity { get; set; }
        public int EventAwarness { get; set; }
        public int GoalAndObjective { get; set; }
    }
}
