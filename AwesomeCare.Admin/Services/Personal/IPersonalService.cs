using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Personal
{
    public interface IPersonalService
    {
        [Get("/Personal")]
        Task<List<GetPersonal>> Get();

        [Get("/Personal/Get/{id}")]
        Task<GetPersonal> Get(int id);

        [Post("/Personal/Create")]
        Task<HttpResponseMessage> Create([Body] PostPersonal model);

        [Put("/Personal/Put")]
        Task<HttpResponseMessage> Put([Body] PostPersonal model);
    }
}
