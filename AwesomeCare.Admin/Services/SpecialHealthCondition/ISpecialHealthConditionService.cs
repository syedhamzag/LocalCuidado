using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.SpecialHealthCondition
{
    public interface ISpecialHealthConditionService
    {
        [Get("/SpecialHealthCondition")]
        Task<List<GetSpecialHealthCondition>> Get();

        [Get("/SpecialHealthCondition/Get/{id}")]
        Task<GetSpecialHealthCondition> Get(int id);

        [Get("/SpecialHealthCondition/GetbyClient/{id}")]
        Task<GetSpecialHealthCondition> GetbyClient(int id);

        [Post("/SpecialHealthCondition/Create")]
        Task<HttpResponseMessage> Create([Body] PostSpecialHealthCondition model);

        [Put("/SpecialHealthCondition/Put")]
        Task<HttpResponseMessage> Put([Body] PutSpecialHealthCondition model);
    }
}
