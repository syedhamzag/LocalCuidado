﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientMgtVisit
    {
        public ClientMgtVisit()
        {
            StaffName = new HashSet<VisitStaffName>();
            OfficerToAct = new HashSet<VisitOfficerToAct>();
        }

        public int VisitId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public int RateServiceRecieving {get; set;}
        public int RateManagers {get; set;}
        public int HowToComplain {get; set;}
        public int ServiceRecommended {get; set;}
        public string ImprovementExpect {get; set;}
        public string Observation {get; set;}
        public string ActionRequired {get; set;}
        public string ActionsTakenByMPCC {get; set;}
        public string EvidenceOfActionTaken {get; set;}
        public DateTime Deadline { get; set; }
        public string RotCause{ get; set; }
        public string LessonLearntAndShared {get; set;}
        public int Status {get; set;}
        public string Remarks {get; set;}
        public string URL {get; set;}
        public string Attachment { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<VisitOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<VisitStaffName> StaffName { get; set; }
    }
}
