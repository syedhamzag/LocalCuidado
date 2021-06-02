using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientProgram
{
    public interface IClientProgramService
    {
        [Get("/ClientProgram")]
        Task<List<GetClientProgram>> Get();

        [Get("/ClientProgram/Get/{id}")]
        Task<GetClientProgram> Get(int id);

        [Post("/ClientProgram/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientProgram model);

        [Put("/ClientProgram")]
        Task<GetClientProgram> Put([Body] PutClientProgram model);
    }
}
