using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientInvolvingParty
{
  public  interface IClientInvolvingParty
    {
        [Get("/ClientInvolvingParty/{id}")]
        Task<GetClientInvolvingParty> Get(int id);

        [Get("/ClientInvolvingParty")]
        Task<List<GetClientInvolvingParty>> GetAll();
        [Post("/ClientInvolvingParty")]
        Task<GetClientInvolvingParty> Post([Body]PostClientInvolvingParty model);

        [Post("/ClientInvolvingParty")]
        Task<HttpResponseMessage> Post([Body]List<PostClientInvolvingParty> models);

        [Put("/ClientInvolvingParty")]
        Task<HttpResponseMessage> Put([Body] List<PutClientInvolvingParty> models);
    }
}
