using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffHealth
{
    public class PostStaffHealth : BaseDTO
    {
        public PostStaffHealth()
        {
            PostStaffHealthTask = new List<PostStaffHealthTask>();
        }
        public int StaffHealthId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PostStaffHealthTask> PostStaffHealthTask { get; set; }
    }
}
