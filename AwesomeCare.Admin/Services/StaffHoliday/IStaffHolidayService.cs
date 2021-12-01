using AwesomeCare.DataTransferObject.DTOs.Staff.StaffHoliday;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffHoliday
{
    public interface IStaffHolidayService
    {
        [Get("/StaffHoliday")]
        Task<List<GetStaffHoliday>> Get();

        [Get("/StaffHoliday/Get/{id}")]
        Task<GetStaffHoliday> Get(int id);

        [Post("/StaffHoliday/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffHoliday model);

        [Put("/StaffHoliday/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffHoliday model);
    }
}
