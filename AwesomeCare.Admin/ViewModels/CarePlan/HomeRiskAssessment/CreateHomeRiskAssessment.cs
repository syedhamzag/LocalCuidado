using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan.HomeRiskAssessment
{
    public class CreateHomeRiskAssessment
    {
        public CreateHomeRiskAssessment()
        {
            HeadingList = new List<SelectListItem>();
            Tasks = new List<GetHomeRiskAssessmentTask>();
            baseRecordList = new List<GetBaseRecordItem>();
        }
        public List<SelectListItem> HeadingList { get; set; }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public int HeadingId { get; set; }
        public string Heading { get; set; }
        public int TaskCount { get; set; }

        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public List<GetHomeRiskAssessmentTask> Tasks { get; set; }
    }
}
