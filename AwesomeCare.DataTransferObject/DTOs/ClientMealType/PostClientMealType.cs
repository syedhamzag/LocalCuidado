using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMealType
{
   public class PostClientMealType : BaseDTO
    {
        [Required]
        public string MealType { get; set; }
    }
}
