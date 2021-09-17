using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TaskBoard
{
    public class GetTaskBoard
    {
        public GetTaskBoard()
        {
            AssignedTo = new HashSet<GetTaskBoardAssignedTo>();
        }
        public int TaskId { get; set; }
        public int TaskName { get; set; }
        public int AssignedBy { get; set; }
        public int TaskImage { get; set; }
        public int Attachment { get; set; }
        public int CompletionDate { get; set; }
        public int Note { get; set; }
        public int Status { get; set; }


        public virtual ICollection<GetTaskBoardAssignedTo> AssignedTo { get; set; }
    }
}
