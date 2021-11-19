using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffInterview
{
    public class GetStaffInterview : BaseDTO
    {
        public GetStaffInterview()
        {
            GetStaffInterviewTask = new List<GetStaffInterviewTask>();
        }
        public int StaffInterviewId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }



        public List<GetStaffInterviewTask> GetStaffInterviewTask { get; set; }
    }
}
