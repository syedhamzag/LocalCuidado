using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred
{
    public class PutPersonCentred
    {
        public PutPersonCentred()
        {
            Focus = new List<PutPersonCentredFocus>();
        }
        public int PersonCentredId { get; set; }
        public int ClientId { get; set; }
        public int Class { get; set; }
        public string ExpSupport { get; set; }

        public List<PutPersonCentredFocus> Focus { get; set; }
    }
}
