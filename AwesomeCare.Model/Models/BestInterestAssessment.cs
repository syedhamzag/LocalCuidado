using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class BestInterestAssessment
    {
        public BestInterestAssessment()
        {
            CareIssuesTask = new HashSet<CareIssuesTask>();
            HealthTask = new HashSet<HealthTask>();
            BelieveTask = new HashSet<BelieveTask>();
            HealthTask2 = new HashSet<HealthTask2>();

        }
        public int BestId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CareIssuesTask> CareIssuesTask { get; set; }
        public ICollection<HealthTask> HealthTask { get; set; }
        public ICollection<BelieveTask> BelieveTask { get; set; }
        public ICollection<HealthTask2> HealthTask2 { get; set; }
        public virtual Client Client { get; set; }
        public string Name {get; set;}
        public string Position {get; set;}
        public string Signature {get; set;}


    }
}
