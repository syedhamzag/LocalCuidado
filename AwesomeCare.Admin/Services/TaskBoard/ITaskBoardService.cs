using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.TaskBoard
{
    public interface ITaskBoardService
    {
        [Get("/TaskBoard")]
        Task<List<GetTaskBoard>> Get();

        [Get("/TaskBoard/GetPin")]
        Task<GetRotaPin> GetPin();

        [Post("/TaskBoard/ChangePin")]
        Task<HttpResponseMessage> ChangePin([Body] PostRotaPin model);

        [Get("/TaskBoard/GetWithStaff")]
        Task<List<GetTaskBoard>> GetWithStaff();

        [Get("/TaskBoard/Get/{id}")]
        Task<GetTaskBoard> Get(int id);

        [Post("/TaskBoard/Create")]
        Task<HttpResponseMessage> Create([Body] PostTaskBoard model);

        [Put("/TaskBoard/Put")]
        Task<HttpResponseMessage> Put([Body] PutTaskBoard model);

        [Delete("/TaskBoard/Delete")]
        Task<HttpResponseMessage> Delete([Body] List<GetTaskBoard> model);
    }
}
