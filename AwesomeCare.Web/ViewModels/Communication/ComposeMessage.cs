using AwesomeCare.DataTransferObject.DTOs.Communication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.Communication
{
    public class ComposeMessage : PostCommunication
    {
        public ComposeMessage()
        {
            Staffs = new List<SelectListItem>();
        }

        public List<SelectListItem> Staffs { get; set; }
    }
}
