using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffSpotCheck
{
    public interface IStaffSpotCheckService
    {
        [Get("/StaffSpotCheck")]
        Task<List<GetStaffSpotCheck>> Get();

        [Get("/StaffSpotCheck/Get/{id}")]
        Task<GetStaffSpotCheck> Get(int id);

        [Post("/StaffSpotCheck/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffSpotCheck model);

        [Put("/StaffSpotCheck")]
        Task<GetStaffSpotCheck> Put([Body] PutStaffSpotCheck model);
    }
}
