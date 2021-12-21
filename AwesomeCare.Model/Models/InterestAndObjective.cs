using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class InterestAndObjective
    {

        public InterestAndObjective()
        {
            Interest = new HashSet<Interest>();
            PersonalityTest = new HashSet<PersonalityTest>();
        }

        public int GoalId { get; set; }
        public int ClientId { get; set; }
        public string CareGoal { get; set; }
        public string Brief { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Interest> Interest { get; set; }
        public virtual ICollection<PersonalityTest> PersonalityTest { get; set; }
    }
}
