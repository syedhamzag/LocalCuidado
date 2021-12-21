using AwesomeCare.DataTransferObject.DTOs.StaffInterview;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffInterview
{
    public interface IStaffInterviewService
    {
        [Get("/StaffInterview")]
        Task<List<GetStaffInterview>> Get();

        [Get("/StaffInterview/Get/{id}")]
        Task<GetStaffInterview> Get(int id);

        [Get("/StaffInterview/GetByStaffPersonalInfo/{id}")]
        Task<List<GetStaffInterview>> GetByStaffPersonalInfo(int id);

        [Post("/StaffInterview/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffInterview model);

        [Put("/StaffInterview/Put")]
        Task<HttpResponseMessage> Put([Body] PostStaffInterview model);
    }
}
