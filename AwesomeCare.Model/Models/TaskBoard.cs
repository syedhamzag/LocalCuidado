using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public  class TaskBoard
    {
        public TaskBoard()
        {
            AssignedTo = new HashSet<TaskBoardAssignedTo>();
        }
        public int TaskId { get; set; }
        public int TaskName { get; set; }
        public int AssignedBy { get; set; }
        public int TaskImage { get; set; }
        public int Attachment { get; set; }
        public int CompletionDate { get; set; }
        public int Note { get; set; }
        public int Status { get; set; }


        public virtual ICollection<TaskBoardAssignedTo> AssignedTo { get; set; }
    }
}
