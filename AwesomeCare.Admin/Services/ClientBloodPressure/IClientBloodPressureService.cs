using AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientBloodPressure
{
    public interface IClientBloodPressureService
    {
        [Get("/ClientBloodPressure")]
        Task<List<GetClientBloodPressure>> Get();

        [Get("/ClientBloodPressure/Get/{id}")]
        Task<GetClientBloodPressure> Get(int id);

        [Post("/ClientBloodPressure/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientBloodPressure model);

        [Put("/ClientBloodPressure/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientBloodPressure model);
    }
}
