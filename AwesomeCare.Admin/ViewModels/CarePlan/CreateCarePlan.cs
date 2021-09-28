using AwesomeCare.DataTransferObject.Validations;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;

namespace AwesomeCare.Admin.ViewModels.CarePlan
{
    public class CreateCarePlan
    {
        public string ActionName { get; set; } = "Save";
        public CreateCarePlan()
        {
            Individuality = new List<SelectListItem>();
            RightsAndRespect = new List<SelectListItem>();
            Choice = new List<SelectListItem>();
            DignityAndPrivacy = new List<SelectListItem>();
            Partnership = new List<SelectListItem>();
            IndicatorList = new List<SelectListItem>();
            FocusList = new Dictionary<string, List<SelectListItem>>();
            StaffList = new List<SelectListItem>();
            GetEquipment = new List<GetEquipment>();
            LandLogList = new List<SelectListItem>();
            KeyLogList = new List<SelectListItem>();
            InvolingList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 11)]
        public IFormFile Attach { get; set; }

        public List<GetEquipment> GetEquipment { get; set; }
        public List<GetPersonCentred> GetPersonCentred { get; set; }
        public List<GetPersonCentredFocus> GetPersonCentredFocus { get; set; }
        public List<int> Focus1 { get; set; }
        public List<int> Focus2 { get; set; }
        public List<int> Focus3 { get; set; }
        public List<int> Focus4 { get; set; }
        public List<int> Focus5 { get; set; }
        public List<SelectListItem> Individuality { get; set; }
        public List<SelectListItem> RightsAndRespect { get; set; }
        public List<SelectListItem> Choice { get; set; }
        public List<SelectListItem> DignityAndPrivacy { get; set; }
        public List<SelectListItem> Partnership { get; set; }
        public List<SelectListItem> StaffList { get; set; }
        public Dictionary<string,List<SelectListItem>> FocusList { get; set; }
        public List<SelectListItem> IndicatorList { get; set; }
        public List<SelectListItem> LandLogList { get; set; }
        public List<SelectListItem> KeyLogList { get; set; }
        public List<SelectListItem> InvolingList { get; set; }
        public List<SelectListItem> ClassList { get; set; }

        public string ClientName { get; set; }
        public int EquipmentCount { get; set; } = 0;
        public int PersonCentreCount { get; set; } = 5;

        #region Personal Detail
        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }
        #endregion

        #region Capacity
        [Required]
        public int CapacityId { get; set; }
        public List<int> Indicator { get; set; }
        [Required]
        public int Pointer { get; set; }
        [Required]
        public string Implications { get; set; }
        #endregion

        #region ConsentCare
        [Required]
        public int CareId { get; set; }
        [Required]
        public int CareSignature { get; set; }
        [Required]
        public DateTime CareDate { get; set; }
        public int CareName { get; set; }
        public string CareRelation { get; set; }
        #endregion

        #region ConsentData
        [Required]
        public int DataId { get; set; }
        [Required]
        public int DataSignature { get; set; }
        [Required]
        public DateTime DataDate { get; set; }
        public int DataName { get; set; }
        public string DataRelation { get; set; }
        #endregion

        #region ConsentLandLine
        [Required]
        public int LandLineId { get; set; }
        [Required]
        public int LandLineSignature { get; set; }
        [Required]
        public List<int> LandLineLogMethod { get; set; }
        [Required]
        public DateTime LandLineDate { get; set; }

        public int LandName { get; set; }
        public string LandRelation { get; set; }
        #endregion

        #region KeyIndicators
        public int KeyId { get; set; }
        [Required]
        public string AboutMe { get; set; }
        [Required]
        public string FamilyRole { get; set; }
        [Required]
        public int LivingStatus { get; set; }
        [Required]
        public int Debture { get; set; }
        [Required]
        public string ThingsILike { get; set; }
        [Required]
        public List<int> LogMethod { get; set; }
        #endregion

        #region Personal
        [Required]
        public int PersonalId { get; set; }
        [Required]
        public int Smoking { get; set; }
        [Required]
        public int DNR { get; set; }

        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public int PreferredLanguage { get; set; }
        public int Gender { get; set; }
        public string Religion { get; set; }
        public int PreferredGender { get; set; }
        public string Telephone { get; set; }
        public string AccessCode { get; set; }
        public string PostCode { get; set; }
        public string Nationality { get; set; }
        public string DateofBirth { get; set; }
        public string Address { get; set; }

        public string KeyWorker { get; set; }
        public string TeamLeader { get; set; }
        #endregion

        #region Review
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public DateTime CP_PreDate { get; set; }
        [Required]
        public DateTime CP_ReviewDate { get; set; }
        [Required]
        public DateTime RA_PreDate { get; set; }
        [Required]
        public DateTime RA_ReviewDate { get; set; }
        #endregion
    }
}
