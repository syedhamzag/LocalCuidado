using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientVoice
{
    public class CreateClientVoice
    {
        public CreateClientVoice()
        {
            StatusList = new List<SelectListItem>();
            InterestedInProgramList = new List<SelectListItem>();
            RateServiceRecievingList = new List<SelectListItem>();
            RateStaffAttendingList = new List<SelectListItem>();
            OfficerToActList = new List<SelectListItem>();
            CallerList = new List<SelectListItem>();
            BestSupportList = new List<SelectListItem>();
            PoorSupportList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        
        public IFormFile Attach { get; set; }
        [DataType(DataType.Upload)]
        
        public IFormFile Evidence { get; set; }
        #region DropDowns

        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> CallerList { get; set; }
        public ICollection<SelectListItem> BestSupportList { get; set; }
        public ICollection<SelectListItem> PoorSupportList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        public ICollection<SelectListItem> InterestedInProgramList { get; set; }
        public ICollection<SelectListItem> RateServiceRecievingList { get; set; }
        public ICollection<SelectListItem> RateStaffAttendingList { get; set; }
        #endregion
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        public string ActiveTab { get; set; } = "voice";
        public string Reference { get; set; }
        public int VoiceId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public int RateServiceRecieving { get; set; }
        [Required]
        public int RateStaffAttending { get; set; }
        [Required]
        public List<int> StaffBestSupport { get; set; }
        public List<string> Best_Staff { get; set; }
        [Required]
        public List<int> StaffPoorSupport { get; set; }
        public List<string> Poor_Staff { get; set; }
        [Required]
        public int OfficeStaffSupport { get; set; }
        [Required]
        public string AreasOfImprovements { get; set; }
        [Required]
        public string SomethingSpecial { get; set; }
        [Required]
        public int InterestedInPrograms { get; set; }
        [Required]
        public string HealthGoalShortTerm { get; set; }
        [Required]
        public string HealthGoalLongTerm { get; set; }
        [Required]
        public List<int> NameOfCaller { get; set; }
        public List<string> Caller_Name { get; set; }
        [Required]
        public string ActionRequired { get; set; }
        [Required]
        public string ActionsTakenByMPCC { get; set; }
        public string EvidenceOfActionTaken { get; set; }
        [Required]
        public List<int> OfficerToAct { get; set; }
        public List<string> OfficerName { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string RotCause { get; set; }
        [Required]  
        public string LessonLearntAndShared { get; set; }
        [Required]
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
