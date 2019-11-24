using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientRotaType
{
    public class ClientRotaTypeViewModel
    {
        public ClientRotaTypeViewModel()
        {
            RotaTypes = new List<GetClientRotaType>();           
        }

        public string SubTitle { get; set; } = "Add RotaType";
        [Required]
        public int ClientRotaTypeId { get; set; }
        [Required]
        [Display(Name = "Rota Type")]
        public string RotaType { get; set; }
        public bool Deleted { get; set; } = false;
        public List<GetClientRotaType> RotaTypes { get; set; }
    }
}
