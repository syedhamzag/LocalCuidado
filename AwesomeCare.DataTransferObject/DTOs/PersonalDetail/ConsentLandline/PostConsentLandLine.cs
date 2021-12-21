using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline
{
    public class PostConsentLandLine
    {
        public PostConsentLandLine()
        {
            LogMethod = new List<PostConsentLandlineLog>();
        }
        public int LandlineId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }
        public int Name { get; set; }

        public List<PostConsentLandlineLog> LogMethod { get; set; }
    }
}
