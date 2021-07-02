using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientNutrition;
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
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;

namespace AwesomeCare.Admin.ViewModels.Reporting
{
    public class ReportingViewModel
    {
        public ReportingViewModel()
        {
            ClientList = new List<SelectListItem>();
            RotaTasks = new List<SelectListItem>();
            ClientRotas = new List<GetClientRota>();
            RotaTypes = new List<GetClientRotaType>();
        }
        [Required]
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public string Month { get; set; }
        public string Year { get; set; }
        public List<SelectListItem> ClientList {get; set;}

        public string Date { get; set; }
        public List<GetClientRota> ClientRotas { get; set; }
        public List<GetClientRotaType> RotaTypes { get; set; }
        public List<SelectListItem> RotaTasks { get; set; }
    }
}
