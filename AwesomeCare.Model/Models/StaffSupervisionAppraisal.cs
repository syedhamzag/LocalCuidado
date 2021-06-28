using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffSupervisionAppraisal
    {
        public StaffSupervisionAppraisal()
        {
            OfficerToAct = new HashSet<SupervisionOfficerToAct>();
            Workteam = new HashSet<SupervisionWorkteam>();
        }
        public int StaffSupervisionAppraisalId { get; set; }
        public string Reference { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextCheckDate { get; set; }
        public string Details { get; set; }
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
        public DateTime Deadline { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Attachment { get; set; }

        public virtual ICollection<SupervisionOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<SupervisionWorkteam> Workteam { get; set; }
    }
}
