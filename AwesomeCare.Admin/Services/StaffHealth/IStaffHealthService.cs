using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffHealth
{
    public interface IStaffHealthService
    {
        [Get("/StaffHealth")]
        Task<List<GetStaffHealth>> Get();

        [Get("/StaffHealth/Get/{id}")]
        Task<GetStaffHealth> Get(int id);

        [Get("/StaffHealth/GetByStaffPersonalInfo/{id}")]
        Task<List<GetStaffHealth>> GetByStaffPersonalInfo(int id);

        [Post("/StaffHealth/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffHealth model);

        [Put("/StaffHealth/Put")]
        Task<HttpResponseMessage> Put([Body] PostStaffHealth model);
    }
}
