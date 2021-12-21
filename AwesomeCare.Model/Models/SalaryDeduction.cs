using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class SalaryDeduction: Base.BaseModel
    {
        public int SalaryDeductionId { get; set; }
        public int AllowanceType { get; set; }
        public string Reoccurent { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StaffPersonalInfoId { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
