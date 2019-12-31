using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using Refit;

namespace AwesomeCare.Admin.Services.ClientInvolvingPartyBase
{
  public  interface IClientInvolvingPartyBase
    {
        [Get("/ClientInvolvingPartyBase")]
        Task<List<GetClientInvolvingPartyItem>> Get();

        [Post("/ClientInvolvingPartyBase")]
        Task<GetClientInvolvingPartyItem> Post([Body]PostClientInvolvingPartyItem model);

        [Get("/ClientInvolvingPartyBase/{id}")]
        Task<GetClientInvolvingPartyItem> Get(int id);

        [Put("/ClientInvolvingPartyBase")]
        Task<GetClientInvolvingPartyItem> Put([Body]PutClientInvolvingPartyItem model);
    }
}
