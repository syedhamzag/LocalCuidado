using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMeal
{
    public class PostClientMeal
    {
        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client MealType is required")]
        public int ClientMealTypeId { get; set; }
    }
}
