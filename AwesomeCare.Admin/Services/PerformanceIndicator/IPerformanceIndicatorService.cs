using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.PerformanceIndicator
{
    public interface IPerformanceIndicatorService
    {
        [Get("/PerformanceIndicator")]
        Task<List<GetPerformanceIndicator>> Get();

        [Get("/PerformanceIndicator/Get/{id}")]
        Task<GetPerformanceIndicator> Get(int id);

        [Post("/PerformanceIndicator/Create")]
        Task<HttpResponseMessage> Create([Body] PostPerformanceIndicator model);

        [Post("/PerformanceIndicator/Edit")]
        Task<HttpResponseMessage> Edit([Body] PostPerformanceIndicator model);

        [Put("/PerformanceIndicator/Put")]
        Task<HttpResponseMessage> Put([Body] PutPerformanceIndicator model);

        [Delete("/PerformanceIndicator/DeleteTask/{id}")]
        Task<HttpResponseMessage> DeleteTask(int id);
    }
}
