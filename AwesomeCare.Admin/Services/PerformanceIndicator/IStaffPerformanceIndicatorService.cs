using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.PerformanceIndicator
{
    public interface IStaffPerformanceIndicatorService
    {
        [Get("/StaffPerformanceIndicator")]
        Task<List<GetPerformanceIndicator>> Get();

        [Get("/StaffPerformanceIndicator/Get/{id}")]
        Task<GetPerformanceIndicator> Get(int id);

        [Get("/StaffPerformanceIndicator/GetByStaffPersonalInfo/{id}")]
        Task<List<GetPerformanceIndicator>> GetByStaffPersonalInfo(int id);

        [Post("/StaffPerformanceIndicator/Create")]
        Task<HttpResponseMessage> Create([Body] PostPerformanceIndicator model);

        [Put("/StaffPerformanceIndicator/Put")]
        Task<HttpResponseMessage> Put([Body] PostPerformanceIndicator model);
    }
}
