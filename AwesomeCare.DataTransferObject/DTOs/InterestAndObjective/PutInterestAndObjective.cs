using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.InterestAndObjective
{
    public class PutInterestAndObjective
    {
        public PutInterestAndObjective()
        {
            Interest = new List<PutInterest>();
            PersonalityTest = new List<PutPersonalityTest>();
        }

        public int GoalId { get; set; }
        public int ClientId { get; set; }
        public string CareGoal { get; set; }
        public string Brief { get; set; }

        public List<PutInterest> Interest { get; set; }
        public List<PutPersonalityTest> PersonalityTest { get; set; }
    }
}
