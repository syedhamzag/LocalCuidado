using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMealType
{
   public class GetClientMealType:BaseDTO
    {
        public int ClientMealTypeId { get; set; }
        public string MealType { get; set; }
    }
}
