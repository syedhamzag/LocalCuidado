using AwesomeCare.DataTransferObject.DTOs.ClientBMIChart;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientBMIChart
{
    public interface IClientBMIChartService
    {
        [Get("/ClientBMIChart")]
        Task<List<GetClientBMIChart>> Get();

        [Get("/ClientBMIChart/Get/{id}")]
        Task<GetClientBMIChart> Get(int id);

        [Post("/ClientBMIChart/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientBMIChart model);

        [Put("/ClientBMIChart/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientBMIChart model);
    }
}
