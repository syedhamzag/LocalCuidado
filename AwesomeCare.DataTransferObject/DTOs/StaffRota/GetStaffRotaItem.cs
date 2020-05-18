using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
   public class GetStaffRotaItem
    {
        public int StaffRotaItemId { get; set; }
        public int StaffRotaId { get; set; }
        public string ItemName { get; set; }
    }
}
