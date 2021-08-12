using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData
{
    public class PostConsentData
    {
        public int DataId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Signature { get; set; }
        public int Name { get; set; }
        public DateTime Date { get; set; }
    }
}
