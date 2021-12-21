using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.KeyIndicators
{
    public interface IKeyIndicatorsService
    {
        [Get("/KeyIndicators")]
        Task<List<GetKeyIndicators>> Get();

        [Get("/KeyIndicators/Get/{id}")]
        Task<GetKeyIndicators> Get(int id);

        [Post("/KeyIndicators/Create")]
        Task<HttpResponseMessage> Create([Body] PostKeyIndicators model);

        [Put("/KeyIndicators/Put")]
        Task<HttpResponseMessage> Put([Body] PostKeyIndicators model);
    }
}
