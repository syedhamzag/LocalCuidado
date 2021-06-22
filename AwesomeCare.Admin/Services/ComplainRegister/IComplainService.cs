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

        [Get("/Complain/{complainId}")]
        Task<GetClientComplainRegister> Get(int complainId);

        [Get("/Complain/GetComplain/{clientId}/{complainId}")]
        Task<GetClientComplainRegister> GetComplain(int clientId, int complainId);

        [Post("/Complain/Create")]
        Task<HttpResponseMessage> Create([Body] PostComplainRegister model);

        [Put("/Complain/Put")]
        Task<HttpResponseMessage> Put([Body] PutComplainRegister model);
    }
}
