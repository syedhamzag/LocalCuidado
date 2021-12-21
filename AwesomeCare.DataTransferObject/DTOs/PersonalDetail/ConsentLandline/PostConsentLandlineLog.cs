using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline
{
    public class PostConsentLandlineLog
    {
        public int ConsentLandlineLogId { get; set; }
        public int LandlineId { get; set; }
        public int BaseRecordId { get; set; }
    }
}
