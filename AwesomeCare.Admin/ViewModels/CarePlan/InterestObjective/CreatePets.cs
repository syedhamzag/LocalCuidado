using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.InterestObjective
{
    public class CreatePets
    {
        public int PetsId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Age { get; set; }
        public int Gender { get; set; }
        public string PetActivities { get; set; }
        public int MealStorage { get; set; }
        public int VetVisit { get; set; }
        public int PetInsurance { get; set; }
        public string PetCare { get; set; }
        public string MealPattern { get; set; }
    }
}
