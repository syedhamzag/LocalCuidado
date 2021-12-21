using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators
{
    public class PutKeyIndicators
    {
        public PutKeyIndicators()
        {
            LogMethod = new List<PutKeyIndicatorLog>();
        }
        public int PersonalDetailId { get; set; }
        public int KeyId { get; set; }
        public string AboutMe { get; set; }
        public string FamilyRole { get; set; }
        public int LivingStatus { get; set; }
        public int Debture { get; set; }
        public string ThingsILike { get; set; }

        public List<PutKeyIndicatorLog> LogMethod { get; set; }
    }
}
