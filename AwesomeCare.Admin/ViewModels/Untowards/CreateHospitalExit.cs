using AwesomeCare.DataTransferObject.DTOs.HospitalExit;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Untowards
{
    public class CreateHospitalExit
    {
        public CreateHospitalExit()
        {
            StaffList = new List<SelectListItem>();
        }
        public int ClientId { get; set; }
        public int HospitalExitId { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string PurposeofAdmission { get; set; }
        public int ConditionOnDischarge { get; set; }
        public int NumberOfStaffRequiredOnDischarge { get; set; }
        public int IsGrosSriesAvaible { get; set; }
        public int IsHomeCleaned { get; set; }
        public int IsMedicationAvaialable { get; set; }
        public int IsServiceUseronRota { get; set; }
        public int isRotaTeamInformed { get; set; }
        public int isLittleCashAvailableForServiceUser { get; set; }
        public int ModeOfMeansOfTrasportBackHome { get; set; }
        public string URLLINK { get; set; }
        public int AreEqipmentNeededAvailable { get; set; }
        public int AreStaffTrainnedOnEquipmentNeeded { get; set; }
        public int AreContinentProductNeedAndAvailable { get; set; }
        public int AreLocalSupportOrProgramNeeded { get; set; }
        public string WhichSupportIsNeeded { get; set; }
        public int IsCarePlanUpdated { get; set; }
        public int ReablementRequired { get; set; }
        public string ContactIncaseOfReAdmission { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }

        public string StatusName { get; set; }
        public string ClientName { get; set; }
        public string IdNumber { get; set; }
        public string DOB { get; set; }
        public string ConditionOnDischargeName { get; set; }
        public string NumberOfStaffRequiredOnDischargeName { get; set; }
        public string IsGrosSriesAvaibleName { get; set; }

        public string IsHomeCleanedName { get; set; }
        public string IsMedicationAvaialableName { get; set; }
        public string IsServiceUseronRotaName { get; set; }

        public string isRotaTeamInformedName { get; set; }
        public string isLittleCashAvailableForServiceUserName { get; set; }
        public string ModeOfMeansOfTrasportBackHomeName { get; set; }
        public string AreEqipmentNeededAvailableName { get; set; }
        public string AreStaffTrainnedOnEquipmentNeededName { get; set; }
        public string AreContinentProductNeedAndAvailableName { get; set; }
        public string AreLocalSupportOrProgramNeededName { get; set; }
        public string IsCarePlanUpdatedName { get; set; }
        public string ReablementRequiredName { get; set; }
        public List<SelectListItem> StaffList {get;set;}
        public List<int> OfficerToTakeAction { get; set; }
        public string OfficerToTakeActionName { get;set; }
    }
}
