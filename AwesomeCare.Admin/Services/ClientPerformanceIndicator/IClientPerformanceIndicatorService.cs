using AwesomeCare.DataTransferObject.DTOs.ClientPerformanceIndicator;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientPerformanceIndicator
{
    public interface IClientPerformanceIndicatorService
    {
        [Get("/ClientPerformanceIndicator")]
        Task<List<GetClientPerformanceIndicator>> Get();

        [Get("/ClientPerformanceIndicator/Get/{id}")]
        Task<GetClientPerformanceIndicator> Get(int id);

        [Get("/ClientPerformanceIndicator/GetByClient/{id}")]
        Task<GetClientPerformanceIndicator> GetByClient(int id);

        [Post("/ClientPerformanceIndicator/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientPerformanceIndicator model);

        [Post("/ClientPerformanceIndicator/Edit")]
        Task<HttpResponseMessage> Edit([Body] PostClientPerformanceIndicator model);

        [Put("/ClientPerformanceIndicator/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientPerformanceIndicator model);

        [Delete("/ClientPerformanceIndicator/DeleteTask/{id}")]
        Task<HttpResponseMessage> DeleteTask(int id);
    }
}
