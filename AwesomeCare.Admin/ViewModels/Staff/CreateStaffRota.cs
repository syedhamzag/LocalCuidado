using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffRota//: PostStaffRota
    {
        public CreateStaffRota()
        {
           
        }
        [ValidDateTime("MM/dd/yyyy", ErrorMessage ="Invalid {0}, format is {1}")]
        public string StartDate { get; set; }
        [ValidDateTime("MM/dd/yyyy", ErrorMessage = "Invalid {0}, format is {1}")]
        public string StopDate { get; set; }
       
    }
}
