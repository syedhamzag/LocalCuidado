using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffOneToOne
{
    public class GetOneToOneOfficerToAct
    {
        public int OneToOneOfficerToActId { get; set; }
        public int OneToOneId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
