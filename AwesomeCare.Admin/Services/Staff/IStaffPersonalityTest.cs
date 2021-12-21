using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffPersonalityTest
    {
        [Get("/StaffPersonalityTest")]
        Task<List<GetStaffPersonalityTest>> Get();

        [Get("/StaffPersonalityTest/Get/{id}")]
        Task<List<GetStaffPersonalityTest>> Get(int id);

        [Post("/StaffPersonalityTest/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostStaffPersonalityTest> post);

        [Put("/StaffPersonalityTest/Put")]
        Task<HttpResponseMessage> Put([Body] List<PostStaffPersonalityTest> model);
    }
}
