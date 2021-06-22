using AwesomeCare.DataTransferObject.DTOs.ClientPulseRate;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientPulseRate
{
    public interface IClientPulseRateService
    {
        [Get("/ClientPulseRate")]
        Task<List<GetClientPulseRate>> Get();

        [Get("/ClientPulseRate/Get/{id}")]
        Task<GetClientPulseRate> Get(int id);

        [Post("/ClientPulseRate/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientPulseRate model);

        [Put("/ClientPulseRate/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientPulseRate model);
    }
}
