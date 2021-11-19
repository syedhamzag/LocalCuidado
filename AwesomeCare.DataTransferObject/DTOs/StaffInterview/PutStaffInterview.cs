using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffInterview
{
    public class PutStaffInterview:BaseDTO
    {
        public PutStaffInterview()
        {
            PutStaffInterviewTask = new List<PutStaffInterviewTask>();
        }
        public int StaffInterviewId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Heading { get; set; }


        public List<PutStaffInterviewTask> PutStaffInterviewTask { get; set; }
    }
}
