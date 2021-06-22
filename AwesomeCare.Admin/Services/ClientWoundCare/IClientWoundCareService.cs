using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientWoundCare
{
    public interface IClientWoundCareService
    {
        [Get("/ClientWoundCare")]
        Task<List<GetClientWoundCare>> Get();

        [Get("/ClientWoundCare/Get/{id}")]
        Task<GetClientWoundCare> Get(int id);

        [Post("/ClientWoundCare/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientWoundCare model);

        [Put("/ClientWoundCare/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientWoundCare model);
    }
}
