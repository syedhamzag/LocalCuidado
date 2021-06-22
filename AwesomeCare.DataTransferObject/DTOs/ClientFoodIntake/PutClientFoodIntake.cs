using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake
{
    public class PutClientFoodIntake
    {
        public PutClientFoodIntake()
        {
            OfficerToAct = new List<PutFoodIntakeOfficerToAct>();
            Physician = new List<PutFoodIntakePhysician>();
            StaffName = new List<PutFoodIntakeStaffName>();
        }

        public List<PutFoodIntakeOfficerToAct> OfficerToAct { get; set; }
        public List<PutFoodIntakePhysician> Physician { get; set; }
        public List<PutFoodIntakeStaffName> StaffName { get; set; }

        public string Reference { get; set; }
        public int FoodIntakeId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Goal { get; set; }
        public int CurrentIntake { get; set; }
        public string Comment { get; set; }
        public int StatusImage { get; set; }
        public string StatusAttach { get; set; }
        public string PhysicianResponse { get; set; }
        public DateTime Deadline { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
    }
}
