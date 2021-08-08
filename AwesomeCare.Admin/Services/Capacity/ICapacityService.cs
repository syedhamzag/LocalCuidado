using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Capacity
{
    public interface ICapacityService
    {
        [Get("/Capacity")]
        Task<List<GetCapacity>> Get();

        [Get("/Capacity/Get/{id}")]
        Task<GetCapacity> Get(int id);

        [Post("/Capacity/Create")]
        Task<HttpResponseMessage> Create([Body] PostCapacity model);

        [Put("/Capacity/Put")]
        Task<HttpResponseMessage> Put([Body] PostCapacity model);
    }
}
