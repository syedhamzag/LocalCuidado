﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientVoice
    {
        public ClientVoice()
        {
            PoorStaff = new HashSet<VoicePoorStaff>();
            GoodStaff = new HashSet<VoiceGoodStaff>();
            CallerName = new HashSet<VoiceCallerName>();
            OfficerToAct = new HashSet<VoiceOfficerToAct>();
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

        public virtual Client Client { get; set; }
        public virtual ICollection<VoiceOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<VoiceCallerName> CallerName { get; set; }
        public virtual ICollection<VoiceGoodStaff> GoodStaff { get; set; }
        public virtual ICollection<VoicePoorStaff> PoorStaff { get; set; }
    }
}
