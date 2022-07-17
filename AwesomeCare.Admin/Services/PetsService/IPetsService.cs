using AwesomeCare.DataTransferObject.DTOs.Pets;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Pets
{
    public interface IPetsService
    {
        [Get("/Pets")]
        Task<List<GetPets>> Get();

        [Get("/Pets/Get/{id}")]
        Task<GetPets> Get(int id);

        [Get("/Pets/GetbyClient/{id}")]
        Task<GetPets> GetbyClient(int id);

        [Post("/Pets/Create")]
        Task<HttpResponseMessage> Create([Body] PostPets model);

        [Put("/Pets/Put")]
        Task<HttpResponseMessage> Put([Body] PutPets model);

        [Delete("/Pets/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
