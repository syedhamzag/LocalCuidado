using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.TaskBoard
{
    public class PostTaskBoard
    {
        public PostTaskBoard()
        {
            AssignedTo = new List<PostTaskBoardAssignedTo>();
        }
        public int TaskId { get; set; }
        public int TaskName { get; set; }
        public int AssignedBy { get; set; }
        public int TaskImage { get; set; }
        public int Attachment { get; set; }
        public int CompletionDate { get; set; }
        public int Note { get; set; }
        public int Status { get; set; }


        public List<PostTaskBoardAssignedTo> AssignedTo { get; set; }
    }
}
