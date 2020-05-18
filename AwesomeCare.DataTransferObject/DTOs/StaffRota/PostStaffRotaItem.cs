using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
   public class PostStaffRotaItem
    {
        public int StaffRotaItemId { get; set; }
        public int StaffRotaId { get; set; }
        public int StaffRotaDynamicAdditionId { get; set; }
    }
}
