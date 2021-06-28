﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class VoiceGoodStaff
    {
        public int VoiceGoodStaffId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int VoiceId { get; set; }

        public virtual ClientVoice Voice { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
