using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.HealthAndLiving
{
    public interface IHealthAndLivingService
    {
        [Get("/HealthAndLiving")]
        Task<List<GetHealthAndLiving>> Get();

        [Get("/HealthAndLiving/Get/{id}")]
        Task<GetHealthAndLiving> Get(int id);

        [Get("/HealthAndLiving/GetbyClient/{id}")]
        Task<GetHealthAndLiving> GetbyClient(int id);

        [Post("/HealthAndLiving/Create")]
        Task<HttpResponseMessage> Create([Body] PostHealthAndLiving model);

        [Put("/HealthAndLiving/Put")]
        Task<HttpResponseMessage> Put([Body] PutHealthAndLiving model);
    }
}
