using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using Refit;

namespace AwesomeCare.Admin.Services.ClientRota
{
   public interface IClientRotaService
    {
        [Get("/ClientRota")]
        Task<List<GetClientRota>> Get();

        [Post("/ClientRota")]
        Task<GetClientRota> Post([Body]PostClientRota model);

        [Get("/ClientRota/{id}")]
        Task<GetClientRota> Get(int id);

        [Put("/ClientRota")]
        Task<GetClientRota> Put([Body]PutClientRota model);
    }
}
