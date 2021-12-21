using AwesomeCare.DataTransferObject.DTOs.SetupStaffHoliday;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffHoliday
{
    public interface ISetupStaffHolidayService
    {
        [Get("/SetupStaffHoliday")]
        Task<List<GetSetupStaffHoliday>> Get();

        [Get("/SetupStaffHoliday/Get/{id}")]
        Task<GetSetupStaffHoliday> Get(int id);

        [Post("/SetupStaffHoliday/Create")]
        Task<HttpResponseMessage> Create([Body] PostSetupStaffHoliday model);

        [Put("/SetupStaffHoliday/Put")]
        Task<HttpResponseMessage> Put([Body] PutSetupStaffHoliday model);
    }
}
