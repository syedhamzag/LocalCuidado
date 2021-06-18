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

        [Get("/ClientProgram/GetByRef/{Reference}")]
        Task<List<GetClientProgram>> GetByRef(string Reference);

        [Get("/ClientProgram")]
        Task<List<GetClientProgram>> Get();

        [Get("/ClientProgram/Get/{id}")]
        Task<GetClientProgram> Get(int id);

        [Post("/ClientProgram/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostClientProgram> model);

        [Put("/ClientProgram/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutClientProgram> model);
    }
}
