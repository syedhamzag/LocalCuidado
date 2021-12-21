using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare
{
    public class GetConsentCare
    {
        public int CareId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }
        public int Name { get; set; }
    }
}
