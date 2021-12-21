using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PutBestInterestAssessment :BaseDTO
    {
        public PutBestInterestAssessment()
        {
            PutCareIssuesTask = new List<PutCareIssuesTask>();
            PutHealthTask = new List<PutHealthTask>();
            PutBelieveTask = new List<PutBelieveTask>();
            PutHealthTask2 = new List<PutHealthTask2>();

        }
        public int BestId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public List<PutCareIssuesTask> PutCareIssuesTask { get; set; }
        public List<PutHealthTask> PutHealthTask { get; set; }
        public List<PutBelieveTask> PutBelieveTask { get; set; }
        public List<PutHealthTask2> PutHealthTask2 { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Signature { get; set; }
    }
}
