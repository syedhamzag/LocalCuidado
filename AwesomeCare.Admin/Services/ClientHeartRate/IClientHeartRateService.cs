using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientHeartRate
{
    public interface IClientHeartRateService
    {
        [Get("/ClientHeartRate")]
        Task<List<GetClientHeartRate>> Get();

        [Get("/ClientHeartRate/Get/{id}")]
        Task<GetClientHeartRate> Get(int id);

        [Post("/ClientHeartRate/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientHeartRate model);

        [Put("/ClientHeartRate/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientHeartRate model);
    }
}
