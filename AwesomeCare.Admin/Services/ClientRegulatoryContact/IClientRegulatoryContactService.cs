using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientRegulatoryContact
{
   public interface IClientRegulatoryContactService
    {
        [Get("/ClientRegulatoryContact")]
        Task<List<GetClientRegulatoryContact>> Get();

        [Post("/ClientRegulatoryContact")]
        Task<GetClientRegulatoryContact> Post([Body]PostClientRegulatoryContact model);

        [Get("/ClientRegulatoryContact/{id}")]
        Task<GetClientRegulatoryContact> Get(int id);

        [Post("/ClientRegulatoryContact")]
        Task<HttpResponseMessage> Post([Body]List<PostClientRegulatoryContact> models);
    }
}
