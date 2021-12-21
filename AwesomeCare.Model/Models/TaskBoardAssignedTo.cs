using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class TaskBoardAssignedTo
    {
        public int TaskBoardAssignedToId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int TaskBoardId { get; set; }


        public virtual TaskBoard TaskBoard { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
