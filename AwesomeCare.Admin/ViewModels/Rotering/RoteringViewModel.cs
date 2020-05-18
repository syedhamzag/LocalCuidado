using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class RoteringViewModel
    {
        public string ActionName { get; set; } = "Save";
        public RoteringViewModel()
        {
            WeekDays = new List<GetRotaDayofWeek>();
            RotaTypes = new List<GetClientRotaType>();
            Rotas = new List<SelectListItem>();
            RotaTasks = new List<SelectListItem>();
            ClientRotas = new List<GetClientRota>();
        }
        public int ClientId { get; set; }
        public List<GetRotaDayofWeek> WeekDays { get; set; }
        public List<GetClientRotaType> RotaTypes { get; set; }
        public List<SelectListItem> RotaTasks { get; set; }

        public List<SelectListItem> Rotas { get; set; }

        public List<GetClientRota> ClientRotas { get; set; }
    }
}
