using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateSalaryAllowance
    {
        public int SalaryAllowanceId { get; set; }
        public int AllowanceType { get; set; }
        public string Reoccurent { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
    }
}
