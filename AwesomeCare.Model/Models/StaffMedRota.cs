using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffMedRota
    {
        public int StaffRotaId { get; set; }
        public DateTime RotaDate { get; set; }
        public int? RotaDayofWeekId { get; set; }
        public int Staff { get; set; }
        public int RotaId { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }
        public string DoseGiven { get; set; }
        public string Time { get; set; }
        public string Measurement { get; set; }
        public string Location { get; set; }
        public string Feedback { get; set; }
        public virtual Rota Rota { get; set; }
    }
}
