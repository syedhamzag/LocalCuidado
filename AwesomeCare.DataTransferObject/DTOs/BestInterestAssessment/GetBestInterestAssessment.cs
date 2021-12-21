using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class GetBestInterestAssessment : BaseDTO
    {
        public GetBestInterestAssessment()
        {
            GetCareIssuesTask = new List<GetCareIssuesTask>();
            GetHealthTask = new List<GetHealthTask>();
            GetBelieveTask = new List<GetBelieveTask>();
            GetHealthTask2 = new List<GetHealthTask2>();

        }
        public int BestId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public List<GetCareIssuesTask> GetCareIssuesTask { get; set; }
        public List<GetHealthTask> GetHealthTask { get; set; }
        public List<GetBelieveTask> GetBelieveTask { get; set; }
        public List<GetHealthTask2> GetHealthTask2 { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Signature { get; set; }
    }
}
