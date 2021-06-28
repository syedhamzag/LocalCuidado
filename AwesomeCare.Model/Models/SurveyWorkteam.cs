using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class SurveyWorkteam
    {
        public int SurveyWorkteamId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int StaffSurveyId { get; set; }

        public virtual StaffSurvey Survey { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
