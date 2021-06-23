using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientOneToOne
{
    public class GetOneToOneOfficerToAct
    {
        public int OneToOneOfficerToActId { get; set; }
        public int OneToOneId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
