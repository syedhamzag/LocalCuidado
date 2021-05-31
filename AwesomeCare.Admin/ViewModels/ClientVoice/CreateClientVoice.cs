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
            StaffPoorSupportList = new List<GetStaffs>();
            StaffPoorSupportList = new List<GetStaffs>();
            StaffPoorSupportList = new List<GetStaffs>();
            StaffPoorSupportList = new List<GetStaffs>();
            StaffPoorSupportList = new List<GetStaffs>();
            StatusList = new List<SelectListItem>();
            InterestedInProgramList = new List<SelectListItem>();
            RateServiceRecievingList = new List<SelectListItem>();
            RateStaffAttendingList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }
        #region DropDowns
        public ICollection<GetStaffs> StaffPoorSupportList { get; set; }
        public ICollection<GetStaffs> StaffBestSupportList { get; set; }
        public ICollection<GetStaffs> OfficeStaffSupportList { get; set; }
        public ICollection<GetStaffs> NameOfCallerList { get; set; }
        public ICollection<GetStaffs> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        public ICollection<SelectListItem> InterestedInProgramList { get; set; }
        public ICollection<SelectListItem> RateServiceRecievingList { get; set; }
        public ICollection<SelectListItem> RateStaffAttendingList { get; set; }
        #endregion
        public string ActiveTab { get; set; } = "voice";
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
        public int StaffBestSupport { get; set; }
        [Required]
        public int StaffPoorSupport { get; set; }
        [Required]
        public int OfficeStaffSupport { get; set; }
        [Required]
        [MaxLength(255)]
        public string AreasOfImprovements { get; set; }
        [Required]
        [MaxLength(255)]
        public string SomethingSpecial { get; set; }
        [Required]
        public int InterestedInPrograms { get; set; }
        [Required]
        [MaxLength(255)]
        public string HealthGoalShortTerm { get; set; }
        [Required]
        [MaxLength(255)]
        public string HealthGoalLongTerm { get; set; }
        [Required]
        public int NameOfCaller { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionRequired { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionsTakenByMPCC { get; set; }
        [Required]
        public string EvidenceOfActionTaken { get; set; }
        [Required]
        public int OfficerToAct { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        [MaxLength(255)]
        public string Remarks { get; set; }
        [Required]
        [MaxLength(50)]
        public string RotCause { get; set; }
        [Required]
        [MaxLength(255)]
        public string LessonLearntAndShared { get; set; }
        [Required]
        [MaxLength(255)]
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
