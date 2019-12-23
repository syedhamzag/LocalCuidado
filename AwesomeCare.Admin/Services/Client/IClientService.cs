using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Client
{
   public interface IClientService
    {

        [Get("/Client")]
        Task<List<GetClient>> GetClients();

        [Get("/Client/{id}")]
        Task<GetClient> GetClient(int id);
        [Get("/Client/{clientId}")]
        Task<GetClientForEdit> GetClientForEdit(int clientId);

        [Post("/Client")]
        Task<GetClient> PostClient([Body]PostClient client);

        [Get("/ClientInvolvingPartyBase")]
        Task<List<GetClientInvolvingPartyItem>> GetClientInvolvingPartyBase();

        [Get("/ClientInvolvingPartyBase/{id}")]
        Task<GetClientInvolvingPartyItem> GetClientInvolvingPartyBase(int id);
    }
}
