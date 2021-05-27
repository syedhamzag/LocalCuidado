using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientNutrition;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientMealType;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Nutrition
{
   public interface INutritionService
    {
        [Get("/Nutrition")]
        Task<List<GetClientMealType>> Get();

        [Get("/Nutrition/GetForEdit/{id}")]
        Task<List<GetClientNutrition>> GetForEdit(int id);

        [Put("/Nutrition/Edit/{id}")]
        Task<HttpResponseMessage> Edit([Body] CreateNutrition model, int id);

        [Post("/Nutrition/CreateNutrition")]
        Task<HttpResponseMessage> CreateNutrition([Body] CreateNutrition model);
    }
}
