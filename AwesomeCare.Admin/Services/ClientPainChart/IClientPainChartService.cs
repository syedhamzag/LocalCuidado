using AwesomeCare.DataTransferObject.DTOs.ClientPainChart;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientPainChart
{
    public interface IClientPainChartService
    {
        [Get("/ClientPainChart")]
        Task<List<GetClientPainChart>> Get();

        [Get("/ClientPainChart/Get/{id}")]
        Task<GetClientPainChart> Get(int id);

        [Post("/ClientPainChart/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientPainChart model);

        [Put("/ClientPainChart/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientPainChart model);
    }
}
