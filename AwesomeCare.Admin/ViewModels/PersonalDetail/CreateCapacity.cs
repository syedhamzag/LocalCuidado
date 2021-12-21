using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateCapacity
    {
        public CreateCapacity()
        {
            IndicatorList = new List<SelectListItem>();
        }
        public List<SelectListItem> IndicatorList { get; set; }

        [Required]
        public int ClientId { get; set; } 
        [Required]
        public int CapacityId { get; set; }
        [Required]
        public List <int> Indicator { get; set; }
        [Required]
        public int Pointer { get; set; }
        [Required]
        public string Implications { get; set; }

    }
}
