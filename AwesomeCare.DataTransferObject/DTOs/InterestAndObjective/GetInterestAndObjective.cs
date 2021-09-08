using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.InterestAndObjective
{
    public class GetInterestAndObjective
    {
        public GetInterestAndObjective()
        {
            Interest = new List<GetInterest>();
            PersonalityTest = new List<GetPersonalityTest>();
        }

        public int GoalId { get; set; }
        public int ClientId { get; set; }
        public string CareGoal { get; set; }
        public string Brief { get; set; }

        public List<GetInterest> Interest { get; set; }
        public List<GetPersonalityTest> PersonalityTest { get; set; }
    }
}
