using AwesomeCare.DataTransferObject.DTOs.CarePlanNutrition;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.CarePlanNutrition
{
    public interface ICarePlanNutritionService
    {
        [Get("/CarePlanNutrition")]
        Task<List<GetCarePlanNutrition>> Get();

        [Get("/CarePlanNutrition/Get/{id}")]
        Task<GetCarePlanNutrition> Get(int id);

        [Post("/CarePlanNutrition/Create")]
        Task<HttpResponseMessage> Create([Body] PostCarePlanNutrition model);

        [Put("/CarePlanNutrition/Put")]
        Task<HttpResponseMessage> Put([Body] PutCarePlanNutrition model);
    }
}
