using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TaskBoard
{
    public class PutTaskBoardAssignedTo
    {
        public int TaskBoardAssignedToId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TaskBoardId { get; set; }
    }
}
