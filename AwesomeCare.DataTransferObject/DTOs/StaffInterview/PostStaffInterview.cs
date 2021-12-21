using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffInterview
{
    public class PostStaffInterview : BaseDTO
    {
        public PostStaffInterview()
        {
            PostStaffInterviewTask = new List<PostStaffInterviewTask>();
        }
        public int StaffInterviewId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PostStaffInterviewTask> PostStaffInterviewTask { get; set; }
    }
}
