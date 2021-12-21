using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake
{
    public class GetClientFoodIntake
    {
        public GetClientFoodIntake()
        {
            OfficerToAct = new List<GetFoodIntakeOfficerToAct>();
            Physician = new List<GetFoodIntakePhysician>();
            StaffName = new List<GetFoodIntakeStaffName>();
        }

        public List<GetFoodIntakeOfficerToAct> OfficerToAct { get; set; }
        public List<GetFoodIntakePhysician> Physician { get; set; }
        public List<GetFoodIntakeStaffName> StaffName { get; set; }

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
