using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.ClientService
{
    public class PostClientServiceViewModel: PostClientServiceDetail
    {
        public PostClientServiceViewModel()
        {
            Clients = new List<SelectListItem>();
        }

        public List<SelectListItem>  Clients { get; set; }
        [Display(Name = "Attachment, if any")]
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFileCollection Receipts { get; set; }

    }
}
