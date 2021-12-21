using AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientBodyTemp
{
    public interface IClientBodyTempService
    {
        [Get("/ClientBodyTemp")]
        Task<List<GetClientBodyTemp>> Get();

        [Get("/ClientBodyTemp/Get/{id}")]
        Task<GetClientBodyTemp> Get(int id);

        [Post("/ClientBodyTemp/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientBodyTemp model);

        [Put("/ClientBodyTemp/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientBodyTemp model);
    }
}
