using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class Pets
    {
        public int PetsId { get; set; }
        public int ClientId { get; set; }
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

        public virtual Client Client {get; set;}
    }
}
