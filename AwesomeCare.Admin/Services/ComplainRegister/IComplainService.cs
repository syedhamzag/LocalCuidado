using AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ComplainRegister
{
   public interface IComplainService
    {
        [Get("/Complain")]
        Task<List<GetClientComplainRegister>> Get();

        [Get("/Complain/Get/{id}")]
        Task<GetClientComplainRegister> Get(int id);

        [Post("/Complain/Create")]
        Task<HttpResponseMessage> Create([Body] PostComplainRegister model);

        [Put("/Complain/Put")]
        Task<HttpResponseMessage> Put([Body] PutComplainRegister model);
    }
}
