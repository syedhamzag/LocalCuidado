using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientVoice
{
    public interface IClientVoiceService
    {
        [Get("/ClientVoice/GetByRef/{Reference}")]
        Task<List<GetClientVoice>> GetByRef(string Reference);

        [Get("/ClientVoice")]
        Task<List<GetClientVoice>> Get();

        [Get("/ClientVoice/Get/{id}")]
        Task<GetClientVoice> Get(int id);

        [Post("/ClientVoice/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostClientVoice> model);

        [Put("/ClientVoice/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutClientVoice> model);
    }
}
