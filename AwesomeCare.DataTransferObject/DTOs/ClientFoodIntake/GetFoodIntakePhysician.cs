using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake
{
    public class GetFoodIntakePhysician
    {
        public int FoodIntakePhysicianId { get; set; }
        public int FoodIntakeId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
