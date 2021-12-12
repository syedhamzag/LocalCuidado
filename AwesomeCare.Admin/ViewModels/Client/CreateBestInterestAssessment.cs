using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateBestInterestAssessment
    {
        public CreateBestInterestAssessment()
        {
            GetCareIssuesTask = new List<GetCareIssuesTask>();
            GetHealthTask = new List<GetHealthTask>();
            GetBelieveTask = new List<GetBelieveTask>();
            GetHealthTask2 = new List<GetHealthTask2>();
            ClientList = new List<SelectListItem>();
        }
        public int BestId { get; set; }
        public string ClientName { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public List<GetCareIssuesTask> GetCareIssuesTask { get; set; }
        public List<GetHealthTask> GetHealthTask { get; set; }
        public List<GetBelieveTask> GetBelieveTask { get; set; }
        public List<GetHealthTask2> GetHealthTask2 { get; set; }
        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Signature { get; set; }

        public int BelieveTaskCount { get; set; }
        public int CareIssuesTaskCount { get; set; }
        public int HealthTaskCount { get; set; }
        public int HealthTask2Count { get; set; }
    }
}
