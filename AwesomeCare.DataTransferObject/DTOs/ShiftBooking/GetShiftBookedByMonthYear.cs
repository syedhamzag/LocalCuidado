using AwesomeCare.DataTransferObject.DTOs.ShiftBookingBlockedDays;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ShiftBooking
{
  public  class GetShiftBookedByMonthYear
    {
        public GetShiftBookedByMonthYear()
        {
            Staffs = new List<StaffBooked>();
            BookedDays = new List<BookedDays>();
            BlockedDays = new List<GetShiftBookingBlockedDays>();
        }
        public int TeamId { get; set; }
        public bool DriverRequired { get; set; }
        public int NumberOfStaffRequired { get; set; }
        public int? PublishTo { get; set; }
        public string Remark { get; set; }
        public int RotaId { get; set; }
        public int ShiftBookingId { get; set; }
        public string ShiftDate { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public int NumberOfStaffRegistered { get; set; }
        public List<StaffBooked> Staffs { get; set; }
        public List<BookedDays> BookedDays { get; set; }
        public List<GetShiftBookingBlockedDays> BlockedDays { get; set; }
    }
    //public class ShiftBookingStaff
    //{
    //    public ShiftBookingStaff()
    //    {
    //        ShiftDays = new List<ShiftBookingStaffDay>();
    //    }
    //    public int ShiftBookingId { get; set; }
    //    public List<ShiftBookingStaffDay> ShiftDays { get; set; }
    //}

    public class BookedDays
    {
       
        public int StaffShiftBookingId { get; set; }
        public int StaffShiftBookingDayId { get; set; }
        public string WeekDay { get; set; }
        public string Day { get; set; }
        /// <summary>
        /// StaffPersonalId
        /// </summary>
        public int ShiftBookedById { get; set; }

    }

    public class StaffBooked
    {
        public StaffBooked()
        {
            BookedDays = new List<BookedDays>();
        }
        public int StaffPersonalInfoId { get; set; }
        public string StaffName { get; set; }
        public bool IsStaffDriver { get; set; }
        public int StaffShiftBookingId { get; set; }
        public int ShiftBookingId { get; set; }
        public int MyProperty { get; set; }

        public List<BookedDays> BookedDays { get; set; }
    }
}
