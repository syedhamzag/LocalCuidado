using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class RoteringViewModel
    {
        public RoteringViewModel()
        {
            WeekDays = new List<string>
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
            RotaTypes = new List<GetClientRotaType>();
            Rotas = new List<SelectListItem>();
        }
        public List<string> WeekDays { get; set; }
        public List<GetClientRotaType> RotaTypes { get; set; }

        public List<SelectListItem> Rotas { get; set; }
    }
}
