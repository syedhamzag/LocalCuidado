using AwesomeCare.DataTransferObject.DTOs.Staff.Lateness;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffLatenessService
    {
        [Get("/StaffLateness")]
        Task<List<GetStaffLateness>> Get();

        [Get("/StaffLateness/Get/{id}")]
        Task<GetStaffLateness> Get(int id);

        [Post("/StaffLateness/Post")]
        Task<HttpResponseMessage> Post([Body] PostStaffLateness model);

        [Put("/StaffLateness/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffLateness model);
    }
}
