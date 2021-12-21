using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.Client;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;

namespace AwesomeCare.Admin.ViewModels.WhisttleBlower
{
    public class CreateWhisttleBlower
    {
        public CreateWhisttleBlower()
        {
            ClientList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> ClientList { get; set; }
        public int WhisttleBlowerId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int UserName { get; set; }
        [Required]
        public string StaffName { get; set; }
        [Required]
        public string IncidentDate { get; set; }
        [Required]
        public string Happening { get; set; }
        [Required]
        public int Witness { get; set; }
        [Required]
        public int LikeCalling { get; set; }
        [Required]
        public int Status { get; set; }

        public string Evidence { get; set; }
    }
}
