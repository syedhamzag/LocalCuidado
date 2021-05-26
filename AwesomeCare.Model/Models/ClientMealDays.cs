using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientMealDays
    {
       
        public int ClientMealId { get; set; }
        public int NutritionId { get; set; }
        public int MealDayofWeekId { get; set; }
        public int MealId { get; set; }
        public int TypeId { get; set; }
        public string MEALDETAILS { get; set; }
        public string HOWTOPREPARE { get; set; }
        public string SEEVIDEO { get; set; }
        public string PICTURE { get; set; }

        public virtual ClientNutrition ClientNutrition { get; set; }
        public virtual RotaDayofWeek MealDayofWeek { get; set; }
        public virtual ClientMealType ClientMealType { get; set; }
    }
}
