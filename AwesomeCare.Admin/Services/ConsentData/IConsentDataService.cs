using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ConsentData
{
    public interface IConsentDataService
    {
        [Get("/ConsentData")]
        Task<List<GetConsentData>> Get();

        [Get("/ConsentData/Get/{id}")]
        Task<GetConsentData> Get(int id);

        [Post("/ConsentData/Create")]
        Task<HttpResponseMessage> Create([Body] PostConsentData model);

        [Put("/ConsentData/Put")]
        Task<HttpResponseMessage> Put([Body] PutConsentData model);
    }
}
