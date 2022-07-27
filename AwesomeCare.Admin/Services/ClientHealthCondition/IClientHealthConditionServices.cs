using AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientHealthCondition
{
    public interface IClientHealthConditionServices
    {
        [Get("/ClientHealthCondition")]
        Task<List<GetClientHealthCondition>> Get();

        [Get("/ClientHealthCondition/Get/{id}")]
        Task<GetClientHealthCondition> Get(int id);

        [Post("/ClientHealthCondition/Post")]
        Task<HttpResponseMessage> Post([Body] List<PostClientHealthCondition> model);

        [Put("/ClientHealthCondition/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutClientHealthCondition> model);
    }
}
