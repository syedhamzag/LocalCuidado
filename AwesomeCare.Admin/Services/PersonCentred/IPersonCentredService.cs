using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.PersonCentred
{
    public interface IPersonCentredService
    {
        [Get("/PersonCentred")]
        Task<List<GetPersonCentred>> Get();

        [Get("/PersonCentred/Get/{id}")]
        Task<GetPersonCentred> Get(int id);

        [Post("/PersonCentred/Create")]
        Task<HttpResponseMessage> Create([Body] PostPersonCentred model);

        [Put("/PersonCentred/Put")]
        Task<HttpResponseMessage> Put([Body] PutPersonCentred model);
    }
}
