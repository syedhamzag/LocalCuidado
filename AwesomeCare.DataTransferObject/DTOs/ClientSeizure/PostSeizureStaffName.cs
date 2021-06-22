using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientSeizure
{
    public class PostSeizureStaffName
    {
        public int SeizureOfficerToActId { get; set; }
        public int SeizureId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
