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

namespace AwesomeCare.Admin.ViewModels.Resources
{
    public class CreateResources
    {
        public CreateResources()
        {
            ClientList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }

        public ICollection<SelectListItem> ClientList { get; set; }
        public int ResourcesId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int PublishTo { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string PublishBy { get; set; }
        [Required]
        public string Video { get; set; }

        public string Image { get; set; }
    }
}
