using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ConsentCare
{
    public interface IConsentCareService
    {
        [Get("/ConsentCare")]
        Task<List<GetConsentCare>> Get();

        [Get("/ConsentCare/Get/{id}")]
        Task<GetConsentCare> Get(int id);

        [Post("/ConsentCare/Create")]
        Task<HttpResponseMessage> Create([Body] PostConsentCare model);

        [Put("/ConsentCare/Put")]
        Task<HttpResponseMessage> Put([Body] PostConsentCare model);
    }
}
