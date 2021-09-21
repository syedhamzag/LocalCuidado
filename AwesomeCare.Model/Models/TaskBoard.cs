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
        public string TaskName { get; set; }
        public int AssignedBy { get; set; }
        public string TaskImage { get; set; }
        public string Attachment { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }


        public virtual ICollection<TaskBoardAssignedTo> AssignedTo { get; set; }
    }
}
