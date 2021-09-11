using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Interest
    {
        public int InterestId { get; set; }
        public int GoalId { get; set; }
        public int LeisureActivity { get; set; }
        public int InformalActivity { get; set; }
        public int MaintainContact { get; set; }
        public int CommunityActivity { get; set; }
        public int EventAwarness { get; set; }
        public int GoalAndObjective { get; set; }

        public virtual InterestAndObjective InterestAndObjective { get; set; }
    }
}
