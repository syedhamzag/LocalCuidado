using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffHoliday : Base.BaseModel
    {
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

        public string Attachment { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }

    }
}
