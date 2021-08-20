using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ConsentLandLine
    {
        public ConsentLandLine()
        {
            LogMethod = new HashSet<ConsentLandlineLog>();
        }
        
        public int LandlineId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Name { get; set; }
        public int Signature { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<ConsentLandlineLog> LogMethod { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
    }
}
