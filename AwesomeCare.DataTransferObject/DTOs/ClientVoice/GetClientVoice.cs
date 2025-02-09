﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientVoice
{
    public class GetClientVoice
    {
        public GetClientVoice()
        {
            OfficerToAct = new List<GetVoiceOfficerToAct>();
            CallerName = new List<GetVoiceCallerName>();
            GoodStaff = new List<GetVoiceGoodStaff>();
            PoorStaff = new List<GetVoicePoorStaff>();
        }
        public int VoiceId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public int RateServiceRecieving { get; set; }
        public int RateStaffAttending { get; set; }
        public int OfficeStaffSupport { get; set; }
        public string AreasOfImprovements { get; set; }
        public string SomethingSpecial { get; set; }
        public int InterestedInPrograms { get; set; }
        public string HealthGoalShortTerm { get; set; }
        public string HealthGoalLongTerm { get; set; }
        public string ActionRequired { get; set; }
        public string ActionsTakenByMPCC { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        public int Status { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public string RotCause { get; set; }
        public string LessonLearntAndShared { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public List<GetVoiceOfficerToAct> OfficerToAct { get; set; }
        public List<GetVoiceCallerName> CallerName { get; set; }
        public List<GetVoiceGoodStaff> GoodStaff { get; set; }
        public List<GetVoicePoorStaff> PoorStaff { get; set; }
    }
}
