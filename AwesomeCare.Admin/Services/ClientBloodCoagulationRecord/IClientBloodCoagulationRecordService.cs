using AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientBloodCoagulationRecord
{
    public interface IClientBloodCoagulationRecordService
    {
        [Get("/ClientBloodCoagulationRecord")]
        Task<List<GetClientBloodCoagulationRecord>> Get();

        [Get("/ClientBloodCoagulationRecord/Get/{id}")]
        Task<GetClientBloodCoagulationRecord> Get(int id);

        [Post("/ClientBloodCoagulationRecord/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientBloodCoagulationRecord model);

        [Put("/ClientBloodCoagulationRecord/Put")]
        Task<HttpResponseMessage> Put([Body] PutClientBloodCoagulationRecord model);
    }
}
