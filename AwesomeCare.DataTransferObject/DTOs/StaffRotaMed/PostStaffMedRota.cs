using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRotaMed
{
  public  class PostStaffMedRota
    {
        public PostStaffMedRota()
        {
            StaffMedRotaPeriods = new List<PostStaffMedRotaPeriod>();
        }
        public DateTime RotaDate { get; set; }
        public int Staff { get; set; }
        public int RotaId { get; set; }
        public int? RotaDayofWeekId { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }
        public string DoseGiven { get; set; }
        public string Time { get; set; }
        public string Measurement { get; set; }
        public string Location { get; set; }
        public string Feedback { get; set; }

        public  List<PostStaffMedRotaPeriod> StaffMedRotaPeriods { get; set; }
    }
}
