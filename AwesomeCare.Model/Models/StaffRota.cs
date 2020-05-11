using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffRota
    {
        public StaffRota()
        {
            StaffRotaPeriods = new HashSet<StaffRotaPeriod>();
            StaffRotaPartners = new HashSet<StaffRotaPartner>();
            StaffRotaItem = new HashSet<StaffRotaItem>();
        }
        public int StaffRotaId { get; set; }
        public DateTime RotaDate { get; set; }
        public int? RotaDayofWeekId { get; set; }
        public int Staff { get; set; }
        public int RotaId { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }
        public virtual Rota Rota { get; set; }
        public virtual ICollection<StaffRotaPeriod> StaffRotaPeriods { get; set; }
        public virtual ICollection<StaffRotaPartner> StaffRotaPartners { get; set; }
        public virtual ICollection<StaffRotaItem> StaffRotaItem { get; set; }
    }
}
