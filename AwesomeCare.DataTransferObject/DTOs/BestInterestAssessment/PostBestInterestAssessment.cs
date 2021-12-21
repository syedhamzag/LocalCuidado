using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment
{
    public class PostBestInterestAssessment : BaseDTO
    {
        public PostBestInterestAssessment()
        {
            PostCareIssuesTask = new List<PostCareIssuesTask>();
            PostHealthTask = new List<PostHealthTask>();
            PostBelieveTask = new List<PostBelieveTask>();
            PostHealthTask2 = new List<PostHealthTask2>();

        }
        public int BestId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public List<PostCareIssuesTask> PostCareIssuesTask { get; set; }
        public List<PostHealthTask> PostHealthTask { get; set; }
        public List<PostBelieveTask> PostBelieveTask { get; set; }
        public List<PostHealthTask2> PostHealthTask2 { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Signature { get; set; }
    }
}
