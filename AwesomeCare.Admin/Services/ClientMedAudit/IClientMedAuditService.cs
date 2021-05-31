using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientMedAudit
{
    public interface IClientMedAuditService
    {
        [Get("/ClientMedAudit")]
        Task<List<GetClientMedAudit>> Get();

        [Get("/ClientMedAudit/Get/{id}")]
        Task<GetClientMedAudit> Get(int id);

        [Post("/ClientMedAudit/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientMedAudit model);

        [Put("/ClientMedAudit")]
        Task<GetClientMedAudit> Put([Body] PutClientMedAudit model);
    }
}
