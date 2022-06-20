using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class CareObjPersonToAct
    {
        public int Id { get; set; }
        public int CareObjId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public virtual ClientCareObj ClientCareObj { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
