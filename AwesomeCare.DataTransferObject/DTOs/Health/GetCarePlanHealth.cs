using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Health
{
    public class GetCarePlanHealth
    {
        public GetCarePlanHealth()
        {
            Balance = new List<GetBalance>();
            PhysicalAbility = new List<GetPhysicalAbility>();
            HistoryOfFall = new List<GetHistoryOfFall>();
            HealthAndLiving = new List<GetHealthAndLiving>();
            SpecialHealthCondition = new List<GetSpecialHealthCondition>();
            SpecialHealthAndMedication = new List<GetSpecialHealthAndMedication>();
        }
        public int HealthId { get; set; }
        public int ClientId { get; set; }

        public List<GetBalance> Balance { get; set; }
        public List<GetPhysicalAbility> PhysicalAbility { get; set; }
        public List<GetHistoryOfFall> HistoryOfFall { get; set; }
        public List<GetHealthAndLiving> HealthAndLiving { get; set; }
        public List<GetSpecialHealthCondition> SpecialHealthCondition { get; set; }
        public List<GetSpecialHealthAndMedication> SpecialHealthAndMedication { get; set; }
    }
}
