using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Investigation;
using Refit;


namespace AwesomeCare.Admin.Services.Investigation
{
  public  interface IInvestigationService
    {
        [Get("/Investigation")]
        Task<List<GetInvestigation>> Get();

        [Get("/Investigation/{id}")]
        Task<GetInvestigation> Get(int id);

        [Post("/Investigation")]
        Task<HttpResponseMessage> Post([Body] PostInvestigation model);
    }
}
