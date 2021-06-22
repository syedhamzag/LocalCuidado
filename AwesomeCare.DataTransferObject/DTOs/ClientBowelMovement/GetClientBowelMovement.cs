using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement
{
    public class GetClientBowelMovement
    {
        public GetClientBowelMovement()
        {
            OfficerToAct = new List<GetBowelMovementOfficerToAct>();
            Physician = new List<GetBowelMovementPhysician>();
            StaffName = new List<GetBowelMovementStaffName>();
        }

        public List<GetBowelMovementOfficerToAct> OfficerToAct { get; set; }
        public List<GetBowelMovementPhysician> Physician { get; set; }
        public List<GetBowelMovementStaffName> StaffName { get; set; }

        public int BowelMovementId { get; set; }
        public string Reference { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Type { get; set; }
        public string TypeAttach { get; set; }
        public int Size { get; set; }
        public int Color { get; set; }
        public string ColorAttach { get; set; }
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        public string Comment { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
