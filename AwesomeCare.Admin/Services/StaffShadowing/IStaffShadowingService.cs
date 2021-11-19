using AwesomeCare.DataTransferObject.DTOs.StaffShadowing;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffShadowing
{
    public interface IStaffShadowingService
    {
        [Get("/StaffShadowing")]
        Task<List<GetStaffShadowing>> Get();

        [Get("/StaffShadowing/Get/{id}")]
        Task<GetStaffShadowing> Get(int id);

        [Get("/StaffShadowing/GetByStaffPersonalInfo/{id}")]
        Task<List<GetStaffShadowing>> GetByStaffPersonalInfo(int id);

        [Post("/StaffShadowing/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffShadowing model);

        [Put("/StaffShadowing/Put")]
        Task<HttpResponseMessage> Put([Body] PostStaffShadowing model);
    }
}
