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
        Task<List<GetOfficeLocation>> Get();

        [Get("/OfficeLocation/{id}")]
        Task<GetOfficeLocation> Get(int id);

        [Post("/OfficeLocation")]
        Task<GetOfficeLocation> Post([Body] PostOfficeLocation model);

        [Put("/OfficeLocation")]
        Task<HttpResponseMessage> Put([Body] PutOfficeLocation model);
    }
}
