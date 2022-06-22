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
        [Required]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        public DateTime YearOfEmployment { get; set; }
        [Required]
        public int TypeOfHoliday { get; set; }
        [Required]
        public DateTime YearEndPeriodStartDate { get; set; }
        [Required]
        public int HoursSoFar { get; set; }
        [Required]
        public int IncrementalDailyHolidayByHrs { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        [Required]
        public string Remark { get; set; }

        public string Attachment { get; set; }

        public string StaffName { get; set; }
        public ICollection<SelectListItem> StaffList { get; set; }
    }
}
