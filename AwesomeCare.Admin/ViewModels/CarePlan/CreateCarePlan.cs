using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan
{
    public class CreateCarePlan
    {
        public CreateCarePlan()
        {
            IndicatorList = new List<SelectListItem>();
            FocusList = new List<SelectListItem>();
            StaffList = new List<SelectListItem>();
        }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }

        public List<SelectListItem> StaffList { get; set; }
        public List<SelectListItem> FocusList { get; set; }
        public List<SelectListItem> IndicatorList { get; set; }

        [Required]
        public int ClientId { get; set; }

        #region Capacity
        [Required]
        public int CapacityId { get; set; }
        [Required]
        public List<int> Indicator { get; set; }
        [Required]
        public int Pointer { get; set; }
        [Required]
        public int Implications { get; set; }
        #endregion

        #region ConsentCare
        [Required]
        public int CareId { get; set; }
        [Required]
        public int CareSignature { get; set; }
        [Required]
        public DateTime CareDate { get; set; }
        #endregion

        #region ConsentData
        [Required]
        public int DataId { get; set; }
        [Required]
        public int DataSignature { get; set; }
        [Required]
        public DateTime DataDate { get; set; }
        #endregion

        #region ConsentLandLine
        [Required]
        public int LandLineId { get; set; }
        [Required]
        public int LandLineSignature { get; set; }
        [Required]
        public int LandLineLogMethod { get; set; }
        [Required]
        public DateTime LandLineDate { get; set; }
        #endregion

        #region Equipment
        public int EquipmentId { get; set; }
        [Required]
        public int Name { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int Location { get; set; }
        [Required]
        public DateTime ServiceDate { get; set; }
        [Required]
        public DateTime NextServiceDate { get; set; }
        public string Attachment { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int PersonToAct { get; set; }
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
        public int LogMethod { get; set; }
        #endregion

        #region Personal
        [Required]
        public int PersonalId { get; set; }
        [Required]
        public int Smoking { get; set; }
        [Required]
        public int DNR { get; set; }
        #endregion

        #region PersonCentred
        [Required]
        public int PersonCentredId { get; set; }
        [Required]
        public List<int> Focus { get; set; }
        [Required]
        public int Class { get; set; }
        [Required]
        public string ExpSupport { get; set; }
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
