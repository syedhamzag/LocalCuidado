using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientWoundCare
{
    public class GetClientWoundCare
    {
        public GetClientWoundCare()
        {
            OfficerToAct = new List<GetWoundCareOfficerToAct>();
            Physician = new List<GetWoundCarePhysician>();
            StaffName = new List<GetWoundCareStaffName>();
        }

        public List<GetWoundCareOfficerToAct> OfficerToAct { get; set; }
        public List<GetWoundCarePhysician> Physician { get; set; }
        public List<GetWoundCareStaffName> StaffName { get; set; }

        public int WoundCareId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Goal { get; set; }
        public int Type { get; set; }
        public string TypeAttach { get; set; }
        public int UlcerStage { get; set; }
        public string UlcerStageAttach { get; set; }
        public int Measurment { get; set; }
        public string MeasurementAttach { get; set; }
        public int PainLvl { get; set; }
        public int Location { get; set; }
        public string LocationAttach { get; set; }
        public int WoundCause { get; set; }
        public string Comment { get; set; }
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
