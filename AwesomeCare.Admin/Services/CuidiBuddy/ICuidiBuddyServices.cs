using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.CuidiBuddy;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.CuidiBuddy
{
    public interface ICuidiBuddyServices
    {
        [Get("/CuidiBuddy")]
        Task<List<GetCuidiBuddy>> Get();
        
        [Get("/CuidiBuddy/GetCuidi")]    
        Task<List<GetClient>> GetCuidi();

        [Get("/CuidiBuddy/Get/{id}")]
        Task<GetCuidiBuddy> Get(int id);

        [Post("/CuidiBuddy/Post")]
        Task<HttpResponseMessage> Post([Body] PostCuidiBuddy model);

        [Put("/CuidiBuddy/Put")]
        Task<HttpResponseMessage> Put([Body] PutCuidiBuddy model);
    }
}
