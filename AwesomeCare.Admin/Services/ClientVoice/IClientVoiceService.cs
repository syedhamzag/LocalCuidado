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
        [Get("/ClientVoice")]
        Task<List<GetClientVoice>> Get();

        [Get("/ClientVoice/Get/{id}")]
        Task<GetClientVoice> Get(int id);

        [Post("/ClientVoice/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientVoice model);

        [Put("/ClientVoice")]
        Task<GetClientVoice> Put([Body] PutClientVoice model);
    }
}
