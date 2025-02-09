﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators
{
    public class PostKeyIndicators
    {
        public PostKeyIndicators()
        {
            LogMethod = new List<PostKeyIndicatorLog>();
        }
        public int PersonalDetailId { get; set; }
        public int KeyId { get; set; }
        public string AboutMe { get; set; }
        public string FamilyRole { get; set; }
        public int LivingStatus { get; set; }
        public int Debture { get; set; }
        public string ThingsILike { get; set; }

        public List<PostKeyIndicatorLog> LogMethod { get; set; }
    }
}
