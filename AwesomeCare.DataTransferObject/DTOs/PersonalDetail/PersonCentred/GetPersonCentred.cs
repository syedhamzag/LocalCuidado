using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred
{
    public class GetPersonCentred
    {
        public GetPersonCentred()
        {
            Focus = new List<GetPersonCentredFocus>();
        }
        public int PersonCentredId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Class { get; set; }
        public string ExpSupport { get; set; }

        public List<GetPersonCentredFocus> Focus { get; set; }
    }
}
