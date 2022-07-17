using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.HistoryOfFall
{
    public interface IHistoryOfFallService
    {
        [Get("/HistoryOfFall")]
        Task<List<GetHistoryOfFall>> Get();

        [Get("/HistoryOfFall/Get/{id}")]
        Task<GetHistoryOfFall> Get(int id);

        [Get("/HistoryOfFall/GetbyClient/{id}")]
        Task<GetHistoryOfFall> GetbyClient(int id);

        [Post("/HistoryOfFall/Create")]
        Task<HttpResponseMessage> Create([Body] PostHistoryOfFall model);

        [Put("/HistoryOfFall/Put")]
        Task<HttpResponseMessage> Put([Body] PutHistoryOfFall model);

        [Delete("/HistoryOfFall/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
