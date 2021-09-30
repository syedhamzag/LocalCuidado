using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.CarePlanNutrition
{
    public interface IHomeRiskAssessmentService
    {
        
        [Get("/HomeRiskAssessment")]
        Task<List<GetHomeRiskAssessment>> Get();

        [Get("/HomeRiskAssessment/Get/{id}")]
        Task<GetHomeRiskAssessment> Get(int id);

        [Get("/HomeRiskAssessment/GetByClient/{id}")]
        Task<List<GetHomeRiskAssessment>> GetByClient(int id);

        [Post("/HomeRiskAssessment/Create")]
        Task<HttpResponseMessage> Create([Body] PostHomeRiskAssessment model);

        [Put("/HomeRiskAssessment/Put")]
        Task<HttpResponseMessage> Put([Body] PostHomeRiskAssessment model);
    }
}
