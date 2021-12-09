using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks
{
    public class PostStaffTeamLeadTasks : BaseDTO
    {
        public int TeamLeadTaskId { get; set; }
        public int TeamLeadId { get; set; }
        public int Title { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
