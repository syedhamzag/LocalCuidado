using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class KeyIndicators
    {
        public int KeyId { get; set; }
        public int PersonalDetailId { get; set; }
        public string AboutMe { get; set; }
        public string FamilyRole { get; set; }
        public int LivingStatus { get; set; }
        public int Debture { get; set; }
        public string ThingsILike { get; set; }
        public int LogMethod { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
    }
}
