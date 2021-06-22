using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement
{
    public class PostClientBowelMovement
    {
        public PostClientBowelMovement()
        {
            OfficerToAct = new List<PostBowelMovementOfficerToAct>();
            Physician = new List<PostBowelMovementPhysician>();
            StaffName = new List<PostBowelMovementStaffName>();
        }

        public List<PostBowelMovementOfficerToAct> OfficerToAct { get; set; }
        public List<PostBowelMovementPhysician> Physician { get; set; }
        public List<PostBowelMovementStaffName> StaffName { get; set; }

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
