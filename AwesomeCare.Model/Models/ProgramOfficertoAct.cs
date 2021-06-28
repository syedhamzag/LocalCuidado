using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class ProgramOfficerToAct
    {
        public int ProgramOfficerToActId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ProgramId { get; set; }

        public virtual ClientProgram Program { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
