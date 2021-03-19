using AwesomeCare.DataTransferObject.DTOs.StaffRotaTask;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Rotering
{
   public class StaffClockInClockOut
    {
        public StaffClockInClockOut()
        {
            StaffRotaTasks = new List<PostStaffRotaTask>();
        }
        [Required]
        public int StaffRotaPeriodId { get; set; }
        public string Feedback { get; set; }

        public List<PostStaffRotaTask> StaffRotaTasks { get; set; }
    }
}
