using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Review
{
    public interface IReviewService
    {
        [Get("/Review")]
        Task<List<GetReview>> Get();

        [Get("/Review/Get/{id}")]
        Task<GetReview> Get(int id);

        [Post("/Review/Create")]
        Task<HttpResponseMessage> Create([Body] PostReview model);

        [Put("/Review/Put")]
        Task<HttpResponseMessage> Put([Body] PutReview model);
    }
}
