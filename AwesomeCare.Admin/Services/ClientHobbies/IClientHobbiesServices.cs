using AwesomeCare.DataTransferObject.DTOs.ClientHobbies;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientHobbies
{
    public interface IClientHobbiesServices
    {
        [Get("/ClientHobbies")]
        Task<List<GetClientHobbies>> Get();

        [Get("/ClientHobbies/Get/{id}")]
        Task<GetClientHobbies> Get(int id);

        [Post("/ClientHobbies/Post")]
        Task<HttpResponseMessage> Post([Body] List<PostClientHobbies> model);

        [Put("/ClientHobbies/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutClientHobbies> model);
    }
}
