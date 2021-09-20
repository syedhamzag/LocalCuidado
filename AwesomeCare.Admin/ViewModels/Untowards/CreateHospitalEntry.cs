using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Untowards
{
    public class CreateHospitalEntry
    {
        public CreateHospitalEntry()
        {
            StaffList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }
        public int HospitalEntryId { get; set; }
        public int ClientId { get; set; }

        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string PurposeofAdmission { get; set; }

        public string CauseofAdmission { get; set; }
        public DateTime LastDateofAdmission { get; set; }
        public int ConditionOfAdmission { get; set; }
        public int IsFamilyInformed { get; set; }
        public DateTime PossibleDateReturn { get; set; }
        public int IsHomeCleaned { get; set; }
        public int NameParamedicStaff { get; set; }
        public int ParamicStaffTeamNo { get; set; }
        public string URLLINK { get; set; }
        public int MeansOfTransport { get; set; }
        public string Attachment { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string ClientName { get; set; }
        public string StatusName { get; set; }

        public List<SelectListItem> StaffList { get; set; }
        public List<int> StaffInvolved { get; set; }
        public List<int> PersonToTakeAction { get; set; }
    }
}
