using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffSurvey
{
    public interface IStaffSurveyService
    {
        [Get("/StaffSurvey")]
        Task<List<GetStaffSurvey>> Get();

        [Get("/StaffSurvey/Get/{id}")]
        Task<GetStaffSurvey> Get(int id);

        [Post("/StaffSurvey/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffSurvey model);

        [Put("/StaffSurvey/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffSurvey model);
    }
}
