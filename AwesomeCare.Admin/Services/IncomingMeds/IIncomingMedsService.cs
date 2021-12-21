using AwesomeCare.DataTransferObject.DTOs.IncomingMeds;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.IncomingMeds
{
    public interface IIncomingMedsService
    {
        [Get("/IncomingMeds")]
        Task<List<GetIncomingMeds>> Get();

        [Get("/IncomingMeds/Get/{id}")]
        Task<GetIncomingMeds> Get(int id);

        [Post("/IncomingMeds/Create")]
        Task<HttpResponseMessage> Create([Body] PostIncomingMeds model);

        [Put("/IncomingMeds")]
        Task<GetIncomingMeds> Put([Body] PutIncomingMeds model);
    }
}
