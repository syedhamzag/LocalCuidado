using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ConsentLandLine
    {
        public int LandlineId { get; set; }
        public int PersonalDetailId { get; set; }
        public int LogMethod { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }

        public virtual PersonalDetail Client { get; set; }
    }
}
