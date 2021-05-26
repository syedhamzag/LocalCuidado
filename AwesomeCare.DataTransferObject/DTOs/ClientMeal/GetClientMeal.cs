using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMeal
{
    public class GetClientMeal 
    {
        public GetClientMeal()
        {
            ClientMealDays = new List<GetClientMealDay>();
        }
        public int ClientMealId { get; set; }       
        public int ClientId { get; set; }
        public int ClientMealTypeId { get; set; }

        public List<GetClientMealDay> ClientMealDays { get; set; }
    }
}
