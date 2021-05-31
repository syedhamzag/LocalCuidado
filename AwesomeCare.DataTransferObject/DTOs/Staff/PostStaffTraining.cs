using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class PostStaffTraining
    {
        public int StaffPersonalInfoId { get; set; }
        public string Training { get; set; }
        public string Certificate { get; set; }
        public string Location { get; set; }
        public string Trainer { get; set; }
        public string StartDate { get; set; }
        public string ExpiredDate { get; set; }
        public string CertificateAttachment { get; set; }
    }
}
