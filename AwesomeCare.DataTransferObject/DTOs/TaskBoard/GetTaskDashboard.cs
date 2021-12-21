using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TaskBoard
{
    public class GetTaskDashboard
    {
        public GetTaskDashboard()
        {
            AssignedTo = new HashSet<GetTaskBoardAssignedTo>();
        }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int AssignedBy { get; set; }
        public string TaskImage { get; set; }
        public string Attachment { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }


        public virtual ICollection<GetTaskBoardAssignedTo> AssignedTo { get; set; }
    }
}
