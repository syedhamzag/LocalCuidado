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

        [Get("/Client/GetClientDetails")]
        Task<List<GetClientDetail>> GetClientDetail();

    }
}
