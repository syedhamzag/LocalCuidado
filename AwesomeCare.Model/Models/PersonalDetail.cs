using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class PersonalDetail
    {
        public PersonalDetail()
        {
          Capacity  = new HashSet<Capacity>();
          ConsentCare  = new HashSet<ConsentCare>();
          ConsentData   = new HashSet<ConsentData>();
          ConsentLandLine = new HashSet<ConsentLandLine>();
          Equipment = new HashSet<Equipment>();
          KeyIndicators = new HashSet<KeyIndicators>();
          Personal = new HashSet<Personal>();
          PersonCentred = new HashSet<PersonCentred>();
          Review = new HashSet<Review>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Capacity> Capacity { get; set; }
        public virtual ICollection<ConsentCare> ConsentCare { get; set; }
        public virtual ICollection<ConsentData> ConsentData { get; set; }
        public virtual ICollection<ConsentLandLine> ConsentLandLine { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<KeyIndicators> KeyIndicators { get; set; }
        public virtual ICollection<Personal> Personal { get; set; }
        public virtual ICollection<PersonCentred> PersonCentred { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
