using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Client.CareObj
{
    public class PutCareObjPersonToAct
    {
        public int Id { get; set; }
        public int CareObjId { get; set; }
        public int StaffPersonalInfoId { get; set; }
    }
}
