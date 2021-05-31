using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.ShiftBookingBlockedDays;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using Refit;
namespace AwesomeCare.Admin.Services.ShiftBooking
{
  public  interface IShiftBookingService
    {
        [Post("/ShiftBooking")]
        Task<HttpResponseMessage> Post([Body]PostShiftBooking shiftBooking);
        [Get("/ShiftBooking")]
        Task<List<GetShiftBookingDetails>> Get();
        [Get("/ShiftBooking/{id}")]
        Task<GetShiftBookingDetails> Get(int id);
        [Delete("/ShiftBooking/{shiftId}")]
        Task<HttpResponseMessage> Delete(int shiftId);


        [Get("/ShiftBooking/Admin/{monthId}/{rotaId}")]
        Task<GetShiftBookedByMonthYear> GetStaffShiftBookingsByMonth(int monthId,int? rotaId);
        [Get("/ShiftBooking/{month}/{year}/{rotaId}")]
        Task<GetShiftBookedByMonthYear> GetShiftByMonthAndYear(string month, string year,int? rotaId);
        [Post("/ShiftBooking/Staff/CreateBooking")]
        Task<HttpResponseMessage> CreateBooking([Body] PostStaffShiftBooking shiftBooking);
        [Post("/ShiftBooking/BlockDay")]
        Task<HttpResponseMessage> BlockDays([Body] PostShiftBookingBlockedDays blockDays);
        [Post("/ShiftBooking/BlockDays")]
        Task<HttpResponseMessage> BlockDays([Body] List<PostShiftBookingBlockedDays> blockDays);

        [Delete("/ShiftBooking")]
        Task<HttpResponseMessage> DeleteStaffShiftBooking([Body] DeleteStaffShiftBookingDay model);
    }
}
