using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.InterestObjective
{
    public class CreateInterestAndObjective
    {
        public CreateInterestAndObjective()
        {
            GetInterest = new List<GetInterest>();
            GetPersonalityTest = new List<GetPersonalityTest>();
        }

        public List<GetInterest> GetInterest { get; set; }
        public List<GetPersonalityTest> GetPersonalityTest { get; set; }

        public int GoalId { get; set; }
        public int ClientId { get; set; }
        public string CareGoal { get; set; }
        public string ClientName { get; set; }
        public string Brief { get; set; }

        public int InterestCount { get; set; }
        public int PersonalityCount { get; set; }
    }
}
