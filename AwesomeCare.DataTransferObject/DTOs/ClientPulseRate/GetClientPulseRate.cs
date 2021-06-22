using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPulseRate
{
    public class GetClientPulseRate
    {
        public GetClientPulseRate()
        {
            OfficerToAct = new List<GetPulseRateOfficerToAct>();
            Physician = new List<GetPulseRatePhysician>();
            StaffName = new List<GetPulseRateStaffName>();
        }

        public List<GetPulseRateOfficerToAct> OfficerToAct { get; set; }
        public List<GetPulseRatePhysician> Physician { get; set; }
        public List<GetPulseRateStaffName> StaffName { get; set; }

        public int PulseRateId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int TargetPulse { get; set; }
        public string TargetPulseAttach { get; set; }
        public string CurrentPulse { get; set; }
        public string Chart { get; set; }
        public int SeeChart { get; set; }
        public string SeeChartAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
