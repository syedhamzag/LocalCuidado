using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.BestInterestAssessment
{
    public interface IBestInterestAssessmentService
    {
        [Get("/BestInterestAssessment")]
        Task<List<GetBestInterestAssessment>> Get();

        [Get("/BestInterestAssessment/Get/{id}")]
        Task<GetBestInterestAssessment> Get(int id);

        [Post("/BestInterestAssessment/Create")]
        Task<HttpResponseMessage> Create([Body] PostBestInterestAssessment model);

        [Put("/BestInterestAssessment/Put")]
        Task<HttpResponseMessage> Put([Body] PostBestInterestAssessment model);
    }
}
