﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class DutyOnCall
    {
        public DutyOnCall()
        {
            PersonResponsible = new HashSet<DutyOnCallPersonResponsible>();
            PersonToAct = new HashSet<DutyOnCallPersonToAct>();
        }
        public int DutyOnCallId { get; set; }
        public string RefNo { get; set; }
        public int TypeOfDutyCall { get; set; }
        public string Subject { get; set; }
        public int ClientId { get; set; }
        public string ClientInitial { get; set; }
        public DateTime DateOfIncident { get; set; }
        public int TypeOfIncident { get; set; }
        public  string ReportedBy { get; set; }
        public  int TelephoneToCall { get; set; }
        public  int PositionOfReporting { get; set; }
        public DateTime DateOfCall { get; set; }
        public DateTime TimeOfCall { get; set; }
        public string DetailsOfIncident { get; set; }
        public string ActionTaken { get; set; }
        public string DetailsRequired { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public int NotificationStatus { get; set; }
        public string Remarks { get; set; }
        public bool NotifyPerson { get; set; }
        public bool StaffBlacklisted { get; set; }
        public bool NotifyStaffInvolved { get; set; }
        public string Attachment { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<DutyOnCallPersonResponsible> PersonResponsible { get; set; }
        public virtual ICollection<DutyOnCallPersonToAct> PersonToAct { get; set; }

    }
}
