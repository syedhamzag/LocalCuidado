using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBookingBlockedDays
{
    public class GetShiftBookingBlockedDays
    {
        public int ShiftBookingBlockedDaysId { get; set; }
        public int ShiftBookingId { get; set; }
        public string Day { get; set; }
        public string WeekDay { get; set; }
    }
}
