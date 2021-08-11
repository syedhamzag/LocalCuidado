using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline
{
    public class PostConsentLandLine
    {
        public int LandlineId { get; set; }
        public int PersonalDetailId { get; set; }
        public int LogMethod { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }
    }
}
