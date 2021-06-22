using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class ClientFoodIntake
    {
        public ClientFoodIntake()
        {
            Physician = new HashSet<FoodIntakePhysician>();
            StaffName = new HashSet<FoodIntakeStaffName>();
            OfficerToAct = new HashSet<FoodIntakeOfficerToAct>();
        }
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
        

    public virtual Client Client { get; set; }
        public virtual ICollection<FoodIntakeOfficerToAct> OfficerToAct { get; set; }
        public virtual ICollection<FoodIntakeStaffName> StaffName { get; set; }
        public virtual ICollection<FoodIntakePhysician> Physician { get; set; }
    }

}
