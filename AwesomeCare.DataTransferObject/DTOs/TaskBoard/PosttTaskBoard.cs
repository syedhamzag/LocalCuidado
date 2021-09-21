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
        public string TaskName { get; set; }
        public int AssignedBy { get; set; }
        public string TaskImage { get; set; }
        public string Attachment { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }


        public List<PostTaskBoardAssignedTo> AssignedTo { get; set; }
    }
}
