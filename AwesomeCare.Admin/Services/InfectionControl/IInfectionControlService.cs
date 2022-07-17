using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.InfectionControl;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.InfectionControl
{
    public interface IInfectionControlService
    {
        [Get("/InfectionControl")]
        Task<List<GetInfectionControl>> Get();

        [Get("/InfectionControl/Get/{id}")]
        Task<GetInfectionControl> Get(int id);

        [Get("/InfectionControl/GetbyClient/{id}")]
        Task<GetInfectionControl> GetbyClient(int id);

        [Post("/InfectionControl/Create")]
        Task<HttpResponseMessage> Create([Body] PostInfectionControl model);

        [Put("/InfectionControl/Put")]
        Task<HttpResponseMessage> Put([Body] PutInfectionControl model);

        [Delete("/InfectionControl/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
