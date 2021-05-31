using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMealType
{
   public class PutClientMealType : BaseDTO
    {
        [Required]
        public int ClientMealTypeId { get; set; }
        [Required]
        public string MealType { get; set; }
    }
}
