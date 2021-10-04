using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.DutyOnCall
{
    public interface IDutyOnCallService
    {
        [Get("/DutyOnCall")]
        Task<List<GetDutyOnCall>> Get();

        [Get("/DutyOnCall/Get/{id}")]
        Task<GetDutyOnCall> Get(int id);

        [Post("/DutyOnCall/Create")]
        Task<HttpResponseMessage> Create([Body] PostDutyOnCall model);

        [Put("/DutyOnCall")]
        Task<HttpResponseMessage> Put([Body] PutDutyOnCall model);
    }
}
