using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class StaffRotaPartner
    {
        public int StaffRotaPartnerId { get; set; }
        public int StaffRotaId { get; set; }
        public int StaffId { get; set; }

        public virtual StaffRota StaffRota { get; set; }
    }
}
