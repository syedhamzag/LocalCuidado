using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public  class FoodIntakeStaffName
    {
        public int FoodIntakeStaffNameId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int FoodIntakeId { get; set; }

        public virtual ClientFoodIntake FoodIntake { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
