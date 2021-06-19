using AwesomeCare.DataTransferObject.DTOs.WhisttleBlower;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.WhisttleBlower
{
    public interface IWhisttleBlowerService
    {
        [Get("/WhisttleBlower")]
        Task<List<GetWhisttleBlower>> Get();

        [Get("/WhisttleBlower/Get/{id}")]
        Task<GetWhisttleBlower> Get(int id);

        [Post("/WhisttleBlower/Create")]
        Task<HttpResponseMessage> Create([Body] PostWhisttleBlower model);

        [Put("/WhisttleBlower")]
        Task<GetWhisttleBlower> Put([Body] PutWhisttleBlower model);
    }
}
