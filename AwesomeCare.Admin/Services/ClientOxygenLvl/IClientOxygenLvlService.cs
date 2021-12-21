using AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientOxygenLvl
{
    public interface IClientOxygenLvlService
    {
        [Get("/ClientOxygenLvl")]
        Task<List<GetClientOxygenLvl>> Get();

        [Get("/ClientOxygenLvl/Get/{id}")]
        Task<GetClientOxygenLvl> Get(int id);

        [Post("/ClientOxygenLvl/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientOxygenLvl model);

        [Put("/ClientOxygenLvl/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientOxygenLvl model);
    }
}
