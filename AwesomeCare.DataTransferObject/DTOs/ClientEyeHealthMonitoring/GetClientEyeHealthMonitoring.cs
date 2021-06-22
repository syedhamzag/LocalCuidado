using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring
{
    public class GetClientEyeHealthMonitoring
    {
        public GetClientEyeHealthMonitoring()
        {
            OfficerToAct = new List<GetEyeHealthOfficerToAct>();
            Physician = new List<GetEyeHealthPhysician>();
            StaffName = new List<GetEyeHealthStaffName>();
        }

        public List<GetEyeHealthOfficerToAct> OfficerToAct { get; set; }
        public List<GetEyeHealthPhysician> Physician { get; set; }
        public List<GetEyeHealthStaffName> StaffName { get; set; }

        public int EyeHealthId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int ToolUsed { get; set; }
        public string ToolUsedAttach { get; set; }
        public int MethodUsed { get; set; }
        public string MethodUsedAttach { get; set; }
        public int TargetSet { get; set; }
        public int CurrentScore { get; set; }
        public int PatientGlasses { get; set; }
        public string Comment { get; set; }
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
