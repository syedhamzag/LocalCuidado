using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Web.Services.ShiftBooking
{
   public interface IShiftBookingService
    {
        [Post("/ShiftBooking")]
        Task<HttpResponseMessage> Post([Body]PostShiftBooking shiftBooking);
        [Get("/ShiftBooking")]
        Task<List<GetShiftBookingDetails>> Get();
        [Get("/ShiftBooking/{month}/{year}")]
        Task<GetShiftBookedByMonthYear> GetShiftByMonthAndYear(string month,string year);
        [Get("/ShiftBooking/{id}")]
        Task<GetShiftBookingDetails> Get(int id);
        [Post("/ShiftBooking/Staff/CreateBooking")]
        Task<HttpResponseMessage> CreateBooking([Body]PostStaffShiftBooking shiftBooking);
        
    }
}
