using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMealDays
{
   public class CreateClientMealDays
    {
        public CreateClientMealDays()
        {
        }
        public int ClientMealId { get; set; }
        [Required(ErrorMessage = "please provide  ClientNutrition")]
        public int NutritionId { get; set; }
        [Required(ErrorMessage = "Please provide Day of Week")]
        public int MealDayofWeekId { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public string MEALDETAILS { get; set; }
        [Required]
        public string HOWTOPREPARE { get; set; }
        [Required]
        public string SEEVIDEO { get; set; }
        public string PICTURE { get; set; }
        [Required]
        public int MealId { get; set; }
    }
}
