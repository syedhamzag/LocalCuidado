using AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientBowelMovement
{
    public interface IClientBowelMovementService
    {
        [Get("/ClientBowelMovement")]
        Task<List<GetClientBowelMovement>> Get();

        [Get("/ClientBowelMovement/Get/{id}")]
        Task<GetClientBowelMovement> Get(int id);

        [Post("/ClientBowelMovement/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientBowelMovement model);

        [Put("/ClientBowelMovement/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientBowelMovement model);
    }
}
