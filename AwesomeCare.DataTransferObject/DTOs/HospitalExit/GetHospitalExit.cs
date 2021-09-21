using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.HospitalExit
{
    public class GetHospitalExit
    {
        public GetHospitalExit()
        {
            OfficerToTakeAction = new List<GetHospitalExitOfficerToTakeAction>();
        }

        public int HospitalExitId { get; set; }
        public int ClientId { get; set; }
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
        public List<GetHospitalExitOfficerToTakeAction> OfficerToTakeAction { get; set; }
    }
}
