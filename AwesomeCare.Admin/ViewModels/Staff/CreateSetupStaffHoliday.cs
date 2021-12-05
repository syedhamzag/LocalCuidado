using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateSetupStaffHoliday
    {
        public CreateSetupStaffHoliday()
        {
            StaffList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        public IFormFile Attach { get; set; }
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

        public string StaffName { get; set; }
        public ICollection<SelectListItem> StaffList { get; set; }
    }
}
