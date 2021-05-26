using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMealDays
{
  public  class PutClientMealDays
    {
        [Required]
        public int ClientMealId { get; set; }
        [Required(ErrorMessage ="please provide  ClientMeal")]
        public int NutritionId { get; set; }
        [Required(ErrorMessage ="Please provide Day of Week")]
        public int MealDayofWeekId { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public string MEALDETAILS { get; set; }
        [Required]
        public string HOWTOPREPARE { get; set; }
        [Required]
        public string SEEVIDEO { get; set; }
        [Required]
        public string PICTURE { get; set; }
        [Required(ErrorMessage = "Please provide Meal")]
        public int MealId { get; set; }
    }
}
