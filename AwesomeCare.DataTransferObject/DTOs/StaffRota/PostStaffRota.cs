using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRota
{
  public  class PostStaffRota
    {
        public PostStaffRota()
        {
            StaffRotaPeriods = new List<PostStaffRotaPeriod>();
            StaffRotaPartners = new List<PostStaffRotaPartner>();
            StaffRotaItem = new List<PostStaffRotaItem>();
        }
        public DateTime RotaDate { get; set; }
        public int Staff { get; set; }
        public int RotaId { get; set; }
        public string Remark { get; set; }
        public string ReferenceNumber { get; set; }

        public  List<PostStaffRotaPeriod> StaffRotaPeriods { get; set; }
        public  List<PostStaffRotaPartner> StaffRotaPartners { get; set; }
        public  List<PostStaffRotaItem> StaffRotaItem { get; set; }
    }
}
