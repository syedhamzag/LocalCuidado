using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffOfficeLocation
    {
        public int Id { get; set; }
        public int Staff { get; set; }
        public int Location { get;set; }

        public virtual OfficeLocation OfficeLocation { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
