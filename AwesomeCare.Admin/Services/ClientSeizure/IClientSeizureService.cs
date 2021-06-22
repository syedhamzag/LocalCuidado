using AwesomeCare.DataTransferObject.DTOs.ClientSeizure;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientSeizure
{
    public interface IClientSeizureService
    {
        [Get("/ClientSeizure")]
        Task<List<GetClientSeizure>> Get();

        [Get("/ClientSeizure/Get/{id}")]
        Task<GetClientSeizure> Get(int id);

        [Post("/ClientSeizure/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientSeizure model);

        [Put("/ClientSeizure/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientSeizure model);
    }
}
