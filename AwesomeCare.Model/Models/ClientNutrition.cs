using AwesomeCare.DataTransferObject.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientNutrition
    {
        public int NutritionId { get; set; }
        public int ClientId { get; set; }
        public int StaffId { get; set; }
        public DateTime DATEFROM { get; set; }
        public DateTime DATETO { get; set; }
        public string MealSpecialNote { get; set; }
        public string ShoppingSpecialNote { get; set; }
        public string CleaningSpecialNote { get; set; }

        public virtual Client Client { get; set; }
        public virtual StaffPersonalInfo Staff { get; set; }

        public virtual ICollection<ClientShopping> ClientShopping { get; set; }
        public virtual ICollection<ClientCleaning> ClientCleaning { get; set; }
        public virtual ICollection<ClientMealDays> ClientMealDays { get; set; }

    }
}
