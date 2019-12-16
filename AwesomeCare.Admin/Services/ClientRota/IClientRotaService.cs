using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientRota
{
   public interface IClientRotaService
    {
        [Post("/ClientRota/CreateRota")]
        Task<HttpResponseMessage> CreateRota([Body]List<CreateClientRota> model);
    }
}
