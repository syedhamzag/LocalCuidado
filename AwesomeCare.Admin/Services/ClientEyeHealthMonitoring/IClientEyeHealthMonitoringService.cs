using AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientEyeHealthMonitoring
{
    public interface IClientEyeHealthMonitoringService
    {
        [Get("/ClientEyeHealthMonitoring")]
        Task<List<GetClientEyeHealthMonitoring>> Get();

        [Get("/ClientEyeHealthMonitoring/Get/{id}")]
        Task<GetClientEyeHealthMonitoring> Get(int id);

        [Post("/ClientEyeHealthMonitoring/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientEyeHealthMonitoring model);

        [Put("/ClientEyeHealthMonitoring/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientEyeHealthMonitoring model);
    }
}
