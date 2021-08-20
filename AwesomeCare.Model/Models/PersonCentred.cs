using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class PersonCentred
    {
        public PersonCentred()
        {
            Focus = new HashSet<PersonCentredFocus>();
        }

        public int PersonCentredId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Class { get; set; }
        public string ExpSupport { get; set; }

        public virtual ICollection<PersonCentredFocus> Focus { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
    }
}
