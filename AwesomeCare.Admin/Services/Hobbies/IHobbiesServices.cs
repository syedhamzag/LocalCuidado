using AwesomeCare.DataTransferObject.DTOs.Hobbies;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Hobbies
{
    public interface IHobbiesServices
    {
        [Get("/Hobbies")]
        Task<List<GetHobbies>> Get();

        [Get("/Hobbies/Get/{id}")]
        Task<GetHobbies> Get(int id);

        [Post("/Hobbies/Post")]
        Task<HttpResponseMessage> Post([Body] PostHobbies model);

        [Put("/Hobbies/Put")]
        Task<HttpResponseMessage> Put([Body] PutHobbies model);

        [Delete("/Hobbies/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
