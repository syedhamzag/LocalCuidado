using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring
{
    public class PutClientEyeHealthMonitoring
    {

        public PutClientEyeHealthMonitoring()
        {
            OfficerToAct = new List<PutEyeHealthOfficerToAct>();
            Physician = new List<PutEyeHealthPhysician>();
            StaffName = new List<PutEyeHealthStaffName>();
        }

        public List<PutEyeHealthOfficerToAct> OfficerToAct { get; set; }
        public List<PutEyeHealthPhysician> Physician { get; set; }
        public List<PutEyeHealthStaffName> StaffName { get; set; }

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
