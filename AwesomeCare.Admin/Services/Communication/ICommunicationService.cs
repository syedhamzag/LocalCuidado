using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Communication;
using Refit;

namespace AwesomeCare.Admin.Services
{
   public interface ICommunicationService
    {
        [Get("/Communication")]
        Task<HttpResponseMessage> Get();
        [Post("/Communication")]
        Task<HttpResponseMessage> Post([Body]PostCommunication model);
    }
}
