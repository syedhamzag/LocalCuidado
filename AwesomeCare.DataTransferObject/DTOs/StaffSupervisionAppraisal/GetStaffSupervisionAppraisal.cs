using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffSupervisionAppraisal
{
    public class GetStaffSupervisionAppraisal
    {
        public int StaffSupervisionAppraisalId { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public string Details { get; set; }
        public int WorkTeam { get; set; }
        public int StaffRating { get; set; }
        public int ProfessionalDevelopment { get; set; }
        public int StaffComplaints { get; set; }
        public string FiveStarRating { get; set; }
        public string StaffDevelopment { get; set; }
        public string StaffAbility { get; set; }
        public string NoAbilityToSupport { get; set; }
        public string CondourAndWhistleBlowing { get; set; }
        public string NoCondourAndWhistleBlowing { get; set; }
        public int StaffSupportAreas { get; set; }
        public string ActionRequired { get; set; }
        public int OfficerToAct { get; set; }
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
