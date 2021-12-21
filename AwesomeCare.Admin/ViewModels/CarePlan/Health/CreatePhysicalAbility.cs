using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.Health
{
    public class CreatePhysicalAbility
    {
        public int PhysicalId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mobility { get; set; }
        public int Status { get; set; }

        public string MobilityName { get; set; }
        public string StatusName { get; set; }
    }
}
