using System;
using System.Collections.Generic;
using System.Text;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;

namespace AwesomeCare.DataTransferObject.DTOs.ClientMeal
{
    public class GetClientNutrition 
    {
        public GetClientNutrition()
        {
            ClientMealDays = new List<GetClientMealDays>();
            ClientShopping = new List<GetClientShopping>();
            ClientCleaning = new List<GetClientCleaning>();
        }
        public int NutritionId { get; set; }       
        public int ClientId { get; set; }
        public int StaffId { get; set; }
        public DateTime DATEFROM { get; set; }
        public DateTime DATETO { get; set; }
        public string MealSpecialNote { get; set; }
        public string ShoppingSpecialNote { get; set; }
        public string CleaningSpecialNote { get; set; }

        public virtual GetClient Client { get; set; }
        public virtual GetStaffPersonalInfo Staff { get; set; }
        public List<GetClientMealDays> ClientMealDays { get; set; }
        public List<GetClientShopping> ClientShopping { get; set; }
        public List<GetClientCleaning> ClientCleaning { get; set; }
    }
}
