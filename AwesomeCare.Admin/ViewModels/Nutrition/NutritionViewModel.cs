using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientMeal;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientMealType;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Nutrition
{
    public class NutritionViewModel
    {
        public string ActionName { get; set; } = "Save";
        public NutritionViewModel()
        {
            STAFF = new List<GetStaffs>();
            WeekDays = new List<GetRotaDayofWeek>();
            MealTypes = new List<GetClientMealType>();
            ClientMealDays = new List<GetClientMealDays>();
            ClientShopping = new List<GetClientShopping>();
            ClientCleaning = new List<GetClientCleaning>();
        }
        public int NutritionId { get; set; }
        [Required]
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

        public int CleaningRowCount { get; set; }
        public int ShoppingRowCount { get; set; }
        public string ClientImage { get; set; }
        public string PlannerImage { get; set; }
        public string ClientName { get; set; }
        public string PlannerName { get; set; }
        public string PlannerContact { get; set; }
        public string ShoppingStaffName { get; set; }
        public string CleaningStaffName { get; set; }
        public List<GetStaffs> STAFF { get; set; }
        public List<GetRotaDayofWeek> WeekDays { get; set; }
        public List<GetClientMealType> MealTypes { get; set; }


        public List<GetClientMealDays> ClientMealDays { get; set; }
        public List<GetClientShopping> ClientShopping { get; set; }
        public List<GetClientCleaning> ClientCleaning { get; set; }
    }
}
