using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.PersonalHygiene
{
    public interface IPersonalHygieneService
    {
        [Get("/PersonalHygiene")]
        Task<List<GetPersonalHygiene>> Get();

        [Get("/PersonalHygiene/Get/{id}")]
        Task<GetPersonalHygiene> Get(int id);

        [Post("/PersonalHygiene/Create")]
        Task<HttpResponseMessage> Create([Body] PostPersonalHygiene model);

        [Put("/PersonalHygiene/Put")]
        Task<HttpResponseMessage> Put([Body] PutPersonalHygiene model);
    }
}
