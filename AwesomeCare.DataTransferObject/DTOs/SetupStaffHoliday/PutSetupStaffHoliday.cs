using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.SetupStaffHoliday
{
    public class PutSetupStaffHoliday : BaseDTO
    {
        public int SetupHolidayId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public DateTime YearOfEmployment { get; set; }
        public int TypeOfHoliday { get; set; }
        public DateTime YearEndPeriodStartDate { get; set; }
        public int HoursSoFar { get; set; }
        public int IncrementalDailyHolidayByHrs { get; set; }
        public int NumberOfDays { get; set; }
        public string Remark { get; set; }

        public string Attachment { get; set; }
    }
}
