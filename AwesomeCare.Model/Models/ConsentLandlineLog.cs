using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ConsentLandlineLog
    {
        public int ConsentLandlineLogId { get; set; }
        public int LandlineId { get; set; }
        public int BaseRecordId { get; set; }

        public virtual ConsentLandLine ConsentLandLine { get; set; }
    }
}
