using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
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

        [Get("/Client/GetClientDetails")]
        Task<List<GetClientDetail>> GetClientDetail();

        [Get("/Client/GetClient/{id}")]
        Task<GetClient> GetClient(int id);

        [Get("/Client/{clientId}")]
        Task<GetClientForEdit> GetClientForEdit(int clientId);

        [Post("/Client")]
        Task<GetClient> PostClient([Body]PostClient client);

        [Put("/Client/{clientId}")]
        Task<int> PutClient([Body]PutClient client,int clientId);

        [Get("/ClientInvolvingPartyBase")]
        Task<List<GetClientInvolvingPartyItem>> GetClientInvolvingPartyBase();

        [Get("/ClientInvolvingPartyBase/{id}")]
        Task<GetClientInvolvingPartyItem> GetClientInvolvingPartyBase(int id);

        #region ClientMedication
        [Get("/Client/Medication/{clientId}")]
        Task<List<GetClientMedication>> GetMedications(int clientId);

        [Post("/Client/Medication")]
        Task<HttpResponseMessage> PostMedication([Body]PostClientMedication model);

        [Get("/Client/Medication/{clientId}/{id}")]
        Task<GetClientMedication> GetMedication( int clientId, int id);

        [Put("/Client/Medication")]
        Task<GetClientMedication> PutMedication([Body]PutClientMedication model);
        #endregion
    }
}
