using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class PersonalDetail
    {
        public PersonalDetail() 
        {            
            Personal = new HashSet<Personal>();
            ConsentCare = new HashSet<ConsentCare>();
            ConsentData = new HashSet<ConsentData>();
            ConsentLandLine = new HashSet<ConsentLandLine>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public virtual ICollection<Personal> Personal { get; set; }
        public virtual ICollection<ConsentLandLine> ConsentLandLine { get; set; }
        public virtual ICollection<ConsentCare> ConsentCare { get; set; }
        public virtual ICollection<ConsentData> ConsentData { get; set; }

        public virtual Client Client { get; set; }
    }
}
