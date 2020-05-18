using AwesomeCare.DataTransferObject.DTOs.Rotering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class RotaAdminViewModel
    {
        public RotaAdminViewModel()
        {
            RotaAdmin = new List<RotaAdmin>();
        }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public List<RotaAdmin> RotaAdmin { get; set; }
    }
}
