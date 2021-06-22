using AwesomeCare.DataTransferObject.DTOs.Resources;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Resources
{
    public interface IResourcesService
    {
        [Get("/Resources")]
        Task<List<GetResources>> Get();

        [Get("/Resources/Get/{id}")]
        Task<GetResources> Get(int id);

        [Post("/Resources/Create")]
        Task<HttpResponseMessage> Create([Body] PostResources model);

        [Put("/Resources")]
        Task<GetResources> Put([Body] PutResources model);
    }
}
