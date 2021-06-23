using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientVoice
{
    public class GetVoiceOfficerToAct
    {
        public int VoiceOfficerToActId { get; set; }
        public int VoiceId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }

    }
}
