using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientNutrition
{
   public class PutClientNutrition
    {
        public PutClientNutrition()
        {
            ClientMealDays = new List<CreateClientMealDays>();
            ClientShopping = new List<CreateClientShopping>();
            ClientCleaning = new List<CreateClientCleaning>();
        }
        [Required]
        public int NutritionId { get; set; }
        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public DateTime DATEFROM { get; set; }
        [Required]
        public DateTime DATETO { get; set; }
        [Required]
        public string MealSpecialNote { get; set; }
        [Required]
        public string ShoppingSpecialNote { get; set; }
        [Required]
        public string CleaningSpecialNote { get; set; }

        public List<CreateClientMealDays> ClientMealDays { get; set; }
        public List<CreateClientShopping> ClientShopping { get; set; }
        public List<CreateClientCleaning> ClientCleaning { get; set; }
    }
}
