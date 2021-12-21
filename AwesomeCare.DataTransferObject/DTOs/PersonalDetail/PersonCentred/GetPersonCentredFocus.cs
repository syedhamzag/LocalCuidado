using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred
{
    public class GetPersonCentredFocus
    {
        public int PersonCentredFocusId { get; set; }
        public int PersonCentredId { get; set; }
        public int BaseRecordId { get; set; }
        public string ValueName { get; set; }
    }
}
