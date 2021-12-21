using AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffInfectionControlService
    {
        [Get("/StaffInfectionControl")]
        Task<List<GetStaffInfectionControl>> Get();

        [Get("/StaffInfectionControl/Get/{id}")]
        Task<GetStaffInfectionControl> Get(int id);

        [Post("/StaffInfectionControl/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffInfectionControl model);

        [Put("/StaffInfectionControl/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffInfectionControl model);
    }
}
