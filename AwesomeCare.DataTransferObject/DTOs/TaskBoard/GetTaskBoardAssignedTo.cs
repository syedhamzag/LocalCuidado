using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TaskBoard
{
    public class GetTaskBoardAssignedTo
    {
        public int TaskBoardAssignedToId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TaskBoardId { get; set; }
        public string StaffName { get; set; }
    }
}
