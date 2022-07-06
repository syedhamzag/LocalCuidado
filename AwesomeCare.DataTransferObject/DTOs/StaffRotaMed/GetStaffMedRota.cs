using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
   public class GetStaffMedRota
    {
        public GetStaffMedRota()
        {
            Periods = new List<GetStaffRotaPeriod>();
        }
        public DateTime RotaDate { get; set; }
        public int StaffId { get; set; }
        public string Staff { get; set; }
        public int RotaId { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }

        public List<GetStaffRotaPeriod> Periods { get; set; }
    }
}
