using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
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
            Tasks = new List<CreateHomeRiskAssessmentTask>();
            baseRecordList = new List<GetBaseRecordItem>();
        }
        public List<SelectListItem> HeadingList { get; set; }
        public int HomeRiskAssessmentId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Heading { get; set; }
        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public List<CreateHomeRiskAssessmentTask> Tasks { get; set; }
    }
}
