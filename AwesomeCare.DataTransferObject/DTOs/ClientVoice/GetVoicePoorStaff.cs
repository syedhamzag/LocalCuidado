using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientVoice
{
    public class GetVoicePoorStaff
    {
        public int VoicePoorStaffId { get; set; }
        public int VoiceId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public string StaffName { get; set; }

    }
}
