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

        [Get("/GetComplain/{clientId}/{complainId}")]
        Task<GetClientComplainRegister> GetComplain(int clientId, int complainId);

        [Post("/Complain")]
        Task<HttpResponseMessage> PostComplainRegister([Body] PostComplainRegister model);

        [Put("/Complain")]
        Task<GetClientComplainRegister> Put([Body] PutComplainRegister model);
    }
}
