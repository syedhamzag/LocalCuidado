using AwesomeCare.DataTransferObject.DTOs.Enotice;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Enotice
{
    public interface IEnoticeService
    {
        [Get("/Enotice")]
        Task<List<GetEnotice>> Get();

        [Get("/Enotice/Get/{id}")]
        Task<GetEnotice> Get(int id);

        [Post("/Enotice/Create")]
        Task<HttpResponseMessage> Create([Body] PostEnotice model);

        [Put("/Enotice")]
        Task<GetEnotice> Put([Body] PutEnotice model);
    }
}
