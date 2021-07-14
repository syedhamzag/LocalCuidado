using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class PersonalDetail
    {
        public PersonalDetail() 
        {            
            ConsentCare = new HashSet<ConsentCare>();
            ConsentData = new HashSet<ConsentData>();
            ConsentLandLine = new HashSet<ConsentLandLine>();
            PersonCentred = new HashSet<PersonCentred>();
            Review = new HashSet<Review>();
            Capacity = new HashSet<Capacity>();
            KeyIndicator = new HashSet<KeyIndicators>();
            Equipment = new HashSet<Equipment>();
            Personal = new HashSet<Personal>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public virtual ICollection<ConsentLandLine> ConsentLandLine { get; set; }
        public virtual ICollection<ConsentCare> ConsentCare { get; set; }
        public virtual ICollection<ConsentData> ConsentData { get; set; }
        public virtual ICollection<PersonCentred> PersonCentred { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<Capacity> Capacity { get; set; }
        public virtual ICollection<KeyIndicators> KeyIndicator { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<Personal> Personal { get; set; }

        public virtual Client Client { get; set; }
    }
}
