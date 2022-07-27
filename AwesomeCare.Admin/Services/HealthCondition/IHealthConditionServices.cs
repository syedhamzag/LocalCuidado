using AwesomeCare.DataTransferObject.DTOs.HealthCondition;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.HealthCondition
{
    public interface IHealthConditionServices
    {
        [Get("/HealthCondition")]
        Task<List<GetHealthCondition>> Get();

        [Get("/HealthCondition/Get/{id}")]
        Task<GetHealthCondition> Get(int id);

        [Post("/HealthCondition/Post")]
        Task<HttpResponseMessage> Post([Body] PostHealthCondition model);

        [Put("/HealthCondition/Put")]
        Task<HttpResponseMessage> Put([Body] PutHealthCondition model);

        [Delete("/HealthCondition/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
