using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffShadowing
{
    public class PostStaffShadowing : BaseDTO
    {
        public PostStaffShadowing()
        {
            PostStaffShadowingTask = new List<PostStaffShadowingTask>();
        }
        public int StaffShadowingId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PostStaffShadowingTask> PostStaffShadowingTask { get; set; }
    }
}
