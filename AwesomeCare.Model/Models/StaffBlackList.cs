using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffBlackList
    {
        public int StaffBlackListId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
        public virtual Client Client { get; set; }
    }
}
