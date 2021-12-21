using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication
{
    public class PutSpecialHealthAndMedication
    {
        public int SHMId { get; set; }
        public int ClientId { get; set; }
        public int AdminLvl { get; set; }
        public int MedicationAllergy { get; set; }
        public int FamilyMeds { get; set; }
        public int LeftoutMedicine { get; set; }
        public int NameFormMedicaiton { get; set; }
        public int WhoAdminister { get; set; }
        public int MedsGPOrder { get; set; }
        public int PharmaMARChart { get; set; }
        public int TempMARChart { get; set; }
        public string MedKeyCode { get; set; }
        public int FamilyReturnMed { get; set; }
        public int AccessMedication { get; set; }
        public int NoMedAccess { get; set; }
        public int MedAccessDenial { get; set; }
        public int PNRMedReq { get; set; }
        public int PNRDoses { get; set; }
        public int PNRMedsAdmin { get; set; }
        public int OverdoseContact { get; set; }
        public string PNRMedsMissing { get; set; }
        public string MedicationStorage { get; set; }
        public int SpecialStorage { get; set; }
        public string PNRMedList { get; set; }
        public int Consent { get; set; }
        public int Type { get; set; }
        public string By { get; set; }
        public DateTime Date { get; set; }
    }
}
