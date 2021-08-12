using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class PersonalDetail
    {
        public PersonalDetail()
        {
          Equipment = new HashSet<Equipment>();
          PersonCentred = new HashSet<PersonCentred>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Capacity Capacity { get; set; }
        public virtual ConsentCare ConsentCare { get; set; }
        public virtual ConsentData ConsentData { get; set; }
        public virtual ConsentLandLine ConsentLandLine { get; set; }     
        public virtual KeyIndicators KeyIndicators { get; set; }
        public virtual Personal Personal { get; set; }
        public virtual Review Review { get; set; }
        public virtual ICollection<PersonCentred> PersonCentred { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
