using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline
{
    public class PutConsentLandLine
    {
        public int LandlineId { get; set; }
        public int ClientId { get; set; }
        public int LogMethod { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }
    }
}
