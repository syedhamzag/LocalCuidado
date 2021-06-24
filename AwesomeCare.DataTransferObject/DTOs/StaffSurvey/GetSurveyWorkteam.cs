using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffSurvey
{
    public class GetSurveyWorkteam
    {
        public int SurveyWorkteamId { get; set; }
        public int SurveyId { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public string StaffName { get; set; }

    }
}
