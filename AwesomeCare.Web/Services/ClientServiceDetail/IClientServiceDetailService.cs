using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs;
using Refit;

namespace AwesomeCare.Web.Services
{
   public interface IClientServiceDetailService
    {
        [Get("/ClientServiceDetail/UserClientService")]
        Task<List<GetClientServiceDetail>> Get();

        [Get("/ClientServiceDetail/UserClientService/{id}")]
        Task<GetClientServiceDetail> GetById(int id);

        [Post("/ClientServiceDetail")]
        Task<HttpResponseMessage> Post([Body] PostClientServiceDetail model);
    }
}
