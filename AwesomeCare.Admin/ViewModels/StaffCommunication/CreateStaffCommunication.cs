using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.StaffCommunication
{
    public class CreateStaffCommunication: PostStaffCommunication
    {
        public CreateStaffCommunication()
        {
            Staffs = new List<GetStaffs>() ;
        }
        public List<GetStaffs> Staffs { get; set; }
    }
}
