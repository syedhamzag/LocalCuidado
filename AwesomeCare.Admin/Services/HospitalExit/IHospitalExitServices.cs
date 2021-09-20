using AwesomeCare.DataTransferObject.DTOs.HospitalExit;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.HospitalExit
{
    public interface IHospitalExitServices
    {
        [Get("/HospitalExit")]
        Task<List<GetHospitalExit>> Get();

        [Get("/HospitalExit/Get/{id}")]
        Task<GetHospitalExit> Get(int id);

        [Post("/HospitalExit/Create")]
        Task<HttpResponseMessage> Create([Body] PostHospitalExit model);

        [Put("/HospitalExit/Put")]
        Task<HttpResponseMessage> Put([Body] PutHospitalExit model);
    }
}
