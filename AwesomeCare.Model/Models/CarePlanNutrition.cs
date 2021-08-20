using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class CarePlanNutrition
    {
        public int NutritionId { get; set; }
        public int ClientId { get; set; }
        public int FoodStorage { get; set; }
        public string ServingMeal { get; set; }
        public string WhenRestock { get; set; }
        public int WhoRestock { get; set; }
        public string SpecialDiet { get; set; }
        public string DrinkType { get; set; }
        public string AvoidFood { get; set; }
        public int ThingsILike { get; set; }
        public int FoodIntake { get; set; }
        public int MealPreparation { get; set; }
        public int EatingDifficulty { get; set; }
        public string RiskMitigations { get; set; }

        public virtual Client Client { get; set; }
    }
}
