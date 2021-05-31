using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.Services.ClientRotaName
{
  public  interface IClientRotaNameService
    {
        [Get("/ClientRotaName")]
        Task<List<GetClientRotaName>> Get();

        [Post("/ClientRotaName")]
        Task<GetClientRotaName> Post([Body]PostClientRotaName model);

        [Get("/ClientRotaName/{id}")]
        Task<GetClientRotaName> Get(int id);

        [Put("/ClientRotaName")]
        Task<GetClientRotaName> Put([Body]PutClientRotaName model);
    }
}
