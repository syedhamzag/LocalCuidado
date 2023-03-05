using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Medication
{
    public class MedTracker
    {
        public int StaffRotaPeriodId { get; set; }
        public string PERIOD { get; set; }
        public DateTime RotaDate { get; set; }
        public string RotaName { get; set; }
        public string ClientName { get; set; }
        public string DOB { get; set; }
        public string ClientIdNumber { get; set; }
        public int ClientMedicationId { get; set; }
        public int ClientId { get; set; }
        public int MedicationId { get; set; }
        public int MedicationManufacturerId { get; set; }
        public string Medication { get; set; }
        public string MedicationManufacturer { get; set; }
        public string ExpiryDate { get; set; }
        public string Dossage { get; set; }
        public string Frequency { get; set; }
        public int Gap_Hour { get; set; }
        public string Route { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public int Means { get; set; }
        public int Type { get; set; }
        public int TimeCritical { get; set; }
        public string ClientMedImage { get; set; }
        public string StaffName { get; set; }
        public int NoOfStaff { get; set; }
        public string DoseGiven { get; set; }
        public string Time { get; set; }
        public string Measurement { get; set; }
        public string Location { get; set; }
        public string Feedback { get; set; }
    }
}
