using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ConsentLandLine
{
    public interface IConsentLandLineService
    {
        [Get("/ConsentLandLine")]
        Task<List<GetConsentLandLine>> Get();

        [Get("/ConsentLandLine/Get/{id}")]
        Task<GetConsentLandLine> Get(int id);

        [Post("/ConsentLandLine/Create")]
        Task<HttpResponseMessage> Create([Body] PostConsentLandLine model);

        [Put("/ConsentLandLine/Put")]
        Task<HttpResponseMessage> Put([Body] PutConsentLandLine model);
    }
}
