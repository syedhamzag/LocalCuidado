using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMealDays
{
   public class GetClientMealDays
    {
        public int ClientMealId { get; set; }
        public int NutritionId { get; set; }
        public int MealDayofWeekId { get; set; }
        public int TypeId { get; set; }
        public string MEALDETAILS { get; set; }
        public string HOWTOPREPARE { get; set; }
        public string SEEVIDEO { get; set; }
        public string PICTURE { get; set; }
        public int MealId { get; set; }
    }
}
