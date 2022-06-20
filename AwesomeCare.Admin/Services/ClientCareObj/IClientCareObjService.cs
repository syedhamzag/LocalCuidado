using AwesomeCare.DataTransferObject.DTOs.Client.CareObj;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientCareObj
{
    public interface IClientCareObjService
    {
        [Get("/ClientCareObj")]
        Task<List<GetClientCareObj>> Get();

        [Get("/ClientCareObj/Get/{id}")]
        Task<GetClientCareObj> Get(int id);

        [Get("/ClientCareObj/GetByClient/{id}")]
        Task<List<GetClientCareObj>> GetByClient(int id);

        [Post("/ClientCareObj/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientCareObj model);

        [Put("/ClientCareObj/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientCareObj model);
    }
}
