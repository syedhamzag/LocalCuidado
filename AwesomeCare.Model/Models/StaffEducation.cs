using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffEducation
    {
        public int StaffEducationId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Institution { get; set; }
        public string Certificate { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CertificateAttachment { get; set; }

        public virtual StaffPersonalInfo Staff { get; set; }
    }
}
