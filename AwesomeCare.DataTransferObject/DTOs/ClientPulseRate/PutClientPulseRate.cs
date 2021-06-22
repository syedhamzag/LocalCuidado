using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientPulseRate
{
    public class PutClientPulseRate
    {
        public PutClientPulseRate()
        {
            OfficerToAct = new List<PutPulseRateOfficerToAct>();
            Physician = new List<PutPulseRatePhysician>();
            StaffName = new List<PutPulseRateStaffName>();
        }

        public List<PutPulseRateOfficerToAct> OfficerToAct { get; set; }
        public List<PutPulseRatePhysician> Physician { get; set; }
        public List<PutPulseRateStaffName> StaffName { get; set; }

        public int PulseRateId { get; set; }
        public int ClientId { get; set; }
        public string Reference { get; set; }
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
