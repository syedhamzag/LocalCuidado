using AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientFoodIntake
{
    public interface IClientFoodIntakeService
    {
        [Get("/ClientFoodIntake")]
        Task<List<GetClientFoodIntake>> Get();

        [Get("/ClientFoodIntake/Get/{id}")]
        Task<GetClientFoodIntake> Get(int id);

        [Post("/ClientFoodIntake/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientFoodIntake model);

        [Put("/ClientFoodIntake/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientFoodIntake model);
    }
}
