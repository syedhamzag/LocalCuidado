using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffCompetenceTest
{
    public interface IStaffCompetenceTestService
    {
        [Get("/StaffCompetenceTest")]
        Task<List<GetStaffCompetenceTest>> Get();

        [Get("/StaffCompetenceTest/Get/{id}")]
        Task<GetStaffCompetenceTest> Get(int id);

        [Get("/StaffCompetenceTest/GetByStaffPersonalInfo/{id}")]
        Task<List<GetStaffCompetenceTest>> GetByStaffPersonalInfo(int id);

        [Post("/StaffCompetenceTest/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffCompetenceTest model);

        [Put("/StaffCompetenceTest/Put")]
        Task<HttpResponseMessage> Put([Body] PostStaffCompetenceTest model);
    }
}
