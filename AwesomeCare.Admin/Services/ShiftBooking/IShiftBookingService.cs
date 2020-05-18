using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
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
    }
}
