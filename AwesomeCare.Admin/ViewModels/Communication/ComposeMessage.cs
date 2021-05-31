using AwesomeCare.DataTransferObject.DTOs.Communication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Communication
{
    public class ComposeMessage: PostCommunication
    {
        public ComposeMessage()
        {
            Staffs = new List<SelectListItem>();
        }
       

        public List<SelectListItem>  Staffs { get; set; }
    }
}
