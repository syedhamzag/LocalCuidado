using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ManagingTasks
{
    public interface IManagingTasksService
    {
        [Get("/ManagingTasks")]
        Task<List<GetManagingTasks>> Get();

        [Get("/ManagingTasks/Get/{id}")]
        Task<GetManagingTasks> Get(int id);

        [Post("/ManagingTasks/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostManagingTasks> model);

        [Put("/ManagingTasks/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutManagingTasks> model);
    }
}
