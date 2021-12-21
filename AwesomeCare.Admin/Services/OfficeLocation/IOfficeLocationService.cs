using AwesomeCare.DataTransferObject.DTOs.OfficeLocation;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.OfficeLocation
{
   public interface IOfficeLocationService
    {
        [Get("/OfficeLocation")]
        Task<List<GetOfficeLocation>> GetAsync();

        [Get("/OfficeLocation/{id}")]
        Task<GetOfficeLocation> GetAsync(int id);

        [Post("/OfficeLocation")]
        Task<GetOfficeLocation> PostAsync([Body] PostOfficeLocation model);

        [Put("/OfficeLocation")]
        Task<HttpResponseMessage> PutAsync([Body] PutOfficeLocation model);
    }
}
