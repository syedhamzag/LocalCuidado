using AwesomeCare.DataTransferObject.DTOs.ClientDailyTask;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientDailyTask
{
    public interface IClientDailyTaskService
    {
        [Get("/ClientDailyTask")]
        Task<List<GetClientDailyTask>> Get();

        [Get("/ClientDailyTask/Get/{id}")]
        Task<GetClientDailyTask> Get(int id);

        [Post("/ClientDailyTask/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientDailyTask model);

        [Put("/ClientDailyTask/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientDailyTask model);
    }
}
