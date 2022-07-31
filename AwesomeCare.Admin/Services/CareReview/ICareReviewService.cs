using AwesomeCare.DataTransferObject.DTOs.CareReview;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.CareReview
{
    public interface ICareReviewService
    {
        [Get("/CareReview")]
        Task<List<GetCareReview>> Get();

        [Get("/CareReview/Get/{id}")]
        Task<GetCareReview> Get(int id);

        [Get("/CareReview/GetByClient/{id}")]
        Task<GetCareReview> GetByClient(int id);

        [Post("/CareReview/Post")]
        Task<HttpResponseMessage> Post([Body] PostCareReview model);

        [Put("/CareReview/Put")]
        Task<HttpResponseMessage> Put([Body] PutCareReview model);
    }
}
