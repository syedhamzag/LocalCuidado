using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Dashboard
{
    public class GetDashboard
    {
        public List<OnCall> OnCall { get; set; }

        public List<ConcernNotes> concernNotes { get; set; }
        public Dictionary<string, List<GetTaskBoard>> GetTaskBoard { get; set; }
        public int ActiveUser { get; set; }
        public int ApprovedStaff { get; set; }

        public List<Status> StaffRatingCount { get; set; }

        public Dictionary<string, List<GetStaffRating>> StaffRating { get; set; }
        public List<GetClient> GetClients { get; set; }
        public List<GetStaffPersonalInfo> GetStaffPersonalInfos { get; set; }
        public List<GetStaffPersonalInfo> GetAllStaff { get; set; }
        public int nId { get; set; }
        public int oId { get; set; }
        public int aId { get; set; }
        public int rId { get; set; }
        public int pId { get; set; }
        public int cId { get; set; }
        public int lId { get; set; }

        public int oncallP { get; set; }
        public int oncallC { get; set; }
        public int oncallO { get; set; }

        public int ConcernIdP { get; set; }
        public int ConcernIdC { get; set; }
        public int ConcernIdO { get; set; }

        public List<Status> OnCallGraph { get; set; }
        public List<Status> concernNoteGraph { get; set; }
        public List<Status> TeleHealth { get; set; }
        public List<Status> BloodCoag { get; set; }
        public List<Status> Pressure { get; set; }
        public List<Status> BMI { get; set; }
        public List<Status> Body { get; set; }
        public List<Status> Bowel { get; set; }
        public List<Status> EyeHealth { get; set; }
        public List<Status> Food { get; set; }
        public List<Status> Heart { get; set; }
        public List<Status> Oxygen { get; set; }
        public List<Status> Pain { get; set; }
        public List<Status> Pulse { get; set; }
        public List<Status> Seizure { get; set; }
        public List<Status> Wound { get; set; }
        public List<Status> ClientMatrix { get; set; }
        public List<Status> Complain { get; set; }
        public List<Status> LogAudit { get; set; }
        public List<Status> MedAudit { get; set; }
        public List<Status> Voice { get; set; }
        public List<Status> MgtVisit { get; set; }
        public List<Status> Program { get; set; }
        public List<Status> ServiceWatch { get; set; }
        public List<Status> StaffMatrix { get; set; }
        public List<Status> AdlObs { get; set; }
        public List<Status> KeyWorker { get; set; }
        public List<Status> MedComp { get; set; }
        public List<Status> OneToOne { get; set; }
        public List<Status> Reference { get; set; }
        public List<Status> SpotCheck { get; set; }
        public List<Status> Supervision { get; set; }
        public List<Status> Survey { get; set; }
        public Dictionary<string, List<Status>> LiveTrackerM { get; set; }
        public Dictionary<string, List<Status>> LiveTrackerW { get; set; }
        public Dictionary<string, List<Status>> LiveTrackerD { get; set; }
        public List<string> Months { get; set; }
        public List<string> Days { get; set; }
        public List<string> Types { get; set; }
    }
    public class Status
    {
        public string Key { get; set; }
        public int Value { get; set; }

    }
}
