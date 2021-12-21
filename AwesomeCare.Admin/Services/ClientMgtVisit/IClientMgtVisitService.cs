using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientMgtVisit
{
    public interface IClientMgtVisitService
    {
        [Get("/ClientMgtVisit")]
        Task<List<GetClientMgtVisit>> Get();

        [Get("/ClientMgtVisit/Get/{id}")]
        Task<GetClientMgtVisit> Get(int id);

        [Post("/ClientMgtVisit/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientMgtVisit model);

        [Put("/ClientMgtVisit/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientMgtVisit model);
    }
}
