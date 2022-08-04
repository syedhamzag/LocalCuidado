using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffOfficeLocation
{
    public class PutStaffOfficeLocation
    {
        public int Id { get; set; }
        public int Staff { get; set; }
        public int Location { get; set; }
    }
}
