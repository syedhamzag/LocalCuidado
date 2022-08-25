using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.PhysicalAbility
{
    public interface IPhysicalAbilityService
    {
        [Get("/PhysicalAbility")]
        Task<List<GetPhysicalAbility>> Get();

        [Get("/PhysicalAbility/Get/{id}")]
        Task<GetPhysicalAbility> Get(int id);

        [Get("/PhysicalAbility/GetbyClient/{id}")]
        Task<GetPhysicalAbility> GetbyClient(int id);

        [Post("/PhysicalAbility/Post")]
        Task<HttpResponseMessage> Post([Body] PostPhysicalAbility model);

        [Put("/PhysicalAbility/Put")]
        Task<HttpResponseMessage> Put([Body] PostPhysicalAbility model);

        [Delete("/PhysicalAbility/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
