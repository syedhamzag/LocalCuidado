using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare
{
    public class PutConsentCare
    {
        public int CareId { get; set; }
        public int ClientId { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }
    }
}
