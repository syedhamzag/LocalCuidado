using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Untowards;
using Refit;
namespace AwesomeCare.Admin.Services.Untowards
{
   public interface IUntowardsService
    {
        [Post("/Untowards")]
        Task<HttpResponseMessage> PostUntowards([Body]PostUntowards untowards);
        [Get("/Untowards")]
        Task<List<GetUntowards>> Get();
        [Get("/Untowards/{id}")]
        Task<GetUntowardsDetails> Get(int id);
    }
}
