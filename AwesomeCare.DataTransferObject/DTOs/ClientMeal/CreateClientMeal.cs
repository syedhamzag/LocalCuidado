using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMeal
{
   public class CreateClientMeal
    {
        public CreateClientMeal()
        {
            ClientMealDays = new List<CreateClientMealDays>();
        }
        public int ClientMealId { get; set; }
        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client MealType is required")]
        public int ClientMealTypeId { get; set; }

        public List<CreateClientMealDays> ClientMealDays { get; set; }
    }
}
