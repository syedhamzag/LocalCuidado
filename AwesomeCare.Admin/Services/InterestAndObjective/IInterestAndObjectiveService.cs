using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.InterestAndObjective
{
    public interface IInterestAndObjectiveService
    {
        [Get("/InterestAndObjective")]
        Task<List<GetInterestAndObjective>> Get();

        [Get("/InterestAndObjective/Get/{id}")]
        Task<GetInterestAndObjective> Get(int id);

        [Post("/InterestAndObjective/Create")]
        Task<HttpResponseMessage> Create([Body] PostInterestAndObjective model);

        [Put("/InterestAndObjective/Put")]
        Task<HttpResponseMessage> Put([Body] PutInterestAndObjective model);
    }
}
