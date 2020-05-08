using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
   public class GetStaffRotaPartner
    {
        public int StaffRotaPartnerId { get; set; }
        public int StaffRotaId { get; set; }
        public int PartnerId { get; set; }
        public string Partner { get; set; }
        public string Telephone { get; set; }
    }
}
