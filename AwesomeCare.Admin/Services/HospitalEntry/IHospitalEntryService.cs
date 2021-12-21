using AwesomeCare.DataTransferObject.DTOs.HospitalEntry;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.HospitalEntry
{
    public interface IHospitalEntryService
    {
        [Get("/HospitalEntry")]
        Task<List<GetHospitalEntry>> Get();

        [Get("/HospitalEntry/Get/{id}")]
        Task<GetHospitalEntry> Get(int id);

        [Post("/HospitalEntry/Create")]
        Task<HttpResponseMessage> Create([Body] PostHospitalEntry model);

        [Put("/HospitalEntry/Put")]
        Task<HttpResponseMessage> Put([Body] PutHospitalEntry model);
    }
}
