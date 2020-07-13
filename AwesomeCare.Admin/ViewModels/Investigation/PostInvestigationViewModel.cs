using AwesomeCare.DataTransferObject.DTOs.Investigation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Investigation
{
    public class PostInvestigationViewModel:PostInvestigation
    {
        public PostInvestigationViewModel()
        {
            Staffs = new List<SelectListItem>();
            Clients = new List<SelectListItem>();
        }

        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> Clients { get; set; }
        public IFormFileCollection  Files { get; set; }
    }
}
