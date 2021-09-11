using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.InterestAndObjective
{
    public class PostInterestAndObjective
    {
        public PostInterestAndObjective()
        {
            Interest = new List<PostInterest>();
            PersonalityTest = new List<PostPersonalityTest>();
        }

        public int GoalId { get; set; }
        public int ClientId { get; set; }
        public string CareGoal { get; set; }
        public string Brief { get; set; }

        public List<PostInterest> Interest { get; set; }
        public List<PostPersonalityTest> PersonalityTest { get; set; }
    }
}
