using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientProgram
{
    public class GetProgramOfficerToAct
    {
        public int ProgramOfficerToActId { get; set; }
        public int ProgramId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
