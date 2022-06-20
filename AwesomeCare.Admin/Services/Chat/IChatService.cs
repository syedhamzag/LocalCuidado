using AwesomeCare.DataTransferObject.DTOs.Chat;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Chat
{
    public interface IChatService
    {
        [Get("/Chat")]
        Task<List<GetChat>> Get();

        [Get("/Chat/Get/{id}")]
        Task<GetChat> Get(int id);

        [Post("/Chat/Create")]
        Task<HttpResponseMessage> Create([Body] PostChat model);

        [Put("/Chat/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutChat> model);
    }
}
