using AwesomeCare.DataTransferObject.DTOs.StaffBlackList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.BlackList
{
    public class CreateStaffBlackList:PostStaffBlackList
    {
        public CreateStaffBlackList()
        {
            Staffs = new List<SelectListItem>();
            Clients = new List<SelectListItem>();
        }
        public List<SelectListItem>  Staffs { get; set; }
        public List<SelectListItem>  Clients { get; set; }
    }
}
