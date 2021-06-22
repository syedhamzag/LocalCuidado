﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientBowelMovement
    {
        public ClientBowelMovement()
        {
            Physician = new HashSet<BowelMovementPhysician>();
            StaffName = new HashSet<BowelMovementStaffName>();
            OfficerToAct = new HashSet<BowelMovementOfficerToAct>();
        }
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
    
        public virtual Client Client { get; set; }
        public virtual ICollection<BowelMovementOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<BowelMovementStaffName> StaffName { get; set; }
        public virtual ICollection<BowelMovementPhysician> Physician { get; set; }
    }

}
