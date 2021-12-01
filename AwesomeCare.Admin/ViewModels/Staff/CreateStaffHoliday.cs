using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class CreateStaffHoliday
    {
        [DataType(DataType.Upload)]

        public IFormFile Attach { get; set; }
        public int StaffHolidayId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public decimal YearOfService { get; set; }
        public decimal AllocatedDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Days { get; set; }
        public string Purpose { get; set; }
        public int Class { get; set; }
        public string PersonOnResponsibility { get; set; }
        public string CopyOfHandover { get; set; }
        public string Remark { get; set; }

        public string StaffName { get; set; }
        public string Attachment { get; set; }
    }
}
