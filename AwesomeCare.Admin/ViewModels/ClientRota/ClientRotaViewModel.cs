using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientRota
{
    public class ClientRotaViewModel
    {
        public ClientRotaViewModel()
        {
            Rotas = new List<GetClientRota>();
            Genders = new List<SelectListItem>
            {
                new SelectListItem{Text ="Male",Value="Male"},
                new SelectListItem{Text ="Female",Value="Female"}
            };
        }

        public string SubTitle { get; set; } = "Add Rota";
        [Required]
        public int RotaId { get; set; }
        [Display(Name = "Number of Staff")]
        [Required]
        public int NumberOfStaff { get; set; }
        [Required]
        [Display(Name = "Rota Name")]
        public string RotaName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Area { get; set; }
        public List<GetClientRota> Rotas { get; set; }
        public bool Deleted { get; set; } = false;
        public List<SelectListItem> Genders { get; set; }
    }
}
